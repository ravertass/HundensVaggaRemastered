using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace HundensVagga {
    /// <summary>
    /// For knowing which direction the cursor should point when hovering over an exit.
    /// </summary>
    internal enum Direction {
        up,
        down,
        left,
        right
    }

    /// <summary>
    /// An exit in a room.
    /// </summary>
    internal class Exit {
        private readonly Rectangle rectangle;
        public Rectangle Rectangle {
            get { return rectangle; }
        }
        private readonly string roomName;
        public string RoomName {
            get { return roomName; }
        }
        private readonly Direction direction;
        private IList<VarVal> prereqs;

        public Direction Direction {
            get { return direction; }
        }

        private SoundEffectInstance soundEffect;
        private VarVal varVal;

        public Exit(Rectangle rectangle, string room, Direction direction, IList<VarVal> prereqs,
                SoundEffectInstance soundEffect, VarVal varVal) {
            this.rectangle = rectangle;
            this.roomName = room;
            this.direction = direction;
            this.prereqs = prereqs;
            this.soundEffect = soundEffect;
            this.varVal = varVal;
        }

        public bool IsActive() {
            foreach (VarVal prereq in prereqs)
                if (!prereq.IsMet())
                    return false;
            return true;
        }

        public void DoEffects() {
            PlaySound();
            SetVarVal();
        }

        private void PlaySound() {
            if (soundEffect != null)
                soundEffect.Play();
        }

        private void SetVarVal() {
            if (varVal != null)
                varVal.Set();
        }
    }
}