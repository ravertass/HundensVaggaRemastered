using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    enum LoadGameStatus {
        SUCCESS, NO_FILE, FAILURE
    }

    class SaveGameManager {
        private Items allItems;
        private Inventory inventory;
        private StateOfTheWorld worldState;

        private static readonly string SAVE_FILE_NAME = "savegame.dog";

        public SaveGameManager(Items allItems, Inventory inventory, StateOfTheWorld worldState) {
            this.allItems = allItems;
            this.inventory = inventory;
            this.worldState = worldState;
        }

        public void SaveGame(Room currentRoom) {
            string saveFilePath = Path.Combine(Directory.GetCurrentDirectory(), SAVE_FILE_NAME);
            File.WriteAllText(saveFilePath, SerializeGameState(currentRoom.Name));
        }

        public LoadGameStatus LoadGame(GameManager gameManager) {
            string saveFilePath = Path.Combine(Directory.GetCurrentDirectory(), SAVE_FILE_NAME);
            if (File.Exists(saveFilePath)) {
                try {
                    string serializedGameState = File.ReadAllText(saveFilePath);
                    DeserializeGameState(gameManager, serializedGameState);
                } catch {
                    return LoadGameStatus.FAILURE;
                }

                return LoadGameStatus.SUCCESS;
            }

            return LoadGameStatus.NO_FILE;
        }

        private string SerializeGameState(string currentRoomName) {
            return SerializeItems() + "\n" + SerializeWorldState() + "\n" + currentRoomName;
        }

        private void DeserializeGameState(GameManager gameManager, string serializedGameState) {
            string serializedItems = serializedGameState.Split('\n')[0];
            string serializedWorldState = serializedGameState.Split('\n')[1];
            string currentRoomName = serializedGameState.Split('\n')[2];

            inventory.Items = DeserializeItems(serializedItems);
            worldState.Replace(DeserializeWorldState(serializedWorldState));
            gameManager.GoToRoom(currentRoomName);
        }

        private string SerializeItems() {
            StringBuilder stringBuilder = new StringBuilder();
            bool first = true;
            foreach (IItem item in inventory.Items) {
                if (!first)
                    stringBuilder.Append(",");
                first = false;

                stringBuilder.Append(item.Name);
            }
            return stringBuilder.ToString();
        }

        private IList<IItem> DeserializeItems(string serializedItems) {
            IList<IItem> itemList = new List<IItem>();
            foreach (string itemName in serializedItems.Split(',')) {
                IItem item = allItems.GetItem(itemName);
                itemList.Add(item);
            }
            return itemList;
        }

        private string SerializeWorldState() {
            StringBuilder stringBuilder = new StringBuilder();
            bool first = true;
            foreach (KeyValuePair<string, WorldStateVariable> entry in worldState.StateVariables) {
                if (!first)
                    stringBuilder.Append(",");
                first = false;

                string name = entry.Key;
                bool value = entry.Value.Value;
                stringBuilder.Append(name);
                stringBuilder.Append(":");
                stringBuilder.Append(value);
            }
            return stringBuilder.ToString();
        }

        private StateOfTheWorld DeserializeWorldState(string serializedWorldState) {
            StateOfTheWorld worldState = new StateOfTheWorld();
            foreach (string serializedStateVar in serializedWorldState.Split(',')) {
                string name = serializedStateVar.Split(':')[0];
                bool value = Convert.ToBoolean(serializedStateVar.Split(':')[1]);
                worldState.Set(name, value);
            }
            return worldState;
        }
    }
}
