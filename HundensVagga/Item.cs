using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    public class Item {
        private readonly string name;
        public string Name {
            get { return name; }
        }
        private readonly Texture2D texture;
        public Texture2D Texture {
            get { return texture; }
        }

        public Item(string name, Texture2D texture) {
            this.name = name;
            this.texture = texture;
        }
    }
}
