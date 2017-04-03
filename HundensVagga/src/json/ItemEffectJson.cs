using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class ItemEffectJson {
        [JsonProperty("item")]
        public string ItemName { get; set; }

        [JsonProperty("effect")]
        public EffectJson Effect { get; set; }
    }
}
