using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace HundensVagga {
    class TimedRoom : Room, ICutsceneRoom {
        private String exitRoomName;
        public String ExitRoomName {
            get { return exitRoomName; }
        }

        private Timer timer;

        public TimedRoom(String name, Song song, float volume, Texture2D background,
                String exitRoomName, double time, List<Interactable> interactables,
                Type specialStateType, bool withInventory)
            : base(name, song, volume, background, new List<Exit>(), interactables,
                specialStateType, withInventory) {
            this.exitRoomName = exitRoomName;
            timer = new Timer(time);
        }

        public override void Update(GameTime gameTime) {
            timer.Update(gameTime);
            base.Update(gameTime);
        }

        public bool ShouldGoToExit() {
            return timer.IsDone();
        }

        public void Stop() {
            // do nothing
        }
    }
}
