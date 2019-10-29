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
        public Texture2D title1, title2, title3, title4, title5, title6, title7, title8, title9, title10, title11, title12, title13, title14, title15;
        public Texture2D space;
        public Vector2 position = Vector2.Zero;
        public int count;
        public int pushcount;
        public int animationNumber;
        public float counter;
        public float alpha;
        public int alphacount;

        public void Update(GameTime gameTime)
        {
            pushcount++;
            count++;
            switch (alphacount)
            {
                case 0:
                    if (count > 3)
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
                    if (count > 3)
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

            counter++;
            if (counter >= 10)
            {
                animationNumber++;
                counter = 0;
                if (animationNumber >= 15)
                {
                    animationNumber = 1;
                }
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
            if (animationNumber == 1)
            {
                sprite.Draw(title1, new Vector2(0, 0), Color.White);
            }
            if (animationNumber == 2)
            {
                sprite.Draw(title2, new Vector2(0, 0), Color.White);
            }
            if (animationNumber == 3)
            {
                sprite.Draw(title3, new Vector2(0, 0), Color.White);
            }
            if (animationNumber == 4)
            {
                sprite.Draw(title4, new Vector2(0, 0), Color.White);
            }
            if (animationNumber == 5)
            {
                sprite.Draw(title5, new Vector2(0, 0), Color.White);
            }
            if (animationNumber == 6)
            {
                sprite.Draw(title6, new Vector2(0, 0), Color.White);
            }
            if (animationNumber == 7)
            {
                sprite.Draw(title7, new Vector2(0, 0), Color.White);
            }
            if (animationNumber == 8)
            {
                sprite.Draw(title8, new Vector2(0, 0), Color.White);
            }
            if (animationNumber == 9)
            {
                sprite.Draw(title9, new Vector2(0, 0), Color.White);
            }
            if (animationNumber == 10)
            {
                sprite.Draw(title10, new Vector2(0, 0), Color.White);
            }
            if (animationNumber == 11)
            {
                sprite.Draw(title11, new Vector2(0, 0), Color.White);
            }
            if (animationNumber == 12)
            {
                sprite.Draw(title12, new Vector2(0, 0), Color.White);
            }
            if (animationNumber == 13)
            {
                sprite.Draw(title13, new Vector2(0, 0), Color.White);
            }
            if (animationNumber == 14)
            {
                sprite.Draw(title14, new Vector2(0, 0), Color.White);
            }
            if (animationNumber == 15)
            {
                sprite.Draw(title15, new Vector2(0, 0), Color.White);
            }

            sprite.Draw(space, Vector2.Zero, Color.White*alpha);
        }

    }
}
