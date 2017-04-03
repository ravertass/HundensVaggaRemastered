using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HundensVagga {
    /// <summary>
    /// For deserialization of JSON data regarding exits. Used to create an Exit instance.
    /// </summary>
    internal class ExitJson {
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

        [JsonProperty("prereqs")]
        public List<VarValJson> Prereqs { get; set; }

        public Exit GetExitInstance(StateOfTheWorld worldState) {
            return new Exit(GetRectangle(), Room, GetDirectionEnum(), GetPrereqs(worldState));
        }

        private Rectangle GetRectangle() {
            return new Rectangle(X, Y, Width, Height);
        }

        private Direction GetDirectionEnum() {
            return (Direction)Enum.Parse(typeof(Direction), Dir);
        }

        private IList<VarVal> GetPrereqs(StateOfTheWorld worldState) {
            IList<VarVal> prereqs = new List<VarVal>();
            if (Prereqs != null)
                foreach (VarValJson prereqJson in Prereqs)
                    prereqs.Add(prereqJson.GetVarValInstance(worldState));

            return prereqs;
        }
    }
}