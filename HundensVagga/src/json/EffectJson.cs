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

        public IEffect GetEffectInstance(ContentManager content, Assets assets,
                StateOfTheWorld worldState, SongManager songManager) {
            SoundAndSubtitle soundAndSubtitle = GetSoundAndSubtitle(content, assets);
            IList<VarVal> varVals = GetVarVals(worldState);
            IItem item = GetItem(assets.Items, ItemName);
            IItem removeItem = GetItem(assets.Items, RemoveItemName);
            Song song = GetSong(assets.Songs);

            return new Effect(varVals, soundAndSubtitle, item, removeItem, assets.Items.Inventory,
                song, songManager, Exit);
        }

        private Song GetSong(Songs songs) {
            return (Song != null) 
                   ? songs.GetSong(Song)
                   : null;
        }

        private SoundAndSubtitle GetSoundAndSubtitle(ContentManager content, Assets assets) {
            return (Sound != null)
                    ? assets.GetSoundAndSubtitle(content, Sound)
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
            return (itemName != null)
                   ? items.GetItem(itemName)
                   : null;
        }
    }
}
