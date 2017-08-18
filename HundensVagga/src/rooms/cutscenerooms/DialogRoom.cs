using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace HundensVagga {
    internal class DialogRoom : Room, ICutsceneRoom {

        private SoundAndSubtitle soundAndSubtitle;

        private String exitRoomName;
        public String ExitRoomName {
            get { return exitRoomName; }
        }

        private SoundAndSubtitleManager soundAndSubtitleManager;

        public DialogRoom(string name, Song song, float volume, Texture2D background,
                String exitRoomName, SoundAndSubtitle soundaAndSubtitle,
                List<Interactable> interactables, Type specialStateType, bool withInventory)
            : base(name, song, volume, background, new List<Exit>(), interactables,
                specialStateType, withInventory) {
            this.soundAndSubtitle = soundaAndSubtitle;
            this.exitRoomName = exitRoomName;
        }

        public override void GoTo(GameManager gameManager) {
            soundAndSubtitleManager = gameManager.SoundAndSubtitleManager;
            soundAndSubtitleManager.PlayAndPrint(soundAndSubtitle);
            base.GoTo(gameManager);
        }

        public bool ShouldGoToExit() {
            return soundAndSubtitleManager.Stopped();
        }

        public void Stop() {
            soundAndSubtitleManager.Stop();
        }
    }
}
