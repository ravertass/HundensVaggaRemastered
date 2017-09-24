using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace HundensVagga {
    internal class TelephoneInteractable : Interactable {
        private int number;
        public int Number {
            get { return number; }
        }

        public TelephoneInteractable(Rectangle rectangle, SoundAndSubtitle lookSoundAndSubtitle,
                    IEffect useEffect, IEffect clickEffect, IEffect hoverEffect,
                    IDictionary<string, IEffect> itemEffects, IList<VarVal> prereqs, int number,
                    Texture2D texture = null)
                : base(rectangle, lookSoundAndSubtitle, useEffect, clickEffect, hoverEffect,
                      itemEffects, prereqs, texture) {
            this.number = number;
        }
    }
}
