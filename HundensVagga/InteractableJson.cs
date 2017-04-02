using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace HundensVagga {
    /// <summary>
    /// For deserialization of JSON data regarding interactables. Used to create an Interactable instance.
    /// </summary>
    internal class InteractableJson {
        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("look")]
        public string Look { get; set; }

        [JsonProperty("prereqs")]
        public List<PrereqJson> Prereqs { get; set; }

        public Interactable GetInteractableInstance(ContentManager content, StateOfTheWorld worldState) {
            SoundEffectInstance lookSound = GetLookSoundEffect(content);
            IList<Prereq> prereqs = GetPrereqs(worldState);

            Texture2D texture = null;
            Rectangle rect;
            if (Image == null)
                rect = new Rectangle(X, Y, Width, Height);
            else {
                texture = content.Load<Texture2D>(Main.INTERACTABLES_DIR
                    + Path.DirectorySeparatorChar + Image);
                rect = new Rectangle(X, Y, texture.Width, texture.Height);
            }

            return new Interactable(rect, lookSound, prereqs, texture);
        }

        private IList<Prereq> GetPrereqs(StateOfTheWorld worldState) {
            IList<Prereq> prereqs = new List<Prereq>();
            if (Prereqs != null)
                foreach (PrereqJson prereqJson in Prereqs)
                    prereqs.Add(prereqJson.GetPrereqInstance(worldState));

            return prereqs;
        }

        private SoundEffectInstance GetLookSoundEffect(ContentManager content) {
            SoundEffectInstance lookSound;
            if (Look != null)
                lookSound = content.Load<SoundEffect>(Main.VOICE_DIR
                    + Path.DirectorySeparatorChar + Look).CreateInstance();
            else
                lookSound = null;

            return lookSound;
        }
    }
}