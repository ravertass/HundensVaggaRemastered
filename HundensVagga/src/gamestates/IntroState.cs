using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace HundensVagga {
    internal class IntroState : IGameState {
        private StateManager stateManager;
        private SongManager songManager;

        private SoundEffectInstance introMonologue;

        public IntroState(StateManager stateManager, Main main) {
            this.stateManager = stateManager;
            songManager = main.SongManager;

            MiscContent miscContent = main.MiscContent;
            songManager.FadeIntoSong(miscContent.IntroSong);
            introMonologue = miscContent.IntroMonologue;
            introMonologue.Play();
        }

        public void Draw(SpriteBatch spriteBatch) {
            // draw nothing
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            songManager.Update();

            if (introMonologue.State == SoundState.Stopped ||
                inputManager.IsLeftButtonPressed())
                FinishState();
        }

        private void FinishState() {
            introMonologue.Stop();
            songManager.FadeOut();
            stateManager.GoToEndOfIntroState();
        }
    }
}
