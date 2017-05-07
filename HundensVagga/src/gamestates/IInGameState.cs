using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    /// <summary>
    /// An in-game state (e.g. using inventory, exploring) that MainGameState delegates work to.
    /// </summary>
    public interface IInGameState {
        void Update(InputManager inputManager, GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
