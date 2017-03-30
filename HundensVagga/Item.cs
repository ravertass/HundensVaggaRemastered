using Microsoft.Xna.Framework;
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
        public Vector2 Coords { get; set; }

        public Rectangle Rectangle {
            get {
                return new Rectangle((int)Coords.X, (int)Coords.Y,
                    Texture.Width, Texture.Height);
            }
        }

        public Item(string name, Texture2D texture) {
            this.name = name;
            this.texture = texture;
            Coords = new Vector2(0, 0);
        }
    }
}
