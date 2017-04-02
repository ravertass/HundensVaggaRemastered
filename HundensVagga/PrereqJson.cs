using Newtonsoft.Json;

namespace HundensVagga {
    /// <summary>
    /// For deserialization of JSON data regarding prerequirements. 
    /// Used to create a Prereq instance.
    /// </summary>

    internal class PrereqJson {
        [JsonProperty("var")]
        public string Var { get; set; }

        [JsonProperty("val")]
        public bool Value { get; set; }

        public Prereq GetPrereqInstance(StateOfTheWorld worldState) {
            return new Prereq(worldState.Get(Var), Value);
        }
    }
}