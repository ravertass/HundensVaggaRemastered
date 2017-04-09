using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class TelephoneExploreState : ExploreState {
        private static readonly IList<int> CORRECT_NUMBER = new ReadOnlyCollection<int>(
            new List<int> {5, 4, 7, 2, 3, 1}
         );

        private IList<int> numbersPressed;

        private const string IDLE_SOUND_PATH = "phone_idle";
        private const string WRONG_NUMBER_SOUND_PATH = "phone_wrong_number";
        private SoundEffectInstance idleSound;
        private SoundEffectInstance wrongNumberSound;

        public TelephoneExploreState(MainGameState mainGameState) : base(mainGameState) {
            numbersPressed = new List<int>();
            idleSound = mainGameState.Content.Load<SoundEffect>(Main.SOUND_EFFECTS_DIR +
                    Path.DirectorySeparatorChar + IDLE_SOUND_PATH).CreateInstance();
            wrongNumberSound = mainGameState.Content.Load<SoundEffect>(Main.SOUND_EFFECTS_DIR +
                    Path.DirectorySeparatorChar + WRONG_NUMBER_SOUND_PATH).CreateInstance();

            idleSound.IsLooped = true;
            idleSound.Play();
        }

        public override void Update(InputManager inputManager) {
            if (wrongNumberSound.State == SoundState.Stopped &&
                idleSound.State == SoundState.Stopped)
                idleSound.Play();

            base.Update(inputManager);
        }

        protected override void UseInteractable(Interactable interactable) {
            if (wrongNumberSound.State == SoundState.Stopped) {
                base.UseInteractable(interactable);
                if (interactable.GetType() == typeof(TelephoneInteractable))
                    PressTelephoneButton(((TelephoneInteractable)interactable).Number);
            }
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
                idleSound.Stop();
                wrongNumberSound.Play();
                numbersPressed.Clear();
            }
        }

        protected override void UseExit(Exit exit) {
            wrongNumberSound.Stop();
            idleSound.Stop();
            base.UseExit(exit);
        }
    }
}
