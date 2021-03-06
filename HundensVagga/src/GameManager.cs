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
    internal class GameManager {
        private ExitGameManager exitGameManager;

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

        private GameStateManager gameStateManager;
        public GameStateManager GameStateManager {
            get { return gameStateManager; }
        }

        private SaveGameManager saveGameManager;
        public SaveGameManager SaveGameManager {
            get { return saveGameManager; }
        }

        private MiscContent miscContent;

        public MiscContent MiscContent {
            get { return miscContent; }
        }

        private SubtitleManager subtitleManager;
        public SubtitleManager SubtitleManager {
            get { return subtitleManager; }
        }

        private SoundAndSubtitleManager soundAndSubtitleManager;
        public SoundAndSubtitleManager SoundAndSubtitleManager {
            get { return soundAndSubtitleManager; }
        }

        public GameManager(Main main) {
            exitGameManager = main.ExitGameManager;
            content = main.Content;
            cursorManager = main.CursorManager;
            rooms = main.Rooms;
            inventory = main.Inventory;
            songManager = main.SongManager;
            miscContent = main.MiscContent;
            subtitleManager = main.SubtitleManager;
            saveGameManager = main.SaveGameManager;
            gameStateManager = new GameStateManager();
            soundAndSubtitleManager = new SoundAndSubtitleManager(subtitleManager);

            gameStateManager.CurrentState = new ExploreState(this);
            GoToRoom(rooms.StartRoom);
        }

        public void Update(InputManager inputManager, GameTime gameTime) {
            soundAndSubtitleManager.Update(gameTime);
            cursorManager.SetToDefault();
            if (CurrentRoom.WithInventory)
                inventory.Update(inputManager);
            gameStateManager.CurrentState.Update(inputManager, gameTime);
            CurrentRoom.Update(gameTime);
            songManager.Update();
        }

        public void Draw(SpriteBatch spriteBatch) {
            CurrentRoom.Draw(spriteBatch);
            if (CurrentRoom.WithInventory)
                inventory.Draw(spriteBatch);
            gameStateManager.CurrentState.Draw(spriteBatch);
            subtitleManager.Draw(spriteBatch);
            cursorManager.Draw(spriteBatch);
        }

        public void GoToRoom(string roomName) {
            ChangeRoom(roomName);
            songManager.NewRoom(CurrentRoom);
        }

        public void ChangeRoom(string roomName) {
            if (IsSpecialRoomName(roomName))
                HandleSpecialRoomName(roomName);
            else
                HandleRoomChange(roomName);
        }

        private bool IsSpecialRoomName(string roomName) {
            return Enum.IsDefined(typeof(SpecialRoomNames), roomName);
        }

        private void HandleSpecialRoomName(string roomName) {
            SpecialRoomNames name =
                (SpecialRoomNames)Enum.Parse(typeof(SpecialRoomNames), roomName);
            switch (name) {
                case SpecialRoomNames.QUIT_GAME:
                    ExitGame();
                    return;
                case SpecialRoomNames.LOAD_GAME:
                    saveGameManager.LoadGame(this);
                    return;
                default:
                    throw new InvalidOperationException("Unknown special room name: " + roomName);
            }
        }

        private void HandleRoomChange(string roomName) {
            CurrentRoom = rooms.GetRoom(roomName);
            CurrentRoom.GoTo(this);
            if (CurrentRoom.HasSpecialState())
                gameStateManager.CurrentState = (IGameState)Activator.CreateInstance(
                    CurrentRoom.SpecialStateType, this);
            else
                gameStateManager.CurrentState = new ExploreState(this);
            gameStateManager.PushState();
        }

        public void ExitGame() {
            exitGameManager.Exit();
        }
    }
}
