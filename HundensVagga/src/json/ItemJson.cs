using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal enum SpecialItemEnum {
        letter, sound
    }

    internal class ItemJson {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public string IconPath { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("image")]
        public string ImagePath { get; set; }

        [JsonProperty("sound")]
        public string SoundPath { get; set; }

        [JsonProperty("at_start")]
        public bool AtStart { get; set; }

        [JsonProperty("text")]
        public string LetterText { get; set; }

        public IItem GetItemInstance(ContentManager content) {
            return (Type == null)
                ? new Item(Name, GetIcon(content))
                : GetSpecialItemInstance(content);
        }

        private IItem GetSpecialItemInstance(ContentManager content) {
            SpecialItemEnum type = (SpecialItemEnum)Enum.Parse(typeof(SpecialItemEnum), Type);

            switch (type) {
                case SpecialItemEnum.letter:
                    return new LetterItem(Name, GetIcon(content), GetLetterImage(content),
                        LetterText);
                case SpecialItemEnum.sound:
                    return new SoundItem(Name, GetIcon(content), GetSoundEffect(content));
                default:
                    throw new TypeLoadException("No such item type: " + Type);
            }
        } 

        private Texture2D GetIcon(ContentManager content) {
            return content.Load<Texture2D>(Main.INVENTORY_DIR +
                Path.DirectorySeparatorChar + IconPath);
        }

        private Texture2D GetLetterImage(ContentManager content) {
            return content.Load<Texture2D>(Main.LETTERS_DIR +
                Path.DirectorySeparatorChar + ImagePath);
        }

        private SoundEffectInstance GetSoundEffect(ContentManager content) {
            return content.Load<SoundEffect>(Main.SOUND_EFFECTS_DIR +
                Path.DirectorySeparatorChar + SoundPath).CreateInstance();
        }
    }
}
