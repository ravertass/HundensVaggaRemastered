using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace HundensVagga {
    internal interface IRoom {
        string Name { get; }
        Song Song { get; }
        Type SpecialStateType { get; }
        float Volume { get; }

        void Draw(SpriteBatch spriteBatch);
        Exit GetExitAt(Vector2 coords);
        Interactable GetInteractableAt(Vector2 coords);
        void GoTo();
        bool HasSpecialState();
        void Update(GameTime gameTime);
    }
}