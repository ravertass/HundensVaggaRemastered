﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class SubtitleManager {
        private readonly SpriteFont font;
        private Timer timer;
        private string text;

        private readonly int windowWidth;
        private readonly int windowHeight;

        private bool printing;

        public SubtitleManager(SpriteFont font, int windowWidth, int windowHeight) {
            this.font = font;
            timer = null;
            printing = false;

            // It would be preferable if we instead got a reference to a "WindowManager"
            // or something similar, which in turn can give these. That way, it would
            // be possible to change resolution in-game.
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        public void Print(string text) {
            this.text = text;
            printing = true;
            timer = null;
        }

        public bool ShouldPrint() {
            return printing;
        }

        public void Stop() {
            printing = false;
            timer = null;
        }

        public void SetTimer(double time) {
            timer = new Timer(time);
        }

        public void Update(GameTime gameTime) {
            if (timer != null) {
                timer.Update(gameTime);

                if (timer.IsDone()) {
                    printing = false;
                    timer = null;
                }
            }
        }
        
        public void Draw(SpriteBatch spriteBatch) {
            if (ShouldPrint())
                DrawText(spriteBatch, text);
        }

        private void DrawText(SpriteBatch spriteBatch, string text) {
            float x = TextPos().X;
            float y = TextPos().Y;

            for (int xOffs = -1; xOffs <= 1; xOffs++)
                for (int yOffs = -1; yOffs <= 1; yOffs++)
                    DrawTextOnce(spriteBatch, text, x+xOffs, y+yOffs, Color.Black);
            DrawTextOnce(spriteBatch, text, x, y, Color.White);
        }

        private void DrawTextOnce(SpriteBatch spriteBatch, string text, float x, float y, Color color) {
            spriteBatch.DrawString(font, text, new Vector2(x, y), color);
        }

        private Vector2 TextPos() {
            return new Vector2(
                (windowWidth / 2) - (font.MeasureString(text).X / 2),
                windowHeight - 66 - (font.MeasureString(text).Y / 2));
        }
    }
}
