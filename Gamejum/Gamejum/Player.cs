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
        private Vector2 position;
        private Vector2 cameraPosition;
        private Vector2 velocity;
        public Texture2D name;
        public Texture2D secoundname;
        public Texture2D iceBlock;
        public int DrawCount;
        public int[,] mapData;

        private bool isHit;//壁掴みようの判定
        private bool isJump;

        public float count; //押す間隔
        private float jumpCount;
        public bool D4C;//平衡世界
        public int D4Ccount;

        private int X;
        private int Y;
        private Vector2 icePosition;
        private int iceSize;
        private float Distance;
        private bool top;

        private Vector2 blockPosition;
        private Vector2 vect;
        private int width;
        private int height;
        private List<Vector2> blockList;

        private int searchX;
        private int searchY;
        private int searchListNumber;

        private float jumpVelocity;//Y軸に使う
        private float speed;
        private int iceTimer;
        private bool tuch;
        private int tuchCount;

        private int rightWall;
        private int bottomWall;

        private KeyboardState previousKey;//キーボード

        MapChip1 mapChip1;

        public Player()
        {
            position = new Vector2(200, 2150);
            cameraPosition = position;
            Distance = 16;
            icePosition = new Vector2(0, 700);
            velocity = Vector2.Zero;
            DrawCount = 1;
            X = 0;
            Y = 0;
            width = 128;
            height = 128;

            mapChip1 = new MapChip1();
            mapChip1.Ini();

            isJump = false;
            jumpCount = 1;
            tuch = false;
            tuchCount = 0;

            mapData = mapChip1.GetMapData();
            blockList = new List<Vector2>();
            blockPosition = new Vector2();
            vect = new Vector2();

            previousKey = Keyboard.GetState();//キーボード

            for (Y = 0; Y < mapData.GetLength(0); Y++)
            {
                for (X = 0; X < mapData.GetLength(1); X++)
                {
                    if (mapData[Y, X] == 0 || mapData[Y, X] == 1 || mapData[Y, X] == 2 || mapData[Y, X] == 3 || mapData[Y, X] == 4 || mapData[Y, X] == 5 || mapData[Y, X] == 6 || mapData[Y, X] == 7 || mapData[Y, X] == 8)

                        blockPosition = new Vector2(X * width, Y * height);
                    blockList.Add(blockPosition);
                }

            }
            searchX = new int();
            searchY = new int();
            searchListNumber = new int();

            speed = 1.5f;
            jumpVelocity = 1;
            iceSize = 15;
            //iceEsing = 2;
            iceTimer = 1;

            rightWall = mapData.GetLength(1) * width;
            bottomWall = mapData.GetLength(0) * height;
        }

        public void Initialize()
        {
        }

        /// <summary>
        /// 当たり判定
        /// </summary>
        /// <param name="SizeX">ブロックのPosition.X</param>
        /// <param name="SizeY">ブロックのposiiton.Y</param>
        /// <param name="Vector">プレイヤーとブロックの二点間の距離(ベクトル)</param>
        public void Hit(int SizeX, int SizeY, Vector2 Vector)
        {
            searchListNumber = mapData[SizeY / height, SizeX / width];
            switch (searchListNumber)
            {
                case 1:
                    HIT(SizeX, SizeY, Vector);
                    break;

                case 2:
                    HIT(SizeX, SizeY, Vector);
                    break;

                case 3:
                    HIT(SizeX, SizeY, Vector);
                    break;
                case 4:
                    HIT(SizeX, SizeY, Vector);
                    break;
                case 5:
                    HIT(SizeX, SizeY, Vector);
                    break;
                case 6:
                    HIT(SizeX, SizeY, Vector);
                    break;
                case 7:
                    HIT(SizeX, SizeY, Vector);
                    break;
                case 8:
                    HIT(SizeX, SizeY, Vector);
                    break;
            }
        }

        /// <summary>
        /// 切り替え
        /// </summary>
        /// <param name="Counter">0 or 1を返すだけ</param>
        public void D4CCounter(int Counter)
        {
            switch (Counter)
            {
                case 1:
                    D4C = true;
                    break;
                case 2:
                    D4C = false;
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            velocity = new Vector2(5, 5);
            position.X += velocity.X * speed;
            position.Y += velocity.Y * jumpVelocity;
            icePosition.X += cameraPosition.X * Distance;

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                count++;
                if (count == 1)
                {
                    D4Ccount++;
                    D4CCounter(D4Ccount);
                    if (D4Ccount == 2)
                    {
                        D4Ccount = 0;
                    }
                }
                if (count > 50)
                {
                    iceSize = 0;
                }
            }
            else
            {
                iceSize++;
                if (iceSize >= 1)
                {
                    if (iceSize >= 255 + 16)
                    {
                        iceSize = 256 + 16;
                    }
                    count = 0;
                }
            }

            ////アニメーションカウント
            //count++;
            //if (count > 4)
            //{
            //    DrawCount++;
            //    count = 0;
            //    if (DrawCount <= 4)
            //    {
            //        DrawCount %= 4;
            //    }
            //}

            Rectangle playerRect = new Rectangle(
               (int)position.X,
               (int)position.Y,
               128, 128);

            Console.WriteLine(top);

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

                    vect = position - b;
                    Hit(searchX, searchY, vect);

                    if (tuch == true || isJump == true)
                    {
                        tuchCount++;
                        if (tuchCount > 150 || top == true)
                        {
                            tuch = false;
                            isJump = false;
                            tuchCount = 0;
                        }
                    }
                    else if (isJump == false || tuch == false)
                    {
                        jumpVelocity = 1.5f;
                        if (speed == 2)
                        {
                            speed = 1.5f;
                        }
                        else if (speed == -2)
                        {
                            speed = -1.5f;
                        }
                    }
                }
            }
            cameraPosition = position;

            //スクロール横
            if (position.X <= Screen.Width / 2 - width / 2)
            {
            }
            else if (position.X >= Screen.Width / 2 - width / 2 && position.X < rightWall - Screen.Width / 2 - width)
            {
                cameraPosition.X = Screen.Width / 2 - width / 2;
            }
            else if (position.X >= rightWall - Screen.Width / 2 - width && position.X <= rightWall)
            {
                cameraPosition.X = position.X - (rightWall - Screen.Width) + width / 2;
            }

            //スクロール縦
            if (position.Y <= Screen.Height / 2 - height / 2)
            {
            }
            else if (position.Y >= Screen.Height / 2 - height / 2 && position.Y < bottomWall - Screen.Height / 2 - height)
            {
                cameraPosition.Y = Screen.Height / 2 - height / 2 + 25;
            }
            else if (position.Y >= bottomWall - Screen.Height / 2 - height && position.Y <= bottomWall)
            {
                cameraPosition.Y = position.Y - (bottomWall - Screen.Height) + height / 2 + 25;
            }
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        private void HIT(int SizeX, int SizeY, Vector2 Vctor)
        {
            if (Math.Abs(Vctor.X) < Math.Abs(Vctor.Y))
            {
                top = true;
                if (Vctor.Y < 0)//top
                {
                    position.Y = SizeY - 128;
                    isHit = true;
                    top = false;
                }
                else if (Vctor.Y > 0)
                {
                    position.Y = SizeY + 128;
                }
            }

            else if (Math.Abs(Vctor.X) > Math.Abs(Vctor.Y))
            {
                if (Vctor.X >= 0)
                {
                    position.X = SizeX + 128;
                }
                else if (Vctor.X < 0)
                {
                    position.X = SizeX - 128;
                }
            }
            if (isHit == true)
            {
                if (Vctor.X >= 0 && searchListNumber == 3)
                {
                    speed = 1.5f;
                }
                else if (Vctor.X <= 0 && searchListNumber == 1)
                {
                    speed = -1.5f;
                }
            }
            else
            {
                if ((searchListNumber == 4 && Vctor.Y < 0) || (searchListNumber == 5 && Vctor.Y < 0))
                {
                    speed = 0;
                }
            }
            if (D4C == true)
            {
                if (Vctor.Y < 0 && searchListNumber == 6)
                {
                    jumpCount++;
                    isJump = true;
                    if (isJump == true)
                    {
                        jumpVelocity = -5;
                        if (speed >= 1.5f)
                        {
                            speed = 3;
                        }
                        if (speed <= -1.5f)
                        {
                            speed = -3;
                        }
                    }
                }
                if (searchListNumber == 7)
                {
                    tuchCount = 0;
                    tuch = true;
                    if (tuch == true)
                    {
                        if (Vctor.X > 0)
                        {
                            speed = 2;
                            jumpVelocity = -2;
                        }
                        if (Vctor.X < 0)
                        {
                            speed = -2;
                            jumpVelocity = -2;
                        }
                    }
                }
            }
            else
            {

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (D4C == false)
            {
                spriteBatch.Draw(name, cameraPosition, new Rectangle(0, 0, 128, 128), Color.White);
            }
            else
            {
                spriteBatch.Draw(secoundname, cameraPosition, new Rectangle(0, 0, 128, 128), Color.White);
            }

            spriteBatch.Draw(iceBlock, icePosition, new Rectangle(0, 0, 128, iceSize), Color.White);

        }
    }
}
