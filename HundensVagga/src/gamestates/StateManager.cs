using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    /// <summary>
    /// Used so game states can change state without a reference to the Main class instance.
    /// </summary>
    public class StateManager {
        public IGameState CurrentState { get; set; }
    }
}
