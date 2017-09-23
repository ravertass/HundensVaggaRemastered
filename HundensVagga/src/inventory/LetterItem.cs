using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    internal class LetterItem : Item, IItem {
        private readonly Texture2D letterImage;
        private readonly string letterText;

        public LetterItem(string name, Texture2D icon, Texture2D letterImage, string letterText)
                : base(name, icon) {
            this.letterImage = letterImage;
            this.letterText = letterText;
        }
        
        public override bool HasEffect() {
            return true;
        }

        public override IGameState GetItemState(GameManager mainGameState) {
            return new LetterItemState(mainGameState, letterImage, letterText);
        }
    }
}
