using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gamejum
{
    class Player
    {
        private Vector2 position;
        private Vector2 velocity;
        public Texture2D name;
        public int count;
        public int DrawCount;

        public Player()
        {
            position = new Vector2(128, 960);
            velocity = Vector2.Zero;
            DrawCount = 1;
        }

        public void Initialize()
        {
        }

        public void Hit()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            count++;
            if (count > 4)
            {
                DrawCount++;
                count = 0;
                if (DrawCount <= 4)
                {
                    DrawCount %= 4;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(name,position,new Rectangle(64*DrawCount,0,64,64), Color.White);
        }
    }
}
