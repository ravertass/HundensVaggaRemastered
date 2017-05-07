using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    internal interface IItem {
        Vector2 Coords { get; set; }
        string Name { get; }
        Rectangle Rectangle { get; }
        Texture2D Texture { get; }

        bool HasEffect();
        void PerformEffect();
        IInGameState GetItemState(MainGameState mainGameState);
    }
}