using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatAndMouse
{
    class HUD
    {
        Rectangle rec;
        public static int hudHeight = 64;

        public Button pauseButton;
        HUDObject cheeseEaten, remainingCheese, playerLives;

        /// <summary>
        /// Displays useful info to the player such as remaining lives and remaining cheese.
        /// </summary>
        /// <param name="windowX">Width of game window</param>
        /// <param name="windowY">Height of game window</param>
        public HUD(int windowX, int windowY)
        {
            rec = new Rectangle(0, windowY - hudHeight, windowX, hudHeight);
            pauseButton = new TextButton(Game1.hudFont, new Vector2(windowX - 64, windowY - 48), "PAUSE");

            cheeseEaten = new HUDObject(new Vector2(8, windowY - 55), "Cheese eaten ", false);
            remainingCheese = new HUDObject(new Vector2(windowX - 32, windowY - 32), "Remaining cheese ", true);
            playerLives = new HUDObject(new Vector2(8, windowY - 24), "Lives ", false);
        }

        public void Update(int cheeseEaten, int playerLives, int remainingCheese)
        {
            pauseButton.Update();
            this.cheeseEaten.value = cheeseEaten;
            this.playerLives.value = playerLives;
            this.remainingCheese.value = remainingCheese;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.colorTexture, rec, Color.Black);
            for (int i = 0; i < playerLives.value; i++)
            {
                spriteBatch.Draw(ObjectManager.mouseTexture, playerLives.pos + new Vector2(50 + i * 20, -5), new Rectangle(0, 0, 32, 32), Color.White);
            }
            spriteBatch.DrawString(Game1.hudFont, "Lives", playerLives.pos, Color.White);

            cheeseEaten.Draw(spriteBatch);
            remainingCheese.Draw(spriteBatch);
            pauseButton.Draw(spriteBatch);

        }
    }
}
