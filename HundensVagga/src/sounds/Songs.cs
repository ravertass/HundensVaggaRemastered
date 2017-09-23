using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class Songs {
        private readonly IDictionary<string, Song> songsMap;
        private readonly ContentManager content;

        public Songs(ContentManager content) {
            songsMap = new Dictionary<string, Song>();
            this.content = content;
        }

        public Song GetSong(string songName) {
            if (!songsMap.ContainsKey(songName))
                LoadSong(songName);

            return songsMap[songName];
        }

        private void LoadSong(string songName) {
            songsMap[songName] = 
                content.Load<Song>(Main.SONGS_DIR + Path.DirectorySeparatorChar + songName);
        }
    }
}
