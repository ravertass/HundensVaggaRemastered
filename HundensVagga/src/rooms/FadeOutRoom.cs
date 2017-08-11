using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace HundensVagga {
    internal class FadeOutRoom : Room, ICutsceneRoom {
        private double time;
        private FadeOutBox fadeOutBox;

        private String exitRoomName;
        public String ExitRoomName {
            get { return exitRoomName; }
        }

        public FadeOutRoom(String name, Song song, float volume, Texture2D background,
            String exitRoomName, double time, List<Interactable> interactables,
            Type specialStateType, bool withInventory)
            : base(name, song, volume, background, new List<Exit>(), interactables,
                  specialStateType, withInventory) {
            this.exitRoomName = exitRoomName;
            this.time = time;
        }

        public override void GoTo() {
            fadeOutBox = new FadeOutBox(time);
            base.GoTo();
        }

        public override void Update(GameTime gameTime) {
            fadeOutBox.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            fadeOutBox.Draw(spriteBatch);
        }

        public bool ShouldGoToExit() {
            return fadeOutBox.IsDone();
        }

        public void Stop() {
            // do nothing
        }
    }
}
