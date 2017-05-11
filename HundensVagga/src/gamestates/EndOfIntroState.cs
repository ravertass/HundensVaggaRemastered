using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace HundensVagga {
    internal class EndOfIntroState : IGameState {
        private StateManager stateManager;
        private SongManager songManager;

        private SoundEffectInstance introPianoSound;

        public EndOfIntroState(StateManager stateManager, Main main) {
            this.stateManager = stateManager;
            songManager = main.SongManager;

            MiscContent miscContent = main.MiscContent;
            introPianoSound = miscContent.IntroPianoSound;
            introPianoSound.Play();
        }

        public void Draw(SpriteBatch spriteBatch) {
            // draw nothing
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            songManager.Update();

            if (introPianoSound.State == SoundState.Stopped ||
                inputManager.IsLeftButtonPressed())
                FinishState();
        }

        private void FinishState() {
            introPianoSound.Stop();
            stateManager.GoToMainGameState();
        }
    }
}
