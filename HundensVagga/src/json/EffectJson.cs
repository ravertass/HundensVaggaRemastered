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

        [JsonProperty("var")]
        public string Var { get; set; }

        [JsonProperty("vars")]
        public IList<VarValJson> Vars { get; set; }

        [JsonProperty("sound")]
        public string Sound { get; set; }
        
        public IEffect GetEffectInstance(ContentManager content,
                StateOfTheWorld worldState) {
            SoundEffectInstance sound = GetSoundEffect(content);
            IList<VarVal> varVals = GetVarVals(worldState);

            return new Effect(varVals, sound);
        }

        private SoundEffectInstance GetSoundEffect(ContentManager content) {
            SoundEffectInstance sound;
            if (Sound != null)
                sound = content.Load<SoundEffect>(Main.VOICE_DIR
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
    }
}
