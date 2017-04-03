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
    /// For deserialization of JSON game room data. Used to create a Room instance.
    /// </summary>
    internal class RoomJson {
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

        public Room GetRoomInstance(ContentManager content, StateOfTheWorld worldState, 
                Items items, Songs songs) {
            List<Exit> exits = GetExits(worldState);
            List<Interactable> interactables = GetInteractables(content, worldState, items);
            Song song = GetSong(songs);
            Texture2D background = GetBackground(content);

            return new Room(Name, song, background, exits, interactables);
        }

        private List<Exit> GetExits(StateOfTheWorld worldState) {
            List<Exit> exits = new List<Exit>();

            foreach (ExitJson exitJson in Exits)
                exits.Add(exitJson.GetExitInstance(worldState));

            return exits;
        }

        private List<Interactable> GetInteractables(ContentManager content, 
                StateOfTheWorld worldState, Items items) {
            List<Interactable> interactables = new List<Interactable>();

            foreach (InteractableJson interactableJson in Interactables)
                interactables.Add(
                    interactableJson.GetInteractableInstance(content, worldState, items));

            return interactables;
        }

        private Song GetSong(Songs songs) {
            return songs.GetSong(Song);
        }

        private Texture2D GetBackground(ContentManager content) {
            return content.Load<Texture2D>(Main.BACKGROUNDS_DIR + 
                Path.DirectorySeparatorChar + Background);
        }
    }
}
