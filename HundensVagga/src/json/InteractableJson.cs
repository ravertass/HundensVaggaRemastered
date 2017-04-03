using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections;

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

        [JsonProperty("use")]
        public EffectJson Use { get; set; }

        [JsonProperty("items")]
        public List<ItemEffectJson> ItemEffects { get; set; }

        [JsonProperty("prereqs")]
        public List<VarValJson> Prereqs { get; set; }

        public Interactable GetInteractableInstance(ContentManager content, 
                StateOfTheWorld worldState, Items items) {
            SoundEffectInstance lookSound = GetLookSoundEffect(content);
            IEffect useEffect = GetUseEffect(content, worldState, items);
            IDictionary<string, IEffect> itemEffects = GetItemEffects(content, worldState, items);
            IList<VarVal> prereqs = GetPrereqs(worldState);

            Texture2D texture = null;
            Rectangle rect;
            if (Image == null)
                rect = new Rectangle(X, Y, Width, Height);
            else {
                texture = content.Load<Texture2D>(Main.INTERACTABLES_DIR
                    + Path.DirectorySeparatorChar + Image);
                rect = new Rectangle(X, Y, texture.Width, texture.Height);
            }

            return new Interactable(rect, lookSound, useEffect, itemEffects, prereqs, texture);
        }

        private SoundEffectInstance GetLookSoundEffect(ContentManager content) {
            if (Look != null)
                return content.Load<SoundEffect>(Main.VOICE_DIR
                    + Path.DirectorySeparatorChar + Look).CreateInstance();
            else
                return null;
        }

        private IEffect GetUseEffect(ContentManager content, StateOfTheWorld worldState, 
                Items items) {
            if (Use != null)
                return Use.GetEffectInstance(content, worldState, items);
            else
                return null;

        }

        private IDictionary<string, IEffect> GetItemEffects(ContentManager content, 
                StateOfTheWorld worldState, Items items) {
            Dictionary<string, IEffect> itemEffects = new Dictionary<string, IEffect>();
            if (ItemEffects != null)
                foreach (ItemEffectJson itemEffectJson in ItemEffects)
                    itemEffects.Add(itemEffectJson.ItemName,
                        itemEffectJson.Effect.GetEffectInstance(content, worldState, items));
            return itemEffects;
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