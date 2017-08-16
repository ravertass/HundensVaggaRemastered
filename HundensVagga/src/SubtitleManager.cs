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
        private Timer timer;
        private string text;
        private IList<string> splitText;

        private int noOfTextPieces;
        private double timePerLine;
        private int pieceIndex;

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

        public void Print(string text, double time) {
            this.text = text;
            splitText = SplitText(text);
            noOfTextPieces = (splitText.Count() + 1) / 2;
            timePerLine = time / (float) splitText.Count();
            pieceIndex = 0;
            timer = new Timer(PieceTime());
        }

        public bool ShouldPrint() {
            return !Stopped() && pieceIndex < noOfTextPieces;
        }

        public void Stop() {
            timer = null;
        }

        public bool Stopped() {
            return timer == null || (timer.IsDone() && pieceIndex == noOfTextPieces);
        }

        public void Update(GameTime gameTime) {
            if (timer != null) {
                timer.Update(gameTime);
                if (timer.IsDone() && pieceIndex < noOfTextPieces) {
                    pieceIndex++;
                    timer = new Timer(PieceTime());
                }
            }
        }

        private double PieceTime() {
            return timePerLine
                + (splitText.Count() > pieceIndex * 2 + 1 ? timePerLine : 0)
                + (pieceIndex == noOfTextPieces - 1 ? ADDITIONAL_TIME : 0);
        }
        
        public void Draw(SpriteBatch spriteBatch) {
            if (ShouldPrint())
                PrintSubtitles(spriteBatch);
        }

        private void PrintSubtitles(SpriteBatch spriteBatch) {
            string upperTextPiece = splitText[pieceIndex * 2];
            string lowerTextPiece = splitText.Count() > pieceIndex*2 + 1
                ? splitText[pieceIndex * 2 + 1]
                : null;

            DrawText(spriteBatch, upperTextPiece,
                TextPos(upperTextPiece).X,
                TextPos(upperTextPiece).Y);
            if (lowerTextPiece != null) {
                DrawText(spriteBatch, lowerTextPiece,
                    TextPos(lowerTextPiece).X,
                    TextPos(lowerTextPiece).Y + font.MeasureString(upperTextPiece).Y + 6);
            }
        }

        private void DrawText(SpriteBatch spriteBatch, string text, float x, float y) {
            for (int xOffs = -1; xOffs <= 1; xOffs++)
                for (int yOffs = -1; yOffs <= 1; yOffs++)
                    DrawTextOnce(spriteBatch, text, x+xOffs, y+yOffs, Color.Black);
            DrawTextOnce(spriteBatch, text, x, y, Color.White);
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
