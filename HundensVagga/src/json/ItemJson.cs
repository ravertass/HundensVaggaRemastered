using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class ItemJson {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public string IconPath { get; set; }

        public Item GetItemInstance(ContentManager content) {
            return new Item(Name, GetIcon(content));
        }

        private Texture2D GetIcon(ContentManager content) {
            return content.Load<Texture2D>(Main.INVENTORY_DIR +
                Path.DirectorySeparatorChar + IconPath);
        }
    }
}
