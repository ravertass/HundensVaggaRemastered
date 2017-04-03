using Newtonsoft.Json;

namespace HundensVagga {
    /// <summary>
    /// For deserialization of JSON data regarding world state variable and value pairs. 
    /// Used to create a VarVal instance.
    /// </summary>

    internal class VarValJson {
        [JsonProperty("var")]
        public string Var { get; set; }

        [JsonProperty("val")]
        public bool Value { get; set; }

        public VarVal GetVarValInstance(StateOfTheWorld worldState) {
            return new VarVal(worldState.Get(Var), Value);
        }
    }
}