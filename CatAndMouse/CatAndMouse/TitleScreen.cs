using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CatAndMouse
{
    class TitleScreen
    {
        public List<Button> buttons;
        public Button play,mapEditor,exit;
        public TitleScreen(GameWindow window)
        {
            float centerX = window.ClientBounds.Width / 2;
            float centerY = window.ClientBounds.Height / 2;
            buttons = new List<Button>();
            play = new Button(Game1.font, new Vector2(centerX, centerY+50),"PLAY", 1f);
            mapEditor = new Button(Game1.font, new Vector2(centerX, centerY+100), "MAP EDITOR", 1f);
            exit = new Button(Game1.font, new Vector2(centerX, centerY+150), "EXIT", 1f);
            buttons.Add(play);
            buttons.Add(mapEditor);
            buttons.Add(exit);
        }

        public void Update()
        {
            foreach (Button button in buttons)
                button.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Button button in buttons)
                button.Draw(spriteBatch);
        }
    }
}
