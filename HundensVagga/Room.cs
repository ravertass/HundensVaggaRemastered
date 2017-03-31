using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    /// <summary>
    /// An in-game room.
    /// </summary>
    public class Room {
        private readonly string name;
        public string Name {
            get { return this.name; }
        }
        private readonly Song song;
        private readonly Texture2D background;
        public Texture2D Background {
            get { return this.background; }
        }
        private readonly List<Exit> exits;
        private readonly List<Interactable> interactables;

        public Room(string name, Song song, Texture2D background, List<Exit> exits, List<Interactable> interactables) {
            this.name = name;
            this.song = song;
            this.background = background;
            this.exits = exits;
            this.interactables = interactables;
        }

        public Interactable GetInteractableAt(Vector2 coords) {
            foreach (Interactable interactable in interactables)
                if (interactable.Rectangle.Contains(coords))
                    return interactable;

            return null;
        }

        public Exit GetExitAt(Vector2 coords) {
            foreach (Exit exit in exits)
                if (exit.Rectangle.Contains(coords))
                    return exit;

            return null;
        }
    }
}
