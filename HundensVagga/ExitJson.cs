using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;

namespace HundensVagga {
    public class ExitJson {
        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("room")]
        public string Room { get; set; }

        [JsonProperty("dir")]
        public string Dir { get; set; }

        public Exit GetExitInstance() {
            Rectangle rect = new Rectangle(X, Y, Width, Height);
            return new Exit(rect, Room, (Direction)Enum.Parse(typeof(Direction), Dir));
        }
    }
}