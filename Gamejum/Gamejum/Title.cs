using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Gamejum
{
    class Title
    {
        public bool isTitle = false;
        public Texture2D title;
        public Vector2 position = Vector2.Zero;
        public int count;
        public int pushcount;
        public float alpha;
        public int alphacount;

        public void Update(GameTime gameTime)
        {
            pushcount++;
            count++;
            switch (alphacount)
            {
                case 0:
                    if (count > 5)
                    {
                        alpha += 0.1f;
                        count = 0;
                        if (alpha > 1)
                        {
                            alpha = 1;
                            alphacount++;
                        }
                    }
                    break;
                case 1:
                    if (count > 5)
                    {
                        alpha -= 0.1f;
                        count = 0;
                        if (alpha < 0)
                        {
                            alpha = 0;
                            alphacount = 0;
                        }
                    }
                    break;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (pushcount >= 30)
                {
                    isTitle = true;
                }
            }

        }
        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(title, position, Color.White * alpha);
        }

    }
}
