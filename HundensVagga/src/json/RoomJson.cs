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
        walk, panorama, dialog, fadein, fadeout, timed, logo
    }

    /// <summary>
    /// For deserialization of JSON game room data. Used to create a Room instance.
    /// </summary>
    internal class RoomJson {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("song")]
        public String Song { get; set; }

        [JsonProperty("volume")]
        public float Volume { get; set; }

        [JsonProperty("background")]
        public String Background { get; set; }

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

        [JsonProperty("logos")]
        public List<string> Logos { get; set; }

        [JsonProperty("exit")]
        public string Exit { get; set; }

        [JsonProperty("time")]
        public double Time { get; set; }

        [JsonProperty("sound")]
        public string Sound { get; set; }

        [JsonProperty("without_inventory")]
        public bool WithoutInventory { get; set; }

        public Room GetRoomInstance(ContentManager content, StateOfTheWorld worldState, 
                Items items, Songs songs, SongManager songManager) {
            Song song = GetSong(songs);
            float volume = GetVolume();
            Type stateType = GetStateType();

            if (RoomType == null)
                return CreateRoom(content, worldState, items, song, volume, stateType, songs, 
                    songManager);

            return CreateSpecialRoom(content, songs, songManager, worldState, items, song, volume,
                stateType);
        }

        private Room CreateSpecialRoom(ContentManager content, Songs songs,
                SongManager songManager, StateOfTheWorld worldState, Items items, Song song,
                float volume, Type stateType) {
            SpecialRoomTypeEnum type =
                (SpecialRoomTypeEnum)Enum.Parse(typeof(SpecialRoomTypeEnum), RoomType);

            Texture2D background;
            List<Interactable> interactables;
            switch (type) {
                case SpecialRoomTypeEnum.walk:
                    List<Texture2D> backgrounds = GetBackgrounds(content);
                    double time = Time != 0.0 ? Time : 1.5;
                    return new WalkRoom(Name, song, volume, backgrounds, Exit, time, stateType,
                        !WithoutInventory);
                case SpecialRoomTypeEnum.panorama:
                    background = GetBackground(content, Background);
                    return new PanoramaRoom(Name, song, volume, background, Exit, stateType,
                        !WithoutInventory);
                case SpecialRoomTypeEnum.fadein:
                    background = GetBackground(content, Background);
                    interactables = GetInteractables(content, worldState, items,
                        songs, songManager);
                    return new FadeInRoom(Name, song, volume, background, Exit, Time,
                        interactables, stateType, !WithoutInventory);
                case SpecialRoomTypeEnum.fadeout:
                    background = GetBackground(content, Background);
                    interactables = GetInteractables(content, worldState, items,
                        songs, songManager);
                    return new FadeOutRoom(Name, song, volume, background, Exit, Time,
                        interactables, stateType, !WithoutInventory);
                case SpecialRoomTypeEnum.dialog:
                    background = GetBackground(content, Background);
                    SoundEffect sound = GetSoundEffect(content);
                    interactables = GetInteractables(content, worldState, items,
                        songs, songManager);
                    return new DialogRoom(Name, song, volume, background, Exit, sound,
                        interactables, stateType, !WithoutInventory);
                case SpecialRoomTypeEnum.timed:
                    background = GetBackground(content, Background);
                    interactables = GetInteractables(content, worldState, items,
                        songs, songManager);
                    return new TimedRoom(Name, song, volume, background, Exit, Time, interactables,
                        stateType, !WithoutInventory);
                case SpecialRoomTypeEnum.logo:
                    IList<Texture2D> logos = GetLogos(content);
                    return new LogoRoom(Name, song, volume, logos, Exit, Time, stateType,
                        !WithoutInventory);
                default:
                    throw new TypeLoadException("No such room: " + RoomType);
            }
        }

        private SoundEffect GetSoundEffect(ContentManager content) {
            if (Sound != null)
                return content.Load<SoundEffect>(Main.DIALOG_DIR
                    + Path.DirectorySeparatorChar + Sound);

            return null;
        }

        private List<Texture2D> GetBackgrounds(ContentManager content) {
            List<Texture2D> backgrounds = new List<Texture2D>();
            foreach (string backgroundName in Backgrounds)
                backgrounds.Add(GetBackground(content, 
                    BackgroundsDirectory + Path.DirectorySeparatorChar + backgroundName));
            return backgrounds;
        }

        private Room CreateRoom(ContentManager content, StateOfTheWorld worldState,
                Items items, Song song, float volume, Type stateType, Songs songs, 
                SongManager songManager) {
            List<Exit> exits = GetExits(content, worldState);
            List<Interactable> interactables = GetInteractables(content, worldState, items,
                songs, songManager);
            Texture2D background = GetBackground(content, Background);

            return new Room(Name, song, volume, background, exits, interactables, stateType,
                        !WithoutInventory);
        }

        private List<Exit> GetExits(ContentManager content, StateOfTheWorld worldState) {
            List<Exit> exits = new List<Exit>();

            if (Exits != null)
                foreach (ExitJson exitJson in Exits)
                    exits.Add(exitJson.GetExitInstance(content, worldState));

            return exits;
        }

        private List<Interactable> GetInteractables(ContentManager content, 
                StateOfTheWorld worldState, Items items, Songs songs, SongManager songManager) {
            List<Interactable> interactables = new List<Interactable>();

            if (Interactables != null)
                foreach (InteractableJson interactableJson in Interactables)
                    interactables.Add(
                        interactableJson.GetInteractableInstance(content, worldState, items,
                            songs, songManager));

            return interactables;
        }

        private Song GetSong(Songs songs) {
            return Song == null ? null : songs.GetSong(Song);
        }

        private float GetVolume() {
            return (Volume == 0.0f) ? 1.0f : Volume;
        }

        private Texture2D GetBackground(ContentManager content, String backgroundName) {
            return backgroundName == null ? null : content.Load<Texture2D>(Main.BACKGROUNDS_DIR + 
                Path.DirectorySeparatorChar + backgroundName);
        }

        private IList<Texture2D> GetLogos(ContentManager content) {
            IList<Texture2D> logos = new List<Texture2D>();

            foreach (string logoName in Logos)
                logos.Add(content.Load<Texture2D>(Main.LOGOS_DIR +
                    Path.DirectorySeparatorChar + logoName));

            return logos;
        }

        private Type GetStateType() {
            if (State != null) {
                Type stateType = Type.GetType(State);
                if (!stateType.GetInterfaces().Contains(typeof(IGameState)))
                    throw new Exception("Non-existing state type: " + State);
                return stateType;
            } else
                return null;
        }
    }
}
