﻿using Microsoft.Xna.Framework;
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
    internal class Room {
        private readonly string name;
        public string Name {
            get { return name; }
        }

        private readonly Song song;
        public Song Song {
            get { return song; }
        }
        private readonly float volume;
        public float Volume {
            get { return volume; }
        }

        private readonly Type specialStateType;
        public Type SpecialStateType {
            get { return specialStateType; }
        }

        private readonly Texture2D background;
        private readonly List<Exit> exits;
        private readonly List<Interactable> interactables;

        public Room(string name, Song song, float volume, Texture2D background, List<Exit> exits, 
                List<Interactable> interactables, Type specialStateType) {
            this.name = name;
            this.song = song;
            this.volume = volume;
            this.background = background;
            this.exits = exits;
            this.interactables = interactables;
            this.specialStateType = specialStateType;
        }

        private List<Interactable> ActiveInteractables() {
            return (from interactable 
                    in interactables
                    where interactable.IsActive()
                    select interactable).ToList<Interactable>();
        }

        private List<Exit> ActiveExits() {
            return (from exit
                    in exits
                    where exit.IsActive()
                    select exit).ToList<Exit>();
        }

        public Interactable GetInteractableAt(Vector2 coords) {
            foreach (Interactable interactable in ActiveInteractables())
                if (interactable.Rectangle.Contains(coords) && interactable.IsInteractive())
                    return interactable;

            return null;
        }

        public Exit GetExitAt(Vector2 coords) {
            foreach (Exit exit in ActiveExits())
                if (exit.Rectangle.Contains(coords))
                    return exit;

            return null;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(background, new Vector2(0f, 0f), Color.White);
            DrawInteractables(spriteBatch);
        }

        private void DrawInteractables(SpriteBatch spriteBatch) {
            foreach (Interactable interactable in ActiveInteractables())
                if (interactable.IsActive())
                    interactable.Draw(spriteBatch);
        }

        public bool HasSpecialState() {
            return specialStateType != null;
        }
    }
}
