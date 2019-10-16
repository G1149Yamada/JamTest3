using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Gamejum
{
    class Player
    {
        private Vector2 worldPosition;
        private Vector2 cameraPosition;
        private Vector2 velocity;
        public Texture2D name;
        public int count;
        public int DrawCount;
        public int[,] mapData;

        private int X;
        private int Y;

        private Vector2 blockPosition;
        private Vector2 vect;
        private int width;
        private int height;
        private List<Vector2> blockList;

        private int searchX;
        private int searchY;
        private int searchListNumber;

        private int W;//Y軸の計算を受け取る場所
        private int Z;

        private int rightWall;
        private int bottomWall;

        private KeyboardState previousKey;//キーボード

        MapChip1 mapChip1;

        public Player()
        {
            worldPosition = new Vector2(1800, 128);
            cameraPosition = worldPosition;
            velocity = Vector2.Zero;
            DrawCount = 1;
            X = 0;
            Y = 0;
            width = 128;
            height = 128;

            mapChip1 = new MapChip1();
            mapChip1.Ini();

            mapData = mapChip1.GetMapData();
            blockList = new List<Vector2>();
            blockPosition = new Vector2();
            vect = new Vector2();

            previousKey = Keyboard.GetState();//キーボード

            for (Y = 0; Y < mapData.GetLength(0); Y++)
            {
                for (X = 0; X < mapData.GetLength(1); X++)
                {
                    if (mapData[Y, X] == 1 || mapData[Y, X] == 2 || mapData[Y, X] == 3 || mapData[Y, X] == 4 || mapData[Y, X] == 5)
                    {

                        blockPosition = new Vector2(X * width, Y * height);
                        blockList.Add(blockPosition);

                    }

                }
            }

            searchX = new int();
            searchY = new int();
            searchListNumber = new int();

            Z = -1;

            rightWall = mapData.GetLength(1) * width;
            bottomWall = mapData.GetLength(0) * height;

        }

        public void Initialize()
        {
        }

        /// <summary>
        /// 当たり判定
        /// </summary>
        /// <param name="SX">ブロックのPosition.X</param>
        /// <param name="SY">ブロックのposiiton.Y</param>
        /// <param name="V">二点間の距離(ベクトル)</param>
        public void Hit(int SX, int SY, Vector2 V)
        {

            searchListNumber = mapData[SY % height, SX % width];

            switch (searchListNumber)
            {
                case 1:

                    if (Math.Abs(V.X) < Math.Abs(V.Y))
                    {
                        if (V.Y < 0)//top
                        {
                            worldPosition.Y = SY - 128;
                        }
                        else if (V.Y > 0)
                        {
                            worldPosition.Y = SY + 128;

                        }
                    }
                    else if (Math.Abs(V.X) > Math.Abs(V.Y))
                    {
                        if (V.X >= 0)
                        {

                            worldPosition.X = SX + 128;
                            Z = 1;
                        }
                        else if (V.X < 0)
                        {

                            worldPosition.X = SX - 128;
                            Z = -1;
                        }

                    }
                    break;
            }

        }

        public void Update(GameTime gameTime)
        {
            velocity = new Vector2(5, 5);
            worldPosition.X += (velocity.X * Z);
            worldPosition.Y++;

            if (previousKey.IsKeyDown(Keys.Space))
            {

            }

            //アニメーションカウント
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



            Rectangle playerRect = new Rectangle(
               (int)worldPosition.X,
               (int)worldPosition.Y,
               128, 128);

            //ブロックの処理
            foreach (var b in blockList)
            {
                Rectangle blockRect = new Rectangle(
                    (int)b.X,
                    (int)b.Y,
                    128, 128);

                if (playerRect.Intersects(blockRect))//playerとブロックの当たり判定
                {

                    searchX = (int)b.X;
                    searchY = (int)b.Y;

                    //searchListNumber = mapData[searchY, searchX];

                    vect = worldPosition - b;

                    Hit(searchX, searchY, vect);

                }
            }

            //もしプレイヤーの縦位置よりもブロックのほうが低い場合は
            //そこの場所にとどまる
            if (worldPosition.Y >= searchY)
            {
                

            }

            cameraPosition = worldPosition;

            //スクロール横
            if (worldPosition.X <= Screen.Width / 2 - width / 2)
            {

            }
            else if (worldPosition.X >= Screen.Width / 2 - width / 2 && worldPosition.X < rightWall - Screen.Width / 2 - width)
            {
                cameraPosition.X = Screen.Width / 2 - width / 2;
            }
            else if (worldPosition.X >= rightWall - Screen.Width / 2 - width && worldPosition.X <= rightWall)
            {
                cameraPosition.X = worldPosition.X - (rightWall - Screen.Width) + width / 2;
            }

            //スクロール縦
            if (worldPosition.Y <= Screen.Height / 2 - height / 2)
            {

            }
            else if (worldPosition.Y >= Screen.Height / 2 - height / 2 && worldPosition.Y < bottomWall - Screen.Height / 2 - height)
            {
                cameraPosition.Y = Screen.Height / 2 - height / 2;
            }
            else if (worldPosition.Y >= bottomWall - Screen.Height / 2 - height && worldPosition.Y <= bottomWall)
            {
                cameraPosition.Y = worldPosition.Y - (bottomWall - Screen.Height) + height / 2;
            }


            Console.WriteLine(worldPosition);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(name, cameraPosition, new Rectangle(64 * DrawCount, 0, 64, 64), Color.White);
        }

        public Vector2 GetPosition()
        {
            return worldPosition;
        }
    }
}
