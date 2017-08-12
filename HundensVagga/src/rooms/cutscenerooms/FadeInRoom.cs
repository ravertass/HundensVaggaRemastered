using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace HundensVagga {
    internal class FadeInRoom : Room, ICutsceneRoom {
        private double time;
        private FadeInBox fadeInBox;

        private String exitRoomName;
        public String ExitRoomName {
            get { return exitRoomName; }
        }

        public FadeInRoom(String name, Song song, float volume, Texture2D background,
                String exitRoomName, double time, List<Interactable> interactables,
                Type specialStateType, bool withInventory)
            : base(name, song, volume, background, new List<Exit>(), interactables,
                  specialStateType, withInventory) {
            this.exitRoomName = exitRoomName;
            this.time = time;
        }

        public override void GoTo(GameManager gameManager) {
            fadeInBox = new FadeInBox(time);
            base.GoTo(gameManager);
        }

        public override void Update(GameTime gameTime) {
            fadeInBox.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            fadeInBox.Draw(spriteBatch);
        }

        public bool ShouldGoToExit() {
            return fadeInBox.IsDone();
        }

        public void Stop() {
            // do nothing
        }
    }
}
