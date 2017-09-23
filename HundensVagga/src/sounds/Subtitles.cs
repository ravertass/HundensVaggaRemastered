using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    /// <summary>
    /// Reads and deserializes the JSON file containing all subtitle data.
    /// </summary>
    internal class Subtitles {
        private IDictionary<String, String> subtitlesMap;

        public Subtitles(string path, ContentManager content) {
            DeserializeSubtitlesJson(path);
        }

        private void DeserializeSubtitlesJson(string path) {
            string jsonString = File.ReadAllText(path);
            subtitlesMap = JsonConvert.DeserializeObject<Dictionary<String,String>>(jsonString);
        }
        public String GetSubtitle(string soundName) {
            return (subtitlesMap.ContainsKey(soundName)) ? subtitlesMap[soundName] : null;
        }
    }
}
