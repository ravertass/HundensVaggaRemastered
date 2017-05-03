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
    internal enum SpecialRoomTypeEnum {
        walk, panorama
    }

    /// <summary>
    /// For deserialization of JSON game room data. Used to create a Room instance.
    /// </summary>
    internal class RoomJson {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("song")]
        public string Song { get; set; }

        [JsonProperty("volume")]
        public float Volume { get; set; }

        [JsonProperty("background")]
        public string Background { get; set; }

        [JsonProperty("exits")]
        public List<ExitJson> Exits { get; set; }

        [JsonProperty("interactables")]
        public List<InteractableJson> Interactables { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("type")]
        public string RoomType { get; set; }

        [JsonProperty("backgrounds_dir")]
        public string BackgroundsDirectory { get; set; }

        [JsonProperty("backgrounds")]
        public List<string> Backgrounds { get; set; }

        [JsonProperty("exit")]
        public string Exit { get; set; }

        public Room GetRoomInstance(ContentManager content, StateOfTheWorld worldState, 
                Items items, Songs songs) {
            Song song = GetSong(songs);
            float volume = GetVolume();
            Type stateType = GetStateType();

            if (RoomType == null)
                return CreateRoom(content, worldState, items, song, volume, stateType);

            return CreateSpecialRoom(content, worldState, items, song, volume, stateType);
        }

        private Room CreateSpecialRoom(ContentManager content, StateOfTheWorld worldState, 
                Items items, Song song, float volume, Type stateType) {
            SpecialRoomTypeEnum type =
                (SpecialRoomTypeEnum)Enum.Parse(typeof(SpecialRoomTypeEnum), RoomType);

            switch (type) {
                case SpecialRoomTypeEnum.walk:
                    List<Texture2D> backgrounds = GetBackgrounds(content);
                    return new WalkRoom(Name, song, volume, backgrounds, Exit, stateType);
                case SpecialRoomTypeEnum.panorama:
                    Texture2D background = GetBackground(content, Background);
                    return new PanoramaRoom(Name, song, volume, background, Exit, stateType);
                default:
                    throw new TypeLoadException("No such room: " + RoomType);
            }
        }

        private List<Texture2D> GetBackgrounds(ContentManager content) {
            List<Texture2D> backgrounds = new List<Texture2D>();
            foreach (string backgroundName in Backgrounds)
                backgrounds.Add(GetBackground(content, 
                    BackgroundsDirectory + Path.DirectorySeparatorChar + backgroundName));
            return backgrounds;
        }

        private Room CreateRoom(ContentManager content, StateOfTheWorld worldState,
                Items items, Song song, float volume, Type stateType) {
            List<Exit> exits = GetExits(content, worldState);
            List<Interactable> interactables = GetInteractables(content, worldState, items);
            Texture2D background = GetBackground(content, Background);

            return new Room(Name, song, volume, background, exits, interactables, stateType);
        }

        private List<Exit> GetExits(ContentManager content, StateOfTheWorld worldState) {
            List<Exit> exits = new List<Exit>();

            foreach (ExitJson exitJson in Exits)
                exits.Add(exitJson.GetExitInstance(content, worldState));

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

        private float GetVolume() {
            return (Volume == 0.0f) ? 1.0f : Volume;
        }

        private Texture2D GetBackground(ContentManager content, string backgroundName) {
            return content.Load<Texture2D>(Main.BACKGROUNDS_DIR + 
                Path.DirectorySeparatorChar + backgroundName);
        }

        private Type GetStateType() {
            if (State != null) {
                Type stateType = Type.GetType(State);
                if (!stateType.GetInterfaces().Contains(typeof(IInGameState)))
                    throw new Exception("Non-existing state type: " + State);
                return stateType;
            } else
                return null;
        }
    }
}
