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
    class RoomJson {
        public const string BACKGROUND_DIR = "backgrounds";
        public const string INTERACTABLES_DIR = "interactables";
        public const string SONG_DIR = "songs";
        public const string VOICE_DIR = "voice";

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("song")]
        public string Song { get; set; }

        [JsonProperty("background")]
        public string Background { get; set; }

        [JsonProperty("exits")]
        public List<ExitJson> Exits { get; set; }

        [JsonProperty("interactables")]
        public List<InteractableJson> Interactables { get; set; }

        public Room GetRoomInstance(ContentManager content) {
            List<Exit> exits = GetExits();
            List<Interactable> interactables = GetInteractables(content);
            Song song = GetSong(content);
            Texture2D background = GetBackground(content);

            return new Room(Name, song, background, exits, interactables);
        }

        private List<Exit> GetExits() {
            List<Exit> exits = new List<Exit>();

            foreach (ExitJson exitJson in Exits) {
                exits.Add(exitJson.GetExitInstance());
            }

            return exits;
        }

        private List<Interactable> GetInteractables(ContentManager content) {
            List<Interactable> interactables = new List<Interactable>();

            foreach (InteractableJson interactableJson in Interactables) {
                interactables.Add(interactableJson.GetInteractableInstance(content));
            }

            return interactables;
        }

        private Song GetSong(ContentManager content) {
            return content.Load<Song>(SONG_DIR + Path.DirectorySeparatorChar + Song);
        }

        private Texture2D GetBackground(ContentManager content) {
            return content.Load<Texture2D>(BACKGROUND_DIR + Path.DirectorySeparatorChar + Background);
        }
    }
}
