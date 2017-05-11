using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    internal class LogoState : IGameState {

        private readonly IList<Texture2D> logos;

        private double elapsedTime;
        private static readonly double TIME_STEP = 0.01;

        private float logoTransparency;
        private float logoTransparencyStep;
        private static readonly float LOGO_TRANSPARENCY_BASE_STEP = 0.005f;
        private static readonly float MAX_LOGO_TRANSPARENCY = 1;

        private int currentLogoIndex;

        private StateManager stateManager;

        public LogoState(StateManager stateManager, MiscContent miscContent) {
            this.stateManager = stateManager;
            logos = miscContent.CompanyLogos;
            elapsedTime = 0;
            ResetLogoTransparency();
            currentLogoIndex = -1;
            AdvanceLogoIndex();
        }

        private void ResetLogoTransparency() {
            logoTransparency = 0;
            logoTransparencyStep = LOGO_TRANSPARENCY_BASE_STEP;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(CurrentLogoTexture(), CurrentLogoPos(), 
                Color.White * logoTransparency);
        }

        private Texture2D CurrentLogoTexture() {
            return logos[currentLogoIndex];
        }

        private Vector2 CurrentLogoPos() {
            return new Vector2(
                (Main.WINDOW_WIDTH - CurrentLogoTexture().Width) / 2,
                (Main.WINDOW_HEIGHT - CurrentLogoTexture().Height) / 2);
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;
            elapsedTime += delta;

            while (elapsedTime >= TIME_STEP) {
                elapsedTime -= TIME_STEP;
                AdvanceLogos();
            }

            if (inputManager.IsLeftButtonPressed())
                AdvanceLogoIndex();
        }

        private void AdvanceLogos() {
            logoTransparency += logoTransparencyStep;

            if (logoTransparency >= MAX_LOGO_TRANSPARENCY)
                logoTransparencyStep = -LOGO_TRANSPARENCY_BASE_STEP;

            if (logoTransparency <= 0)
                AdvanceLogoIndex();
        }

        private void AdvanceLogoIndex() {
            currentLogoIndex++;
            if (currentLogoIndex > logos.Count() - 1)
                FinishState();
            ResetLogoTransparency();
        }

        private void FinishState() {
            stateManager.GoToMainMenuFadeInState();
        }
    }
}
