using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

namespace HundensVagga {
    internal class WalkRoom : Room, ICutsceneRoom {

        private List<Texture2D> backgrounds;

        private int backgroundIndex;
        private double elapsedTime;
        private static readonly double TIME_STEP = 1.5;

        private String exitRoomName;
        public String ExitRoomName {
            get { return exitRoomName; }
        }

        public WalkRoom(string name, Song song, float volume, List<Texture2D> backgrounds, 
            String exitRoomName, Type specialStateType) 
            : base(name, song, volume, GetFirstBackground(backgrounds), new List<Exit>(), 
                new List<Interactable>(), specialStateType) {
            this.backgrounds = backgrounds;
            this.exitRoomName = exitRoomName;
            GoTo();
        }

        public override void GoTo() {
            elapsedTime = 0;
            background = backgrounds[0];
            backgroundIndex = 1;
            base.GoTo();
        }

        private static Texture2D GetFirstBackground(List<Texture2D> backgrounds) {
            return backgrounds[0];
        }

        public override void Update(GameTime gameTime) {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;
            elapsedTime += delta;

            while (elapsedTime >= TIME_STEP) {
                elapsedTime -= TIME_STEP;
                AdvanceBackground();
            }

            base.Update(gameTime);
        }

        private void AdvanceBackground() {
            if (backgroundIndex < backgrounds.Count) {
                background = backgrounds[backgroundIndex];
                backgroundIndex++;
            }
        }

        public bool ShouldGoToExit() {
            return backgroundIndex == backgrounds.Count();
        }
    }
}
