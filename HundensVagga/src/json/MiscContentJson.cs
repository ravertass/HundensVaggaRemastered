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
    internal class MiscContentJson {
        [JsonProperty("inventory_bag_image")]
        public string InventoryBagImagePath { get; set; }
        [JsonProperty("inventory_background_image")]
        public string InventoryBackgroundImagePath { get; set; }
        [JsonProperty("exit_icon_image")]
        public string ExitIconImagePath { get; set; }

        [JsonProperty("exit_menu_image")]
        public string ExitMenuImagePath { get; set; }
        [JsonProperty("exit_menu_yes")]
        public RectJson ExitMenuYesRect { get; set; }
        [JsonProperty("exit_menu_no")]
        public RectJson ExitMenuNoRect { get; set; }

        public Texture2D GetInventoryBagImage(ContentManager content) {
            return GetImage(content, InventoryBagImagePath);
        }

        public Texture2D GetInventoryBackgroundImage(ContentManager content) {
            return GetImage(content, InventoryBackgroundImagePath);
        }

        public Texture2D GetExitIconImage(ContentManager content) {
            return GetImage(content, ExitIconImagePath);
        }

        public Texture2D GetExitMenuImage(ContentManager content) {
            return GetImage(content, ExitMenuImagePath);
        }

        private Texture2D GetImage(ContentManager content, string imagePath) {
            return content.Load<Texture2D>(Main.MISC_DIR + Path.DirectorySeparatorChar + imagePath);
        }
    }
}
