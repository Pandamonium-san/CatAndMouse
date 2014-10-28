using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CatAndMouse
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MapEditor mapEditor;

        enum GameState { Title, Playing, GameOver, Options, MapEditor }
        GameState gameState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            this.Window.Title = "something";
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ObjectManager.LoadContent(Content);
            ObjectManager.Initialize();
            graphics.PreferredBackBufferWidth = (ObjectManager.mapData[0].Length-4) * 32;
            graphics.PreferredBackBufferHeight = (ObjectManager.mapData.Count-4) * 32;
            graphics.ApplyChanges();

            gameState = GameState.MapEditor;
            StartMapEditor(25,25);

        }

        protected override void UnloadContent()
        {
        }

        protected void StartMapEditor(int x, int y)
        {
            mapEditor = new MapEditor(x,y);

            graphics.PreferredBackBufferWidth = (x-4) * 32;
            graphics.PreferredBackBufferHeight = (y-4) * 32;
            graphics.ApplyChanges();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            KeyMouseReader.Update();
            switch(gameState)
            { 
                case GameState.Title:

                    break;
                case GameState.Playing:
                    ObjectManager.Update();
                    break;
                case GameState.MapEditor:
                    if (KeyMouseReader.KeyPressed(Keys.Enter))
                    {
                        MapHandler.SaveMapToText(mapEditor.tiles);
                        ObjectManager.Initialize();
                        gameState = GameState.Playing;
                    }
                    mapEditor.Update();
                    break;
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            switch (gameState)
            {
                case GameState.Title:

                    break;
                case GameState.Playing:
                    ObjectManager.Draw(spriteBatch);
                    break;
                case GameState.MapEditor:
                    mapEditor.Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
