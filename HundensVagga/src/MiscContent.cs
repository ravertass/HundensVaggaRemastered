using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
    internal class MiscContent {
        private Texture2D inventoryBagImage;
        public Texture2D InventoryBagImage {
            get { return inventoryBagImage; }
        }
        private Texture2D inventoryBackgroundImage;
        public Texture2D InventoryBackgroundImage {
            get { return inventoryBackgroundImage; }
        }
        private Texture2D exitIconImage;
        public Texture2D ExitIconImage {
            get { return exitIconImage; }
        }

        private Texture2D exitMenuImage;
        public Texture2D ExitMenuImage {
            get { return exitMenuImage; }
        }
        private Rectangle exitMenuYesRect;
        public Rectangle ExitMenuYesRect {
            get { return exitMenuYesRect; }
        }
        private Rectangle exitMenuNoRect;
        public Rectangle ExitMenuNoRect {
            get { return exitMenuNoRect; }
        }

        private SoundEffectInstance itemFailSound;
        public SoundEffectInstance ItemFailSound {
            get { return itemFailSound; }
        }

        public MiscContent(string jsonFilePath, ContentManager content) {
            MiscContentJson json = DeserializeJson(jsonFilePath);
            InitializeData(json, content);
        }

        private static MiscContentJson DeserializeJson(string path) {
            string jsonString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<MiscContentJson>(jsonString);
        }

        private void InitializeData(MiscContentJson json, ContentManager content) {
            inventoryBagImage = json.GetInventoryBagImage(content);
            inventoryBackgroundImage = json.GetInventoryBackgroundImage(content);
            exitIconImage = json.GetExitIconImage(content);

            exitMenuImage = json.GetExitMenuImage(content);
            exitMenuYesRect = json.ExitMenuYesRect.GetInstance();
            exitMenuNoRect = json.ExitMenuNoRect.GetInstance();

            itemFailSound = json.GetItemFailSound(content);
        }
    }
}
