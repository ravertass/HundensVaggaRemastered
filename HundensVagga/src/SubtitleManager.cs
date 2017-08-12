using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class SubtitleManager {
        private readonly SpriteFont font;
        private static readonly double TIME = 10;
        private Timer timer;
        private string text;

        private readonly int windowWidth;
        private readonly int windowHeight;

        public SubtitleManager(SpriteFont font, int windowWidth, int windowHeight) {
            this.font = font;
            timer = new Timer(0); // timer will be done from start

            // It would be preferable if we instead got a reference to a "WindowManager"
            // or something similar, which in turn can give these. That way, it would
            // be possible to change resolution in-game.
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        public void Print(string text) {
            this.text = text;
            timer = new Timer(TIME);
        }

        public bool ShouldPrint() {
            return !timer.IsDone();
        }

        public void Stop() {
            timer.Stop();
        }

        public void SetTimer(double time) {
            timer = new Timer(time);
        }

        public void Update(GameTime gameTime) {
            timer.Update(gameTime);
        }
        
        public void Draw(SpriteBatch spriteBatch) {
            if (ShouldPrint())
                spriteBatch.DrawString(font, text, TextPos(), Color.White);
        }

        private Vector2 TextPos() {
            return new Vector2(
                (windowWidth / 2) - (font.MeasureString(text).X / 2),
                windowHeight - 16 - (font.MeasureString(text).Y / 2));
        }
    }
}
