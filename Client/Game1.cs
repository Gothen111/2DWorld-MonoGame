#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using GameLibrary.Setting;
using Utility.Gui;
using GameLibrary.Configuration;
#endregion

namespace Client
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private FrameCounter frameCounter = new FrameCounter();

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = Setting.resolutionX;
            graphics.PreferredBackBufferHeight = Setting.resolutionY;

            this.IsMouseVisible = true;

            /*Configuration.isHost = false;
            Configuration.commandManager = new ClientCommandManager();
            Configuration.networkManager = new ClientNetworkManager();*/

            Setting.logInstance = "Log/ClientLog-" + DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString().Replace(":", ".") + ".txt";

            //Configuration.networkManager.client = new GameLibrary.Connection.Client();

            Configuration.gameManager = new ClientGameManager();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GameLibrary.Camera.Camera.camera = new GameLibrary.Camera.Camera(GraphicsDevice.Viewport);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameLibrary.Ressourcen.RessourcenManager.ressourcenManager.loadRessources(Content);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                graphics.ToggleFullScreen();
            }

            GameLibrary.Player.PlayerContoller.playerContoller.update();
            if (this.IsActive)
            {
                Input.Keyboard.KeyboardManager.keyboardManager.update();
                Input.Mouse.MouseManager.mouseManager.update();
            }
            if (GameLibrary.Map.World.World.world != null)
            {
                GameLibrary.Map.World.World.world.update(gameTime);
            }

            GameLibrary.Camera.Camera.camera.update(gameTime);

            GameLibrary.Gui.MenuManager.menuManager.ActiveContainer.update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GameLibrary.Gui.MenuManager.menuManager.ActiveContainer.draw(GraphicsDevice, spriteBatch);

            spriteBatch.Begin();
            if (gameTime.ElapsedGameTime.Milliseconds > 0)
            {
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                frameCounter.Update(deltaTime);

                var fps = string.Format("FPS: {0}", frameCounter.AverageFramesPerSecond);

                spriteBatch.DrawString(GameLibrary.Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], fps, new Vector2(100, 0), Color.White);

                spriteBatch.DrawString(GameLibrary.Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"], "FPS:" + (1000 / gameTime.ElapsedGameTime.Milliseconds), new Vector2(0, 0), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void OnExiting(Object sender, EventArgs args)
        {
            base.OnExiting(sender, args);
            Environment.Exit(Environment.ExitCode);
        }
    }
}
