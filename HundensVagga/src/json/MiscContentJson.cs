using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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

        [JsonProperty("item_fail_sound")]
        public string ItemFailSoundPath { get; set; }

        [JsonProperty("company_logos")]
        public IList<string> CompanyLogoPaths { get; set; }
        [JsonProperty("main_menu_start")]
        public RectJson MainMenuStartRect { get; set; }
        [JsonProperty("main_menu_exit")]
        public RectJson MainMenuExitRect { get; set; }
        [JsonProperty("main_menu_song")]
        public string MainMenuSongPath { get; set; }

        [JsonProperty("main_menu_background")]
        public string MainMenuBackgroundImagePath { get; set; }

        [JsonProperty("intro_song")]
        public string IntroSongPath { get; set; }
        [JsonProperty("intro_monologue")]
        public string IntroMonologuePath { get; set; }
        [JsonProperty("intro_piano_sound")]
        public string IntroPianoSoundPath { get; set; }

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

        public Song GetMainMenuSong(ContentManager content) {
            return GetSong(content, MainMenuSongPath);
        }

        public Song GetIntroSong(ContentManager content) {
            return GetSong(content, IntroSongPath);
        }

        public SoundEffectInstance GetIntroMonologue(ContentManager content) {
            return GetSoundEffect(content, IntroMonologuePath);
        }

        public SoundEffectInstance GetIntroPianoSound(ContentManager content) {
            return GetSoundEffect(content, IntroPianoSoundPath);
        }

        public SoundEffectInstance GetItemFailSound(ContentManager content) {
            return GetSoundEffect(content, ItemFailSoundPath);
        }

        public IList<Texture2D> GetCompanyLogos(ContentManager content) {
            IList<Texture2D> companyLogos = new List<Texture2D>();
            foreach (string companyLogoPath in CompanyLogoPaths)
                companyLogos.Add(GetImage(content, companyLogoPath));

            return companyLogos;
        }

        public Texture2D GetMainMenuBackgroundImage(ContentManager content) {
            return GetImage(content, MainMenuBackgroundImagePath);
        }

        private Texture2D GetImage(ContentManager content, string imagePath) {
            return content.Load<Texture2D>(Main.MISC_DIR + Path.DirectorySeparatorChar + imagePath);
        }

        private SoundEffectInstance GetSoundEffect(ContentManager content, string soundPath) {
            return content.Load<SoundEffect>(Main.SOUND_EFFECTS_DIR + 
                Path.DirectorySeparatorChar + soundPath).CreateInstance();
        }

        private Song GetSong(ContentManager content, string songPath) {
            return content.Load<Song>(Main.SONGS_DIR + Path.DirectorySeparatorChar + songPath);
        }
    }
}
