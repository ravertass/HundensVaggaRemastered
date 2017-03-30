using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace HundensVagga {
    class MainGameState : IGameState {
        private StateManager stateManager;
        private ContentManager content;
        private CursorManager cursorManager;
        private readonly Rooms rooms;
        private Room currentRoom;
        private Inventory inventory;

        public MainGameState(StateManager stateManager, ContentManager content, 
                CursorManager cursorManager, Rooms rooms) {
            this.stateManager = stateManager;
            this.content = content;
            this.cursorManager = cursorManager;
            this.rooms = rooms;
            currentRoom = rooms.GetRoom("front");
            this.inventory = new Inventory(content);

            // TODO ta bort
            inventory.AddItem(new Item("cat", content.Load<Texture2D>("inventory/cat")));
            inventory.AddItem(new Item("flashlight", content.Load<Texture2D>("inventory/flashlight")));
            inventory.AddItem(new Item("saucepan_hot", content.Load<Texture2D>("inventory/saucepan_hot")));
        }

        public void Update(InputManager inputManager) {
            cursorManager.SetToDefault();
            inventory.Update(inputManager);
            if (!inventory.IsActive())
                Explore(inputManager);
        }

        private void Explore(InputManager inputManager) {
            CheckInteractables(inputManager);
            CheckExits(inputManager);
        }

        private void CheckInteractables(InputManager inputManager) {
            Interactable interactable = currentRoom.GetInteractableAt(inputManager.GetMousePosition());
            if (interactable != null) {
                if (interactable.IsLookable() && interactable.IsUsable())
                    cursorManager.SetToUseLook();
                else if (interactable.IsLookable())
                    cursorManager.SetToLookOnly();
                else if (interactable.IsUsable())
                    cursorManager.SetToUseOnly();

                if (inputManager.IsLeftButtonPressed() && interactable.IsLookable())
                    interactable.PlayLookSound();
            }
        }

        private void CheckExits(InputManager inputManager) {
            Exit exit = currentRoom.GetExitAt(inputManager.GetMousePosition());
            if (exit != null) {
                cursorManager.SetToDirection(exit.Direction);
                if (inputManager.IsLeftButtonPressed())
                    currentRoom = rooms.GetRoom(exit.RoomName);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(currentRoom.Background, new Vector2(0f, 0f), Color.White);
            inventory.Draw(spriteBatch);
        }
    }
}
