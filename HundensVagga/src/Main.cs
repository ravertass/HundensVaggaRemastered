using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace HundensVagga {
    /// <summary>
    /// The main type of the game. Initializes stuff, and calls Update() and Draw() repeatedly 
    /// (as defined by the MonoGame Game class). Mainly delegates work to the current state 
    /// (e.g. if we're in a menu or in the main game) in the stateManager.
    /// </summary>
    public class Main : Game {
        public const string CONTENT_DIR = "content";

        public const string BACKGROUND_DIR = "backgrounds";
        public const string INTERACTABLES_DIR = "interactables";
        public const string SONG_DIR = "songs";
        public const string VOICE_DIR = "voice";
        public const string CURSOR_DIR = "cursors";
        public const string MISC_DIR = "misc";

        public const string ROOMS_JSON_PATH = "rooms.json";

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rooms rooms;
        StateManager stateManager;
        InputManager inputManager;
        CursorManager cursorManager;

        public Main() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600; 
            graphics.ApplyChanges();
            Content.RootDirectory = CONTENT_DIR;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            rooms = new Rooms(CONTENT_DIR + Path.DirectorySeparatorChar + 
                ROOMS_JSON_PATH, Content);
            inputManager = new InputManager(this);

            base.Initialize();

            cursorManager = new CursorManager(Content, inputManager);
            stateManager = new StateManager();
            IGameState startState = new MainGameState(stateManager, Content, cursorManager, rooms);
            stateManager.CurrentState = startState;
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
            stateManager.CurrentState.Update(inputManager);
            // But always keep track of input
            inputManager.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // Delegate most draw work to the current game state
            // (e.g. if we're in a menu or in the main game)
            stateManager.CurrentState.Draw(spriteBatch);
            // But always draw the cursor (TODO: for now?)
            cursorManager.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
