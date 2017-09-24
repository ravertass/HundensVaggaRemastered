using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    /// <summary>
    /// Contains variables which model the state of the game world.
    /// (note: not an example of the "state" pattern.)
    /// </summary>
    internal class StateOfTheWorld {
        private IDictionary<string, WorldStateVariable> stateVariables;
        public IDictionary<string, WorldStateVariable> StateVariables {
            get { return stateVariables; }
        }

        public StateOfTheWorld() {
            stateVariables = new Dictionary<string, WorldStateVariable>();
        }

        public WorldStateVariable Get(string stateVariableName) {
            if (!stateVariables.ContainsKey(stateVariableName))
                stateVariables[stateVariableName] = new WorldStateVariable();
            return stateVariables[stateVariableName];
        }

        public void Set(string stateVariableName, bool value) {
            if (!stateVariables.ContainsKey(stateVariableName))
                stateVariables[stateVariableName] = new WorldStateVariable();
            stateVariables[stateVariableName].Value = value;
        }

        public void Replace(StateOfTheWorld other) {
            foreach (KeyValuePair<string, WorldStateVariable> entry in other.StateVariables) {
                stateVariables[entry.Key].Value = entry.Value.Value;
            }
        }
    }
}
