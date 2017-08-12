using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace HundensVagga {
    internal class PanoramaRoom : Room, ICutsceneRoom {
        private static readonly int SCREEN_X = 800; // should probably be somewhere else

        private readonly int xMax;

        private int x;
        private int dx;
        private double elapsedTime;
        private static readonly double BASE_TIME_STEP = 0.01;
        private static readonly double RIGHTMOST_TIME_STEP = 2;
        private double timeStep = BASE_TIME_STEP;

        private String exitRoomName;
        private Texture2D panorama;

        public String ExitRoomName {
            get { return exitRoomName; }
        }

        public PanoramaRoom(string name, Song song, float volume, Texture2D panorama,
                String exitRoomName, Type specialStateType, bool withInventory) 
            : base(name, song, volume, panorama, new List<Exit>(), new List<Interactable>(), 
                specialStateType, withInventory) {
            this.panorama = panorama;
            this.exitRoomName = exitRoomName;

            xMax = panorama.Width - SCREEN_X;
        }

        public override void GoTo(GameManager gameManager) {
            x = 0;
            dx = 1;
            base.GoTo(gameManager);
        }

        public override void Update(GameTime gameTime) {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;
            elapsedTime += delta;

            while (elapsedTime >= timeStep) {
                elapsedTime -= timeStep;
                AdvanceBackground();
            }

            base.Update(gameTime);
        }

        private void AdvanceBackground() {
            x += dx;
            if (timeStep == RIGHTMOST_TIME_STEP)
                timeStep = BASE_TIME_STEP;
            if (x >= xMax) {
                dx = -dx;
                timeStep = RIGHTMOST_TIME_STEP;
            }
        }
        
        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(panorama, new Vector2(0f, 0f), 
                new Rectangle(x, 0, SCREEN_X, panorama.Height), Color.White);
        }

        public bool ShouldGoToExit() {
            return x <= 0 && dx < 0;
        }

        public void Stop() {
            // do nothing
        }
    }
}
