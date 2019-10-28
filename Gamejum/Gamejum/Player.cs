﻿using System;
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
        public bool isGamePlay = false;
        private Vector2 position;
        private Vector2 cameraPosition;
        private Vector2 velocity;
        public Texture2D name;
        public Texture2D secoundname;
        public Texture2D iceBlock;
        public Texture2D rightCatch;
        public Texture2D leftCatch;
        public int[,] mapData;

        private bool isHit;//壁掴みようの判定
        private bool isJump;

        public float count; //押す間隔
        private float jumpCount;
        public bool D4C;//平衡世界
        public int D4Ccount;

        private int X;
        private int Y;
        private bool top;

        private Vector2 blockPosition;
        private Vector2 vect;
        private int width;
        private int height;
        private List<Vector2> blockList;

        private int searchX;
        private int searchY;
        private int searchListNumber;
        private bool four;
        private bool five;

        private float jumpVelocity;//Y軸に使う
        private float speed;
        private bool tuch;
        private int tuchCount;

        private int rightWall;
        private int bottomWall;
        private int DrawCount;
        private int anicount;
        private float alpha;

        private bool bottom;
        private KeyboardState previousKey;//キーボード

        MapChip1 mapChip1;

        public Player()
        {
            position = new Vector2(200, 2150);
            velocity = new Vector2(5, 5);

            isJump = false;
            jumpCount = 1;
            tuch = false;
            four = false;

            tuchCount = 0;
            anicount = 0;

            previousKey = Keyboard.GetState();//キーボード

            speed = 1.5f;
            jumpVelocity = 1;
        }

        private void GetMap()
        {
            X = 0;
            Y = 0;
            width = 128;
            height = 128;

            cameraPosition = position;

            mapChip1 = new MapChip1();
            mapChip1.Ini();

            mapData = mapChip1.GetMapData();
            blockList = new List<Vector2>();
            blockPosition = new Vector2();
            vect = new Vector2();
            for (Y = 0; Y < mapData.GetLength(0); Y++)
            {
                for (X = 0; X < mapData.GetLength(1); X++)
                {
                    if (mapData[Y, X] == 0 || mapData[Y, X] == 1 || mapData[Y, X] == 2 || mapData[Y, X] == 3 || mapData[Y, X] == 4 || mapData[Y, X] == 5 || mapData[Y, X] == 6 || mapData[Y, X] == 7 || mapData[Y, X] == 8 || mapData[Y, X] == 9 || mapData[Y, X] == 10)

                        blockPosition = new Vector2(X * width, Y * height);
                    blockList.Add(blockPosition);
                }

            }
            searchX = new int();
            searchY = new int();
            searchListNumber = new int();

            rightWall = mapData.GetLength(1) * width;
            bottomWall = mapData.GetLength(0) * height;
        }

        public void Initialize()
        {
            position = new Vector2(200, 2150);
            velocity = new Vector2(5, 5);

            isJump = false;
            jumpCount = 1;
            tuch = false;
            four = false;

            tuchCount = 0;
            anicount = 0;

            previousKey = Keyboard.GetState();//キーボード

            speed = 1.5f;
            jumpVelocity = 1;

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
                    if (D4C == true)
                    {
                        isJump = true;
                        if (isJump == true)
                        {
                            jumpVelocity = -5f;
                            if (speed >= 1.5f)
                            {
                                speed = 3.2f;
                            }
                            if (speed <= -1.5f)
                            {
                                speed = -3.2f;
                            }
                        }
                    }
                    break;
                case 7:
                    HIT(SizeX, SizeY, Vector);
                    tuchCount = 0;
                    jumpCount = 0;
                    if (D4C == true)
                    {
                        tuch = true;
                        jumpVelocity = -2;
                        speed = -2;
                        five = true;
                    }
                    break;
                case 8:
                    HIT(SizeX, SizeY, Vector);
                    tuchCount = 0;
                    jumpCount = 0;
                    if (D4C == false)
                    {
                        tuch = true;
                        jumpVelocity = -2;
                        speed = 2;
                        four = true;
                    }
                    break;
                case 9:
                    HIT(SizeX, SizeY, Vector);
                    if (D4C == false)
                    {
                        isJump = true;
                        if (isJump == true)
                        {
                            jumpVelocity = -5;
                            if (speed >= 1.5f)
                            {
                                speed = 3.2f;
                            }
                            if (speed <= -1.5f)
                            {
                                speed = -3.2f;
                            }
                        }
                    }
                    break;
                case 10:
                    HIT(SizeX, SizeY, Vector);
                    isGamePlay = true;
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
            position.X += velocity.X * speed;
            position.Y += velocity.Y * jumpVelocity;
            four = false;

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
            }
            else
            {
                count = 0;
            }

            //アニメーションカウント

            GetMap();

            Rectangle playerRect = new Rectangle(
               (int)position.X,
               (int)position.Y,
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

                    vect = position - b;
                    Hit(searchX, searchY, vect);

                    if (tuch == true)
                    {
                        isJump = false;
                        tuchCount++;
                        if (tuchCount > 90 || top == true)
                        {
                            tuch = false;
                            tuchCount = 0;
                            jumpVelocity = 1.5f;
                        }
                    }
                    if (isJump == true)
                    {
                        jumpCount++;
                        if (jumpCount > 120 || top == true)
                        {
                            isJump = false;
                            jumpCount = 0;
                            jumpVelocity = 1.5f;
                        }
                    }
                    else if (jumpCount == 0 && tuchCount == 0)
                    {
                        jumpVelocity = 1.5f;
                        if (speed > 0)
                        {
                            speed = 1.5f;
                        }
                        else if (speed < 0)
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
            //スクロールした側の場合
            else if (position.Y >= Screen.Height / 2 - height / 2 && position.Y < bottomWall - Screen.Height / 2 - height)
            {
                cameraPosition.Y = Screen.Height / 2 - height / 2 + 25;
            }
            //スクロール上側の場合
            else if (position.Y >= bottomWall - Screen.Height / 2 - height && position.Y <= bottomWall)
            {
                cameraPosition.Y = position.Y - (bottomWall - Screen.Height) + height / 2 + 25;
            }
            if (four == false && five == false)
            {
                anicount++;
                if (anicount > 2)
                {
                    DrawCount++;
                    anicount = 0;
                    if (DrawCount <= 13)
                    {
                        DrawCount %= 13;
                    }
                }
            }
            if (four == true || five == true)
            {
                alpha = 0;
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
                bottom = true;
                top = true;
                if (Vctor.Y < 0)//top
                {
                    position.Y = SizeY - 128;
                    isHit = true;
                    top = false;
                    alpha = 1;
                }
                else if (Vctor.Y > 0)
                {
                    bottom = false;
                    position.Y = SizeY + 128;
                }
            }

            else if (Math.Abs(Vctor.X) > Math.Abs(Vctor.Y))
            {
                jumpVelocity = 0.8f;
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
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (speed < 0)
            {
                spriteBatch.Draw(name, cameraPosition, new Rectangle(128 * DrawCount, 0, 128, 128), Color.White * alpha);
            }
            else if (speed > 0)
            {
                spriteBatch.Draw(secoundname, cameraPosition, new Rectangle(128 * DrawCount, 0, 128, 128), Color.White * alpha);
            }
            if (four == true)
            {
                spriteBatch.Draw(leftCatch, cameraPosition, Color.White);
            }
        }
    }
}
