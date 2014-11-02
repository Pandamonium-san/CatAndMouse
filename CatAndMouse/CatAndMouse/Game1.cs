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
        ObjectManager objectManager;
        MapEditor mapEditor;
        Menu menu;

        public static Texture2D colorTexture;
        public static SpriteFont font, titleFont, hudFont;
        public static Random rnd = new Random();

        enum GameState { Title, Playing, GameOver, MapEditor, Paused }
        GameState gameState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
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
            font = Content.Load<SpriteFont>(@"font1");
            titleFont = Content.Load<SpriteFont>(@"titlefont");
            hudFont = Content.Load<SpriteFont>(@"hudfont");

            objectManager = new ObjectManager();
            menu = new Menu(Window);
            objectManager.LoadContent(Content);

            colorTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            colorTexture.SetData<Color>(new Color[] { Color.White });

            gameState = GameState.Title;
        }

        protected void StartMap(string path)
        {
            objectManager.Start(path);

            graphics.PreferredBackBufferWidth = objectManager.mapRec.Width;
            graphics.PreferredBackBufferHeight = objectManager.mapRec.Height;
            graphics.ApplyChanges();
            gameState = GameState.Playing;
        }

        protected void EditMap(string mapPath)
        {
            mapEditor = new MapEditor(25, 20);
            mapEditor.LoadMap(mapPath);
            graphics.PreferredBackBufferWidth = mapEditor.mapRec.Width;
            graphics.PreferredBackBufferHeight = mapEditor.mapRec.Height + 2 * 32;
            graphics.ApplyChanges();
            gameState = GameState.MapEditor;
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            KeyMouseReader.Update();
            menu.FindCenter(Window);
            switch(gameState)
            { 
                case GameState.Title:
                    menu.Update();
                    if (menu.play)
                    {
                        StartMap(menu.currentMap);
                        menu.play = false;
                    }
                    if(menu.editing)
                    {
                        EditMap(menu.currentMap);
                        menu.editing = false;
                    }
                    if (menu.exit.ButtonClicked() && menu.screen == Menu.Screen.title)
                        this.Exit();
                    break;
                case GameState.Playing:
                    objectManager.Update(gameTime);
                    if (objectManager.hud.pauseButton.ButtonClicked())
                    {
                        menu.LoadPauseScreen();
                        gameState = GameState.Paused;
                    }
                    if (objectManager.victory)
                    {
                        menu.LoadVictoryScreen();
                        gameState = GameState.Title;
                    }
                    if (objectManager.lose)
                    {
                        menu.LoadGameOverScreen();
                        gameState = GameState.Title;
                    }
                    break;
                case GameState.GameOver:
                    menu.Update();
                    if (menu.play)
                    {
                        StartMap(menu.currentMap);
                        menu.play = false;
                    }
                    break;
                case GameState.Paused:
                    menu.Update();
                    if (menu.playButton.ButtonClicked())
                        gameState = GameState.Playing;
                    if (menu.back.ButtonClicked())
                        gameState = GameState.Title;
                    break;
                case GameState.MapEditor:
                    mapEditor.Update(Window);
                    if (KeyMouseReader.KeyPressed(Keys.Enter) || mapEditor.hud.save.ButtonClicked())
                    {
                        MapHandler.SaveMapToText(mapEditor.tiles, menu.currentMap);
                        menu.Update();
                        menu.LoadTitleScreen();
                        gameState = GameState.Title;
                    }
                    break;
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            switch (gameState)
            {
                case GameState.Title:
                    menu.Draw(spriteBatch);
                    break;
                case GameState.Playing:
                    objectManager.Draw(spriteBatch);
                    break;
                case GameState.MapEditor:
                    mapEditor.Draw(spriteBatch);
                    break;
                case GameState.GameOver:
                    menu.Draw(spriteBatch);
                    break;
                case GameState.Paused:
                    objectManager.Draw(spriteBatch);
                    menu.Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
