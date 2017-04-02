using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    /// <summary>
    /// Contains a boolean variable reflecting an aspect of the game world's state
    /// (e.g. if a door is unlocked or not).
    /// </summary>
    internal class WorldStateVariable {
        public bool Value { get; set; }

        public WorldStateVariable() {
            Value = false;
        }
    }
}
