namespace HundensVagga {
    /// <summary>
    /// Contains the current in-game state. Also contains methods for pushing and popping
    /// the current state. This is used because there are special explore states, and we 
    /// want to return to the correct explore state after using the inventory.
    /// </summary>
    internal class InGameStateManager {
        private IInGameState pushedState;
        public IInGameState CurrentState { get; set; }

        public void PushState() {
            pushedState = CurrentState;
        }

        public void PopState() {
            CurrentState = pushedState;
        }
    }
}