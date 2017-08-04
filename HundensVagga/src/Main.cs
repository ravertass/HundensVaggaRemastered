﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System;

namespace HundensVagga {
    /// <summary>
    /// The main type of the game. Initializes stuff, and calls Update() and Draw() repeatedly 
    /// (as defined by the MonoGame Game class). Mainly delegates work to the current state 
    /// (e.g. if we're in a menu or in the main game) in the stateManager.
    /// </summary>
    public class Main : Game {
        public const string CONTENT_DIR = "content";

        public const string BACKGROUNDS_DIR = "backgrounds";
        public const string INVENTORY_DIR = "inventory";
        public const string INTERACTABLES_DIR = "interactables";
        public const string SONGS_DIR = "songs";
        public const string VOICE_DIR = "voice";
        public const string SOUND_EFFECTS_DIR = "sound_effects";
        public const string CURSORS_DIR = "cursors";
        public const string MISC_DIR = "misc";
        public const string LETTERS_DIR = "letters";
        public const string DIALOG_DIR = "dialog";

        public const string ROOMS_JSON_PATH = "rooms.json";
        public const string ITEMS_JSON_PATH = "items.json";
        public const string MISC_CONTENT_JSON_PATH = "misc.json";

        public const int WINDOW_WIDTH = 800;
        public const int WINDOW_HEIGHT = 600;

        private int fullScreenWidth;
        private int fullScreenHeight;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Rooms rooms;
        internal Rooms Rooms {
            get { return rooms; }
        }

        private Items items;
        internal Items Items {
            get { return items; }
        }

        private Inventory inventory;
        internal Inventory Inventory {
            get { return inventory; }
        }

        private StateManager stateManager;
        internal StateManager StateManager {
            get { return stateManager; }
        }

        private ExitGameManager exitGameManager;
        internal ExitGameManager ExitGameManager {
            get { return exitGameManager; }
        }

        private InputManager inputManager;
        internal InputManager InputManager {
            get { return inputManager; }
        }

        private CursorManager cursorManager;
        internal CursorManager CursorManager {
            get { return cursorManager; }
        }

        private SongManager songManager;
        internal SongManager SongManager {
            get { return songManager; }
        }

        private MiscContent miscContent;
        internal MiscContent MiscContent {
            get { return miscContent; }
        }

        private RenderTarget2D renderTarget;
        private Rectangle renderTargetRect;

        public Main() {
            graphics = new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = WINDOW_WIDTH,
                PreferredBackBufferHeight = WINDOW_HEIGHT
            };
            renderTargetRect = RenderTargetWindowRect();
            Content.RootDirectory = CONTENT_DIR;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            base.Initialize();

            renderTarget = new RenderTarget2D(GraphicsDevice, WINDOW_WIDTH, WINDOW_HEIGHT);
            SetFullScreenResolution();
            SetToFullScreen();

            inputManager = new InputManager(this);
            cursorManager = new CursorManager(Content, inputManager);

            exitGameManager = new ExitGameManager(this);
            stateManager = new StateManager(this);

            stateManager.GoToStartState();
        }

        private void SetFullScreenResolution() {
            fullScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            fullScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content...
        /// 
        /// ... according to prewritten MonoGame comments. I have not put any content
        /// loading here, and I don't know if I should. TODO!
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            miscContent = new MiscContent(CONTENT_DIR + Path.DirectorySeparatorChar +
                MISC_CONTENT_JSON_PATH, Content);

            inventory = new Inventory(miscContent);
            items = new Items(CONTENT_DIR + Path.DirectorySeparatorChar +
                ITEMS_JSON_PATH, Content, inventory);

            songManager = new SongManager();
            rooms = new Rooms(CONTENT_DIR + Path.DirectorySeparatorChar +
                ROOMS_JSON_PATH, Content, items, songManager);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here (if important?)
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Delegate most update work to the current game state
            // (e.g. if we're in a menu or in the main game)
            stateManager.UpdateCurrentState(inputManager, gameTime);
            // But always keep track of input
            inputManager.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            DrawToRenderTarget();

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw((Texture2D)renderTarget, renderTargetRect, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawToRenderTarget() {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            // Delegate most draw work to the current game state
            // (e.g. if we're in a menu or in the main game)
            stateManager.DrawCurrentState(spriteBatch);
            // But always draw the cursor (TODO: for now?)
            cursorManager.Draw(spriteBatch);

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
        }

        public void ToggleFullScreen() {
            if (!Window.IsBorderless) 
                SetToFullScreen();
            else
                SetToWindowed();
        }

        private void SetToFullScreen() {
            renderTargetRect = RenderTargetFullScreenRect();
            graphics.PreferredBackBufferWidth = fullScreenWidth;
            graphics.PreferredBackBufferHeight = fullScreenHeight;
            Window.IsBorderless = true;
            Window.Position = new Point(0, 0);

            graphics.ApplyChanges();
        }

        private void SetToWindowed() {
            renderTargetRect = RenderTargetWindowRect();
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            Window.IsBorderless = false;
            Window.Position = new Point((fullScreenWidth - WINDOW_WIDTH) / 2,
                                        (fullScreenHeight - WINDOW_HEIGHT) / 2);

            graphics.ApplyChanges();
        }

        private Rectangle RenderTargetFullScreenRect() {
            int newWindowWidth = (int)Math.Ceiling(
                ((float)fullScreenHeight / (float)WINDOW_HEIGHT) * WINDOW_WIDTH);

            return new Rectangle((fullScreenWidth - newWindowWidth) / 2,
                0, newWindowWidth, fullScreenHeight);
        }

        private Rectangle RenderTargetWindowRect() {
            return new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);
        }
    }
}
