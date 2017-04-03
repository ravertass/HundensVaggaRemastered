using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class EffectJson {
        private const bool DEFAULT_SET_VALUE = true;

        // World state variable set to DEFAULT_SET_VALUE
        // (it's possible to use vars below, but easier to just use var)
        [JsonProperty("var")]
        public string Var { get; set; }

        // World state variables set to defined values
        [JsonProperty("vars")]
        public IList<VarValJson> Vars { get; set; }

        // Sound effect that is played
        [JsonProperty("sound")]
        public string Sound { get; set; }

        // Item that is added to the inventory
        [JsonProperty("item")]
        public string ItemName { get; set; }

        public IEffect GetEffectInstance(ContentManager content,
                StateOfTheWorld worldState, Items items) {
            SoundEffectInstance sound = GetSoundEffect(content);
            IList<VarVal> varVals = GetVarVals(worldState);
            Item item = GetItem(items);

            return new Effect(varVals, sound, item, items.Inventory);
        }

        private SoundEffectInstance GetSoundEffect(ContentManager content) {
            SoundEffectInstance sound;
            if (Sound != null)
                sound = content.Load<SoundEffect>(Main.SOUND_EFFECTS_DIR
                    + Path.DirectorySeparatorChar + Sound).CreateInstance();
            else
                sound = null;

            return sound;
        }

        private IList<VarVal> GetVarVals(StateOfTheWorld worldState) {
            IList<VarVal> varVals = new List<VarVal>();
            if (Vars != null)
                foreach (VarValJson varValJson in Vars)
                    varVals.Add(varValJson.GetVarValInstance(worldState));
            if (Var != null)
                varVals.Add(new VarVal(worldState.Get(Var), DEFAULT_SET_VALUE));

            return varVals;
        }

        private Item GetItem(Items items) {
            if (ItemName != null)
                return items.GetItem(ItemName);
            else
                return null;
        }
    }
}
