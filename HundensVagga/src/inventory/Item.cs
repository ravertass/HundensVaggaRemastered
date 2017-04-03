using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    /// <summary>
    /// An item in the inventory.
    /// </summary>
    public class Item {
        private readonly string name;
        public string Name {
            get { return name; }
        }
        private readonly Texture2D icon;
        public Texture2D Texture {
            get { return icon; }
        }
        public Vector2 Coords { get; set; }

        public Rectangle Rectangle {
            get {
                return new Rectangle((int)Coords.X, (int)Coords.Y,
                    Texture.Width, Texture.Height);
            }
        }

        public Item(string name, Texture2D icon) {
            this.name = name;
            this.icon = icon;
            Coords = new Vector2(0, 0);
        }
    }
}
