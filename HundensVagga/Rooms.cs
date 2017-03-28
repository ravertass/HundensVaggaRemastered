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
    class Rooms {
        private readonly Dictionary<string, Room> roomsMap = new Dictionary<string, Room>();

        public Rooms(string path, ContentManager content) {
            List<RoomJson> roomsJson = DeserializeRoomsJson(path);
            AddRoomsFromJsonToMap(content, roomsJson);
        }

        private static List<RoomJson> DeserializeRoomsJson(string path) {
            string jsonString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<RoomJson>>(jsonString);
        }

        private void AddRoomsFromJsonToMap(ContentManager content, List<RoomJson> roomsJson) {
            foreach (RoomJson roomJson in roomsJson) {
                roomsMap.Add(roomJson.Name, roomJson.GetRoomInstance(content));
            }
        }

        public Room GetRoom(string roomName) {
            return roomsMap[roomName];
        }
    }
}
