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
        private readonly Rooms rooms;
        private Room currentRoom;

        public MainGameState(StateManager stateManager, ContentManager content, Rooms rooms) {
            this.stateManager = stateManager;
            this.content = content;
            this.rooms = rooms;
            this.currentRoom = rooms.GetRoom("front");
        }

        public void Update() {
            // not implemented
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(currentRoom.Background, new Vector2(0f, 0f), Color.White);
        }
    }
}
