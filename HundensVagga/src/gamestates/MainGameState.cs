﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace HundensVagga {
    /// <summary>
    /// The main class for handling everything in-game (e.g. not in the start menu, credits etc.).
    /// This class also delegates to a current in-game state, which controls if we're using the 
    /// inventory, exploring rooms, etc.
    /// </summary>
    internal class MainGameState : IGameState {
        private const string START_ROOM_NAME = "front";
        public SoundEffectInstance CurrentPlayingLookSound { get; set; }


        private ExitGameManager exitGameManager;
        private StateManager stateManager;

        private ContentManager content;
        public ContentManager Content {
            get { return content; }
        }

        private CursorManager cursorManager;
        public CursorManager CursorManager {
            get { return cursorManager; }
        }

        private readonly Rooms rooms;
        public Room CurrentRoom { get; set; }
        private SongManager songManager;

        private Inventory inventory;
        public Inventory Inventory {
            get { return inventory; }
        }

        private InGameStateManager inGameStateManager;
        public InGameStateManager InGameStateManager {
            get { return inGameStateManager; }
        }

        private MiscContent miscContent;

        public MiscContent MiscContent {
            get { return miscContent; }
        }

        public MainGameState(StateManager stateManager, Main main) {
            this.stateManager = stateManager;
            exitGameManager = main.ExitGameManager;
            content = main.Content;
            cursorManager = main.CursorManager;
            rooms = main.Rooms;
            inventory = main.Inventory;
            songManager = main.SongManager;
            miscContent = main.MiscContent;
            inGameStateManager = new InGameStateManager();

            GoToRoom(START_ROOM_NAME);

            inventory.AddItem(main.Items.GetItem("letter"));
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            UpdateCurrentPlayingLookSound();
            cursorManager.SetToDefault();
            inventory.Update(inputManager);
            inGameStateManager.CurrentState.Update(inputManager, gameTime);
            CurrentRoom.Update(gameTime);
            songManager.Update();
        }

        private void UpdateCurrentPlayingLookSound() {
            if (CurrentPlayingLookSound != null
                && CurrentPlayingLookSound.State == SoundState.Stopped)
                CurrentPlayingLookSound = null;
        }

        public void Draw(SpriteBatch spriteBatch) {
            CurrentRoom.Draw(spriteBatch);
            inventory.Draw(spriteBatch);
            inGameStateManager.CurrentState.Draw(spriteBatch);
        }

        public void GoToRoom(string roomName) {
            ChangeRoom(roomName);
            songManager.NewRoom(CurrentRoom);
        }

        private void ChangeRoom(string roomName) {
            CurrentRoom = rooms.GetRoom(roomName);
            CurrentRoom.GoTo();
            if (CurrentRoom.HasSpecialState())
                inGameStateManager.CurrentState = (IInGameState)Activator.CreateInstance(
                    CurrentRoom.SpecialStateType, this);
            else
                inGameStateManager.CurrentState = new ExploreState(this);
            inGameStateManager.PushState();
        }

        public void ExitGame() {
            exitGameManager.Exit();
        }

        public void Win() {
            // TODO go to win state
            Console.WriteLine("Tired of winning");
        }
    }
}
