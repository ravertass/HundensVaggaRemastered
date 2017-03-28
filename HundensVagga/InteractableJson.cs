using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System.IO;

namespace HundensVagga {
    public class InteractableJson {
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

        public Interactable GetInteractableInstance(ContentManager content) {
            Interactable interactable;

            SoundEffectInstance lookSound = content.Load<SoundEffect>(Main.VOICE_DIR 
                + Path.DirectorySeparatorChar + Look).CreateInstance();
            if (Image == null) {
                Rectangle rect = new Rectangle(X, Y, Width, Height);
                interactable = new Interactable(rect, lookSound);
            } else {
                Texture2D texture = content.Load<Texture2D>(Main.INTERACTABLES_DIR 
                    + Path.DirectorySeparatorChar + Image);
                Rectangle rect = new Rectangle(X, Y, texture.Width, texture.Height);
                interactable = new Interactable(rect, lookSound, texture);
            }

            return interactable;
        }
    }
}