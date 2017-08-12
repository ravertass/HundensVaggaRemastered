using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace HundensVagga {
    internal class LogoRoom : Room, ICutsceneRoom {
        private double time;

        private String exitRoomName;
        public String ExitRoomName {
            get { return exitRoomName; }
        }

        private IFadeBox fadeBox;

        private readonly IList<Texture2D> logos;

        private int currentLogoIndex;

        public LogoRoom(String name, Song song, float volume, IList<Texture2D> logos,
                String exitRoomName, double time, Type specialStateType, bool withInventory)
            : base(name, song, volume, null, new List<Exit>(), new List<Interactable>(),
                  specialStateType, withInventory) {
            this.exitRoomName = exitRoomName;
            this.time = time;
            this.logos = logos;
        }

        public override void GoTo(GameManager gameManager) {
            currentLogoIndex = -1;
            AdvanceLogoIndex();
            base.GoTo(gameManager);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            fadeBox.Update(gameTime);
            if (fadeBox.IsDone())
                if (fadeBox.GetType() == typeof(FadeInBox))
                    fadeBox = new FadeOutBox(time);
                else
                    AdvanceLogoIndex();
        }

        private void AdvanceLogoIndex() {
            currentLogoIndex++;
            fadeBox = new FadeInBox(time);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
            DrawLogo(spriteBatch);
            fadeBox.Draw(spriteBatch);
        }

        private void DrawLogo(SpriteBatch spriteBatch) {
            if (!OutOfLogos())
                spriteBatch.Draw(CurrentLogoTexture(), CurrentLogoPos(), Color.White);
        }

        private Texture2D CurrentLogoTexture() {
            return logos[currentLogoIndex];
        }

        private Vector2 CurrentLogoPos() {
            return new Vector2((Main.WINDOW_WIDTH  - CurrentLogoTexture().Width)  / 2,
                               (Main.WINDOW_HEIGHT - CurrentLogoTexture().Height) / 2);
        }

        public bool ShouldGoToExit() {
            return OutOfLogos();
        }

        private bool OutOfLogos() {
            return currentLogoIndex == logos.Count();
        }

        public void Stop() {
            // do nothing
        }
    }
}
