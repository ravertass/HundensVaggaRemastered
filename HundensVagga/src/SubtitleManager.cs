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

        private IList<SubtitlePage> subtitlePages;
        private int pageIndex;

        private Timer timer;

        private static readonly double ADDITIONAL_TIME = 1;

        private readonly int windowWidth;
        private readonly int windowHeight;

        public SubtitleManager(SpriteFont font, int windowWidth, int windowHeight) {
            this.font = font;
            timer = null;

            // It would be preferable if we instead got a reference to a "WindowManager"
            // or something similar, which in turn can give these. That way, it would
            // be possible to change resolution in-game.
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        public void Print(string text, double duration) {
            SetSubtitlePages(text, duration);
        }

        public bool ShouldPrint() {
            return !Stopped();
        }

        public void Stop() {
            timer = null;
        }

        public bool Stopped() {
            return timer == null || (timer.IsDone() && pageIndex == subtitlePages.Count() - 1);
        }

        public void Update(GameTime gameTime) {
            if (timer != null) {
                timer.Update(gameTime);
                if (timer.IsDone() && pageIndex < subtitlePages.Count()) {
                    TurnPage();
                }
            }
        }
        
        public void Draw(SpriteBatch spriteBatch) {
            if (ShouldPrint())
                PrintSubtitles(spriteBatch);
        }

        private void PrintSubtitles(SpriteBatch spriteBatch) {
            string upperLine = CurrentPage().UpperLine;
            string lowerLine = CurrentPage().LowerLine;

            DrawText(spriteBatch, upperLine,
                TextPos(upperLine).X,
                TextPos(upperLine).Y);
            if (lowerLine != null) {
                DrawText(spriteBatch, lowerLine,
                    TextPos(lowerLine).X,
                    TextPos(lowerLine).Y + font.MeasureString(upperLine).Y + 6);
            }
        }

        private void DrawText(SpriteBatch spriteBatch, string text, float x, float y) {
            for (int xOffs = -1; xOffs <= 1; xOffs++)
                for (int yOffs = -1; yOffs <= 1; yOffs++)
                    DrawTextOnce(spriteBatch, text, x+xOffs, y+yOffs, Color.Black);
            DrawTextOnce(spriteBatch, text, x, y, Color.White);
        }

        private void TurnPage() {
            pageIndex++;
            timer = new Timer(CurrentPage().Duration);
        }

        private SubtitlePage CurrentPage() {
            return subtitlePages[pageIndex];
        }

        private void SetSubtitlePages(string text, double duration) {
            subtitlePages = new List<SubtitlePage>();

            IList<string> lines = SplitText(text);
            int nCharsTotal = text.Count();

            for (int i = 0; i <= lines.Count(); i += 2) {
                string upperLine = lines[i];
                string lowerLine = (i == lines.Count() - 1) ? null : lines[i + 1];

                int nCharsUpper = upperLine.Count();
                int nCharsLower = (lowerLine == null) ? 0 : lowerLine.Count();

                double pageDuration =
                    duration * ((double) (nCharsUpper + nCharsLower) / nCharsTotal)
                    + ((i == lines.Count() - 1) ? ADDITIONAL_TIME : 0);
                subtitlePages.Add(new SubtitlePage(upperLine, lowerLine, pageDuration));
            }

            pageIndex = -1;
            TurnPage();
        }

        private IList<string> SplitText(string text) {
            IList<string> lines = new List<string>();
            IList<string> words = text.Split(' ');
            StringBuilder line = new StringBuilder();

            foreach (string word in words) {
                StringBuilder tryLine = new StringBuilder();
                tryLine.Append(line.ToString());
                tryLine.Append(word);
                tryLine.Append(" ");
                if (TextWidth(tryLine.ToString()) < windowWidth - 10) {
                    line.Append(word);
                    line.Append(" ");
                } else {
                    lines.Add(line.ToString().Trim());
                    line = new StringBuilder();
                    line.Append(word);
                    line.Append(" ");
                }
            }

            lines.Add(line.ToString());

            return lines;
        }

        private double TextWidth(string text) {
            return font.MeasureString(text).X;
        }

        private void DrawTextOnce(SpriteBatch spriteBatch, string text, float x, float y, Color color) {
            spriteBatch.DrawString(font, text, new Vector2(x, y), color);
        }

        private Vector2 TextPos(string text) {
            return new Vector2(
                (windowWidth / 2) - (font.MeasureString(text).X / 2),
                windowHeight - 66 - (font.MeasureString(text).Y / 2));
        }
    }
}
