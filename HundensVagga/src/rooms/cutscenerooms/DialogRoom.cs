using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace HundensVagga {
    internal class DialogRoom : Room, ICutsceneRoom {

        private Boolean soundStarted;

        private SoundAndSubtitle soundAndSubtitle;

        private String exitRoomName;
        public String ExitRoomName {
            get { return exitRoomName; }
        }

        private SoundAndSubtitleManager soundAndSubtitleManager;

        public DialogRoom(string name, Song song, float volume, Texture2D background,
                String exitRoomName, SoundAndSubtitle soundAndSubtitle,
                List<Interactable> interactables, Type specialStateType, bool withInventory)
            : base(name, song, volume, background, new List<Exit>(), interactables,
                specialStateType, withInventory) {
            this.soundAndSubtitle = soundAndSubtitle;
            this.exitRoomName = exitRoomName;
        }

        public override void GoTo(GameManager gameManager) {
            soundAndSubtitleManager = gameManager.SoundAndSubtitleManager;
            soundStarted = false;
            base.GoTo(gameManager);
        }

        public override void Update(GameTime gameTime) {
            if (!soundStarted && soundAndSubtitleManager.Stopped())
                StartSound();
            base.Update(gameTime);
        }

        private void StartSound() {
            soundAndSubtitleManager.PlayAndPrint(soundAndSubtitle);
            soundStarted = true;
        }

        public bool ShouldGoToExit() {
            return soundStarted && soundAndSubtitleManager.Stopped();
        }

        public void Stop() {
            soundAndSubtitleManager.Stop();
        }
    }
}
