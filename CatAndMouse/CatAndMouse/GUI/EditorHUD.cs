using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatAndMouse
{
    class EditorHUD
    {
        Rectangle rec;
        public static int hudHeight = 64;

        public List<EditorButton> buttons;
        EditorButton mouse, dumbCat, smartCat, intelligentCat, geniusCat, cheese, wall, floor, teleporter;
        public TextButton save;

        public EditorHUD(int windowX, int windowY)
        {
            rec = new Rectangle(0, windowY - hudHeight, windowX, hudHeight);

            buttons = new List<EditorButton>();
            buttons.Add (mouse           = new EditorButton(ObjectManager.mouseTexture,  new Rectangle(0, 0, 32, 32),        new Vector2(24, rec.Y + 32),    0));
            buttons.Add (dumbCat         = new EditorButton(ObjectManager.catTexture,    new Rectangle(0, 128, 32, 32),      new Vector2(56, rec.Y + 32),    1));
            buttons.Add (smartCat        = new EditorButton(ObjectManager.catTexture,    new Rectangle(0, 0, 32, 32),        new Vector2(88, rec.Y + 32),    2));
            buttons.Add (intelligentCat  = new EditorButton(ObjectManager.catTexture,    new Rectangle(128, 0, 32, 32),      new Vector2(120, rec.Y + 32),   3));
            buttons.Add (geniusCat       = new EditorButton(ObjectManager.catTexture,    new Rectangle(128, 128, 32, 32),    new Vector2(152, rec.Y + 32),   4));
            buttons.Add (cheese          = new EditorButton(ObjectManager.cheeseTexture, null,                               new Vector2(184, rec.Y + 32),   5));
            buttons.Add (wall            = new EditorButton(ObjectManager.tileTexture,   new Rectangle(0, 0, 32, 32),        new Vector2(216, rec.Y + 32),   6));
            buttons.Add (floor           = new EditorButton(ObjectManager.tileTexture,   new Rectangle(32, 0, 32, 32),       new Vector2(248, rec.Y + 32),   7));
            buttons.Add (teleporter      = new EditorButton(ObjectManager.tileTexture,   new Rectangle(64, 0, 32, 32),       new Vector2(280, rec.Y + 32),   8));

            save = new TextButton(Game1.font, new Vector2(rec.Width - 50, rec.Y + 32), "Save");
        }

        public void Update()
        {
            foreach (EditorButton b in buttons)
                b.Update();
            save.Update();
        }

        public void Draw(SpriteBatch spriteBatch, int placeTool)
        {
            spriteBatch.Draw(Game1.colorTexture, rec, Color.Black);
            foreach (EditorButton b in buttons)
                b.Draw(spriteBatch);
            spriteBatch.Draw(Game1.colorTexture, new Rectangle(24 + placeTool * 32 - 16, rec.Y + 32 - 16, 32, 32), Color.Blue * 0.3f);
            save.Draw(spriteBatch);

        }
    }
}
