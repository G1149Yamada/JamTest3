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

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                isTitle = true;
            }
        }
        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(title, position, Color.White);
        }

    }
}
