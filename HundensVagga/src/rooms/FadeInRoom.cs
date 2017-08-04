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
            String exitRoomName, double time, Type specialStateType)
            : base(name, song, volume, background, new List<Exit>(), new List<Interactable>(),
                  specialStateType) {
            this.exitRoomName = exitRoomName;
            this.time = time;
        }

        public override void GoTo() {
            fadeInBox = new FadeInBox(time);
            base.GoTo();
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
    }
}
