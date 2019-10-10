using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gamejum
{
    class Map
    {
        MapChip mapChip = new MapChip();
        private int[,] map;

        private Vector2 position;
        private float whith, hight;

        public Map()
        {
            map = mapChip.map;
        }

        public void Initialize()
        {
            GetPosition();
        }

        public void GetPosition()
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[x, y] == 1)
                    {
                        position = new Vector2(x * whith, y * hight);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
