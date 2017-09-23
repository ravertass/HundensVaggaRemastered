using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class Assets {
        private Subtitles subtitles;
        public Subtitles Subtitles {
            get { return subtitles; }
        }

        private SoundEffects soundEffects;
        public SoundEffects SoundEffects {
            get { return soundEffects; }
        }

        private Items items;
        public Items Items {
            get { return items; }
        }

        private Songs songs;
        public Songs Songs {
            get { return songs; }
        }

        public Assets(Subtitles subtitles, SoundEffects soundEffects, Items items, Songs songs) {
            this.subtitles = subtitles;
            this.soundEffects = soundEffects;
            this.items = items;
            this.songs = songs;
        }

        public SoundAndSubtitle GetSoundAndSubtitle(ContentManager content, String name) {
            return new SoundAndSubtitle(
                soundEffects.GetSoundEffect(content, name),
                subtitles.GetSubtitle(name));
        }
    }
}
