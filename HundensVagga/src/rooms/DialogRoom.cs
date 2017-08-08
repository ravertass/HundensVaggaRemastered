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

        private SoundEffectInstance sound;

        private String exitRoomName;
        public String ExitRoomName {
            get { return exitRoomName; }
        }

        public DialogRoom(string name, Song song, float volume, Texture2D background,
            String exitRoomName, SoundEffectInstance sound, Type specialStateType) 
            : base(name, song, volume, background, new List<Exit>(), new List<Interactable>(),
                  specialStateType) {
            this.sound = sound;
            this.exitRoomName = exitRoomName;
        }

        public override void GoTo() {
            sound.Play();
            base.GoTo();
        }

        public bool ShouldGoToExit() {
            return sound.State == SoundState.Stopped;
        }

        public void Stop() {
            sound.Stop();
        }
    }
}
