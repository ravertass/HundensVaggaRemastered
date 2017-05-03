using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.IO;

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

        [JsonProperty("var")]
        public string Var { get; set; }

        [JsonProperty("sound")]
        public string Sound { get; set; }

        public Exit GetExitInstance(ContentManager content, StateOfTheWorld worldState) {
            return new Exit(GetRectangle(), Room, GetDirectionEnum(), GetPrereqs(worldState), 
                            GetSoundEffect(content), GetVarVal(worldState));
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

        private SoundEffectInstance GetSoundEffect(ContentManager content) {
            if (Sound != null)
                return content.Load<SoundEffect>(Main.SOUND_EFFECTS_DIR
                        + Path.DirectorySeparatorChar + Sound).CreateInstance();

            return null;
        }

        private VarVal GetVarVal(StateOfTheWorld worldState) {
            return (Var != null) ? new VarVal(worldState.Get(Var), true) : null;
        }
    }
}