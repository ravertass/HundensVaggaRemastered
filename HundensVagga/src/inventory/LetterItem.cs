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

        public LetterItem(string name, Texture2D icon, Texture2D letterImage) : base(name, icon) {
            this.letterImage = letterImage;
        }

        public override void PerformEffect() {
            // TODO
        }

        public override bool HasEffect() {
            return true;
        }
    }
}
