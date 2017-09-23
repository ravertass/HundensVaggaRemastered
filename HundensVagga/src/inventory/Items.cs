using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class Items {
        private readonly Dictionary<string, IItem> itemsMap = new Dictionary<string, IItem>();

        // It's a little stupid that this class contains the inventory, but it makes things easier
        private readonly Inventory inventory;
        public Inventory Inventory {
            get { return inventory; }
        }

        public Items(string path, ContentManager content, Inventory inventory) {
            List<ItemJson> itemsJson = DeserializeItemsJson(path);
            AddItemsFromJsonToMap(content, itemsJson);
            this.inventory = inventory;
            AddStartItemsToInventory(itemsJson);
        }

        private static List<ItemJson> DeserializeItemsJson(string path) {
            string jsonString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<ItemJson>>(jsonString);
        }

        private void AddItemsFromJsonToMap(ContentManager content, List<ItemJson> itemsJson) {
            StateOfTheWorld worldState = new StateOfTheWorld();
            foreach (ItemJson itemJson in itemsJson) {
                itemsMap.Add(itemJson.Name, itemJson.GetItemInstance(content));
            }
        }

        public IItem GetItem(string itemName) {
            return itemsMap[itemName];
        }

        private void AddStartItemsToInventory(List<ItemJson> itemsJson) {
            foreach (ItemJson itemJson in itemsJson) {
                if (itemJson.AtStart)
                    inventory.AddItem(itemsMap[itemJson.Name]);
            }
        }
    }
}
