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

        private string startRoom;
        public string StartRoom {
            get { return startRoom; }
        }

        private StateOfTheWorld worldState;

        public Rooms(string path, ContentManager content, Assets assets, SongManager songManager,
                StateOfTheWorld worldState) {
            this.worldState = worldState;
            List<RoomJson> roomsJson = DeserializeRoomsJson(path);
            AddRoomsFromJsonToMap(content, roomsJson, assets, songManager);
            SetStartRoom(roomsJson);
        }

        private static List<RoomJson> DeserializeRoomsJson(string path) {
            string jsonString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<RoomJson>>(jsonString);
        }

        private void AddRoomsFromJsonToMap(ContentManager content, List<RoomJson> roomsJson,
                Assets assets, SongManager songManager) {
            foreach (RoomJson roomJson in roomsJson)
                roomsMap.Add(roomJson.Name,
                    roomJson.GetRoomInstance(content, assets, worldState, songManager));
        }

        private void SetStartRoom(List<RoomJson> roomsJson) {
            foreach (RoomJson roomJson in roomsJson)
                if (roomJson.IsStartRoom)
                    startRoom = roomJson.Name;
        }

        public Room GetRoom(string roomName) {
            return roomsMap[roomName];
        }
    }
}
