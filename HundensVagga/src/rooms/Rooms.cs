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
    /// Reads and deserializes the JSON file containing all game room data.
    /// Keeps track of all game rooms afterward.
    /// </summary>
    internal class Rooms {
        private readonly Dictionary<string, Room> roomsMap = new Dictionary<string, Room>();

        public Rooms(string path, ContentManager content, Subtitles subtitles, Items items,
                SongManager songManager) {
            Songs songs = new Songs(content);
            List<RoomJson> roomsJson = DeserializeRoomsJson(path);
            AddRoomsFromJsonToMap(content, subtitles, roomsJson, items, songs, songManager);
        }

        private static List<RoomJson> DeserializeRoomsJson(string path) {
            string jsonString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<RoomJson>>(jsonString);
        }

        private void AddRoomsFromJsonToMap(ContentManager content, Subtitles subtitles,
                List<RoomJson> roomsJson, Items items, Songs songs, SongManager songManager) {
            StateOfTheWorld worldState = new StateOfTheWorld();
            foreach (RoomJson roomJson in roomsJson)
                roomsMap.Add(roomJson.Name, 
                    roomJson.GetRoomInstance(content, subtitles, worldState, items, songs,
                                             songManager));
        }

        public Room GetRoom(string roomName) {
            return roomsMap[roomName];
        }
    }
}
