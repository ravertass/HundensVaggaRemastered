using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    /// <summary>
    /// Contains most of the game logic. Used to handle different states (e.g. the main menu, or in-game).
    /// </summary>
    public interface IGameState {
        void Update(InputManager inputManager);

        void Draw(SpriteBatch spriteBatch);
    }
}
