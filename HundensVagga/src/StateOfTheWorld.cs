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

        public StateOfTheWorld() {
            stateVariables = new Dictionary<string, WorldStateVariable>();
        }

        public WorldStateVariable Get(string stateVariableName) {
            if (!stateVariables.ContainsKey(stateVariableName))
                stateVariables[stateVariableName] = new WorldStateVariable();
            return stateVariables[stateVariableName];
        }
    }
}
