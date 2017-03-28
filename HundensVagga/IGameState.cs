using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    interface IGameState {

        void Update(InputManager inputManager);

        void Draw(SpriteBatch spriteBatch);
    }
}
