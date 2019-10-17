using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamejum
{
    class MapDraw
    {
        MapChip1 mapChip1;

        Player player;

        public Texture2D texture;
        private Vector2 worldPosition;
        private Vector2 cameraPosition;

        private int[,] mapData;

        private int x;
        private int y;

        private int width;
        private int height;

        private Vector2 playerWorldPosition;

        private int rightWall;
        private int bottomWall;
        private int num;

        public void Initialize()
        {
            player = new Player();

            mapChip1 = new MapChip1();
            mapChip1.Ini();

            x = 0;
            y = 0;

            width = 128;
            height = 128;

            mapData = mapChip1.GetMapData();

            for (y = 0; y < mapData.GetLength(0); y++)
            {
                for (x = 0; x < mapData.GetLength(1); x++)
                {
                    if (mapData[y, x] == 1)
                    {
                        worldPosition = new Vector2(x * width, y * height);


                    }
                }
            }

            playerWorldPosition = new Vector2();

            rightWall = mapData.GetLength(1) * width;
            bottomWall = mapData.GetLength(0) * height;

        }

        public void Updata(GameTime gameTime)
        {
            player.Update(gameTime);
            playerWorldPosition = player.GetPosition();
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < mapData.GetLength(0); y++)
            {
                for (int x = 0; x < mapData.GetLength(1); x++)
                {

                    worldPosition = new Vector2(x * width, y * height);
                    cameraPosition = worldPosition;
                    

                    if (mapData[y, x] == 1||mapData[y,x]==2||mapData[y,x]==3)
                    {
                        num = 0;

                        ////spriteBatch.Draw(texture, cameraPosition, new Rectangle(0, 0, 64, 64), Color.White);
                        if (playerWorldPosition.X <= Screen.Width / 2 - width / 2)
                        {


                        }
                        else if (playerWorldPosition.X >= Screen.Width / 2 - width / 2 && playerWorldPosition.X < rightWall - Screen.Width / 2 - width)
                        {
                            //new Vector2(cameraPosition.X = cameraPosition.X - (playerWorldPosition.X - Screen.Width),cameraPosition.Y);

                            cameraPosition.X = cameraPosition.X - (playerWorldPosition.X - Screen.Width / 2) - width / 2;
                        }
                        else if (playerWorldPosition.X >= rightWall - Screen.Width / 2 - width && playerWorldPosition.X <= rightWall)
                        {
                            cameraPosition.X = cameraPosition.X - (rightWall - Screen.Width) + width / 2;
                        }

                        //もし高さがスクリーンの真ん中により上に行くとき
                        if (playerWorldPosition.Y <= Screen.Height / 2 - height / 2)
                        {

                        }
                        else if (playerWorldPosition.Y >= Screen.Height / 2 - height / 2 && playerWorldPosition.Y < bottomWall - Screen.Height / 2 - height)
                        {
                            cameraPosition.Y = cameraPosition.Y - (playerWorldPosition.Y - Screen.Height / 2) - height / 2;
                        }
                        else if (playerWorldPosition.Y >= bottomWall - Screen.Height / 2 - height && playerWorldPosition.Y <= bottomWall)
                        {
                            cameraPosition.Y = cameraPosition.Y - (bottomWall - Screen.Height) + height / 2;
                        }

                        if (mapData[y, x] == 1)
                        {
                            num = 0;
                        }
                        if (mapData[y, x] == 2)
                        {
                            num = 1;
                        }
                        if (mapData[y, x] == 3)
                        {
                            num = 3;
                        }

                        spriteBatch.Draw(texture, cameraPosition, new Rectangle(128 * num, 0, 128, 128), Color.White);
                    }
                    
                }

            }

        }
    }
}
