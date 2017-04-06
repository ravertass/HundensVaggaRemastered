using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class TelephoneExploreState : ExploreState {
        private static readonly IList<int> CORRECT_NUMBER = new ReadOnlyCollection<int>(
            new List<int> {5, 4, 7, 2, 3, 1}
         );

        private IList<int> numbersPressed;

        public TelephoneExploreState(MainGameState mainGameState) : base(mainGameState) {
            numbersPressed = new List<int>();
        }

        protected override void UseInteractable(Interactable interactable) {
            if (interactable.GetType() == typeof(TelephoneInteractable))
                PressTelephoneButton(((TelephoneInteractable)interactable).Number);
            else
                base.UseInteractable(interactable);
        }

        private void PressTelephoneButton(int number) {
            numbersPressed.Add(number);
            if (numbersPressed.Count == 6)
                MakePhoneCall();
        }

        private void MakePhoneCall() {
            if (numbersPressed.Equals(CORRECT_NUMBER))
                // byt till ett InGameState där vinstsnacket dras, sedan körs Win()
                mainGameState.Win();
            else {
                // spela fail-ljudet
                numbersPressed.Clear();
            }
        }
    }
}
