using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class FadeOutBox {
        private double elapsedTime;
        private static readonly double TIME_STEP = 0.01;

        private float boxTransparency;
        private float boxTransparencyStep;

        private static readonly float MIN_BOX_TRANSPARENCY = 0;
        private static readonly float MAX_BOX_TRANSPARENCY = 1;

        public FadeOutBox(double time) {
            elapsedTime = 0;
            boxTransparency = MIN_BOX_TRANSPARENCY;
            boxTransparencyStep = (float)(TIME_STEP / time);
        }

        public void Update(GameTime gameTime) {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;
            elapsedTime += delta;

            while (elapsedTime >= TIME_STEP) {
                elapsedTime -= TIME_STEP;
                Advance();
            }
        }

        private void Advance() {
            boxTransparency = Math.Min(boxTransparency + boxTransparencyStep, MAX_BOX_TRANSPARENCY);
        }

        public void Draw(SpriteBatch spriteBatch) {
            Texture2D blackTexture =
                new Texture2D(spriteBatch.GraphicsDevice, Main.WINDOW_WIDTH, Main.WINDOW_HEIGHT);
            blackTexture.Dispose();
            spriteBatch.Draw(blackTexture, new Vector2(0f, 0f), new Color(0, 0, 0, 
                (int) Math.Round(boxTransparency*255)));
        }

        public bool IsDone() {
            return boxTransparency == MAX_BOX_TRANSPARENCY;
        }
    }
}
