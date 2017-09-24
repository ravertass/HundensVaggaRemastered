using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using System.Collections;
using System.Diagnostics;

namespace HundensVagga {
    internal enum SpecialInteractableEnum {
        telephone
    }

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

        [JsonProperty("click")]
        public EffectJson Click { get; set; }

        [JsonProperty("hover")]
        public EffectJson Hover { get; set; }

        [JsonProperty("items")]
        public List<ItemEffectJson> ItemEffects { get; set; }

        [JsonProperty("prereqs")]
        public List<VarValJson> Prereqs { get; set; }

        [JsonProperty("type")]
        public string SpecialType { get; set; }

        [JsonProperty("number")]
        public int Number { get; set; }

        public Interactable GetInteractableInstance(ContentManager content, Assets assets,
                StateOfTheWorld worldState, SongManager songManager) {
            SoundAndSubtitle lookSoundAndSubtitle = GetLookSoundAndSubtitle(content, assets);
            IEffect useEffect = GetEffect(Use, content, assets, worldState, songManager);
            IEffect clickEffect = GetEffect(Click, content, assets, worldState, songManager);
            IEffect hoverEffect = GetEffect(Hover, content, assets, worldState, songManager);

            Trace.Assert(!(clickEffect != null &&
                (lookSoundAndSubtitle != null || useEffect != null)),
                "click effect cannot coexist with look sounds or use effects");

            IDictionary<string, IEffect> itemEffects = GetItemEffects(content, assets, worldState, 
                songManager);
            IList<VarVal> prereqs = GetPrereqs(worldState);
            Texture2D texture = GetTexture(content);
            Rectangle rect = GetRectangle(texture);

            return GetInteractable(rect, lookSoundAndSubtitle, useEffect, clickEffect, hoverEffect,
                itemEffects, prereqs, texture);
        }

        private SoundAndSubtitle GetLookSoundAndSubtitle(ContentManager content, Assets assets) {
            if (Look != null)
                return assets.GetSoundAndSubtitle(content, Look);

            return null;
        }

        private IEffect GetEffect(EffectJson effect, ContentManager content, Assets assets,
                StateOfTheWorld worldState, SongManager songManager) {
            if (effect != null)
                return effect.GetEffectInstance(content, assets, worldState, songManager);

            return null;
        }

        private IDictionary<string, IEffect> GetItemEffects(ContentManager content,
                Assets assets, StateOfTheWorld worldState, SongManager songManager) {
            Dictionary<string, IEffect> itemEffects = new Dictionary<string, IEffect>();
            if (ItemEffects != null)
                foreach (ItemEffectJson itemEffectJson in ItemEffects)
                    itemEffects.Add(itemEffectJson.ItemName,
                        itemEffectJson.Effect.GetEffectInstance(content, assets, worldState,
                            songManager));

            return itemEffects;
        }

        private IList<VarVal> GetPrereqs(StateOfTheWorld worldState) {
            IList<VarVal> prereqs = new List<VarVal>();
            if (Prereqs != null)
                foreach (VarValJson prereqJson in Prereqs)
                    prereqs.Add(prereqJson.GetVarValInstance(worldState));

            return prereqs;
        }

        private Texture2D GetTexture(ContentManager content) {
            if (Image != null)
                return content.Load<Texture2D>(Main.INTERACTABLES_DIR
                    + Path.DirectorySeparatorChar + Image);

            return null;
        }

        private Rectangle GetRectangle(Texture2D texture) {
            if (texture != null)
                return new Rectangle(X, Y, texture.Width, texture.Height);

            return new Rectangle(X, Y, Width, Height);
        }

        private Interactable GetInteractable(Rectangle rect, SoundAndSubtitle lookSoundAndSubtitle,
                IEffect useEffect, IEffect clickEffect, IEffect hoverEffect,
                IDictionary<string, IEffect> itemEffects, IList<VarVal> prereqs,
                Texture2D texture) {
            if (SpecialType != null)
                return GetSpecialInteractable(rect, lookSoundAndSubtitle, useEffect, clickEffect,
                    hoverEffect, itemEffects, prereqs, texture);

            return new Interactable(rect, lookSoundAndSubtitle, useEffect, clickEffect,
                hoverEffect, itemEffects, prereqs, texture);
        }

        private Interactable GetSpecialInteractable(Rectangle rect,
                SoundAndSubtitle lookSoundAndSubtitle, IEffect useEffect, IEffect clickEffect,
                IEffect hoverEffect, IDictionary<string, IEffect> itemEffects,
                IList<VarVal> prereqs, Texture2D texture) {
            SpecialInteractableEnum type = 
                (SpecialInteractableEnum)Enum.Parse(typeof(SpecialInteractableEnum), SpecialType);

            switch (type) {
                case SpecialInteractableEnum.telephone:
                    return new TelephoneInteractable(rect, lookSoundAndSubtitle, useEffect,
                        clickEffect, hoverEffect, itemEffects, prereqs, Number, texture);
                default:
                    throw new TypeLoadException("No such interactable: " + SpecialType);
            }
        }
    }
}