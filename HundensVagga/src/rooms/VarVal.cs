using System;

namespace HundensVagga {
    /// <summary>
    /// Models a prerequirement for if an interactable or exit should exist
    /// by keeping a reference to a world state variable and the value that
    /// variable must hold in order for the prerequirement to be met.
    /// </summary>
    internal class VarVal {
        private readonly WorldStateVariable worldStateVariable;
        private readonly bool value;

        public VarVal(WorldStateVariable worldStateVariable, bool value) {
            this.worldStateVariable = worldStateVariable;
            this.value = value;
        }

        internal void Set() {
            worldStateVariable.Value = value;
        }

        public bool IsMet() {
            return worldStateVariable.Value == value;
        }
    }
}