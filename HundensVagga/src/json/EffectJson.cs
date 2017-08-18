using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class EffectJson {
        private const bool DEFAULT_SET_VALUE = true;

        // World state variable set to DEFAULT_SET_VALUE
        // (it's possible to use vars below, but easier to just use var)
        [JsonProperty("var")]
        public string Var { get; set; }

        // World state variables set to defined values
        [JsonProperty("vars")]
        public IList<VarValJson> Vars { get; set; }

        // Sound effect that is played
        [JsonProperty("sound")]
        public string Sound { get; set; }

        // Item that is added to the inventory
        [JsonProperty("item")]
        public string ItemName { get; set; }

        // Item that is removed from the inventory
        [JsonProperty("remove_item")]
        public string RemoveItemName { get; set; }

        // Song to play
        [JsonProperty("song")]
        public string Song { get; set; }

        // Room to go to
        [JsonProperty("exit")]
        public string Exit { get; set; }

        public IEffect GetEffectInstance(ContentManager content, Subtitles subtitles,
                StateOfTheWorld worldState, Items items, Songs songs, SongManager songManager) {
            SoundAndSubtitle soundAndSubtitle = GetSoundAndSubtitle(content, subtitles);
            IList<VarVal> varVals = GetVarVals(worldState);
            IItem item = GetItem(items, ItemName);
            IItem removeItem = GetItem(items, RemoveItemName);
            Song song = GetSong(songs);

            return new Effect(varVals, soundAndSubtitle, item, removeItem, items.Inventory, song,
                songManager, Exit);
        }

        private Song GetSong(Songs songs) {
            return (Song != null) 
                   ? songs.GetSong(Song)
                   : null;
        }

        private SoundAndSubtitle GetSoundAndSubtitle(ContentManager content, Subtitles subtitles) {
            return (Sound != null)
                    ? new SoundAndSubtitle(
                        content.Load<SoundEffect>(Main.SOUND_EFFECTS_DIR
                            + Path.DirectorySeparatorChar + Sound),
                        subtitles.GetSubtitle(Sound))
                    : null;
        }

        private IList<VarVal> GetVarVals(StateOfTheWorld worldState) {
            IList<VarVal> varVals = new List<VarVal>();
            if (Vars != null)
                foreach (VarValJson varValJson in Vars)
                    varVals.Add(varValJson.GetVarValInstance(worldState));
            if (Var != null)
                varVals.Add(new VarVal(worldState.Get(Var), DEFAULT_SET_VALUE));

            return varVals;
        }

        private IItem GetItem(Items items, string itemName) {
            if (itemName != null)
                return items.GetItem(itemName);
            else
                return null;
        }
    }
}
