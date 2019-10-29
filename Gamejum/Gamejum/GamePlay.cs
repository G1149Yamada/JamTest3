﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamejum
{
    class GamePlay
    {
        MapChip1 mapChip1;

        Player player;

        public Texture2D texture;
        public Texture2D secoundTex;
        public Texture2D TripleJump;
        public Texture2D goal;
        public Texture2D candle;
        public Texture2D dimensionsCandle;
        public Texture2D window;
        public Texture2D dimensionsWindow;
        public Texture2D fiveCandle;
        public Texture2D dimensionsFiveCandle;
        private Texture2D AllTexture;
        private Texture2D AllDecoration;
        private Vector2 worldPosition;
        private Vector2 cameraPosition;
        private float Alffa;

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
            player.Initialize();
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
                    if (mapData[y, x] == 1 || mapData[y, x] == 2 || mapData[y, x] == 3 || mapData[y, x] == 4 || mapData[y, x] == 5 || mapData[y, x] == 6 || mapData[y, x] == 7 || mapData[y, x] == 8 || mapData[y, x] == 9 || mapData[y, x] == 10 || mapData[y, x] == 11 || mapData[y, x] == 12||mapData[y,x]==13||mapData[y,x]==14||mapData[y,x]==15)
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

                    if (mapData[y, x] == 1 || mapData[y, x] == 2 || mapData[y, x] == 3 || mapData[y, x] == 4 || mapData[y, x] == 5)
                    {
                        num = 0;

                        ScrollX(x);
                        ScrollY(y);
                        if (mapData[y, x] == 1 || mapData[y, x] == 5)
                        {
                            num = 0;
                        }
                        else if (mapData[y, x] == 2)
                        {
                            num = 1;
                        }
                        else if (mapData[y, x] == 3 || mapData[y, x] == 4)
                        {
                            num = 3;
                        }

                        spriteBatch.Draw(texture, cameraPosition, new Rectangle(128 * num, 0, 128, 128), Color.White);
                    }
                    else if (mapData[y, x] == 6 || mapData[y, x] == 7 || mapData[y, x] == 12)
                    {
                        num = 0;

                        ScrollX(x);
                        ScrollY(y);
                        if (player.D4C == false)
                        {
                            num = 2;
                            AllTexture = texture;
                            Alffa = 0.7f;
                        }
                        else
                        {
                            if (mapData[y, x] == 6)
                            {
                                AllTexture = secoundTex;
                            }
                            if (mapData[y, x] == 7 || mapData[y, x] == 12)
                            {
                                AllTexture = TripleJump;
                            }
                            Alffa = 1f;
                        }

                        spriteBatch.Draw(AllTexture, cameraPosition, new Rectangle(128 * num, 0, 128, 128), Color.White * Alffa);
                    }
                    else if (mapData[y, x] == 8 || mapData[y, x] == 9 || mapData[y, x] == 11)
                    {
                        num = 0;
                        ScrollX(x);
                        ScrollY(y);
                        if (player.D4C == false)
                        {
                            if (mapData[y, x] == 9)
                            {
                                AllTexture = secoundTex;
                            }
                            if (mapData[y, x] == 8 || mapData[y, x] == 11)
                            {
                                AllTexture = TripleJump;
                            }
                            Alffa = 1;
                        }
                        else
                        {
                            num = 2;
                            AllTexture = texture;
                            Alffa = 0.7f;
                        }
                        spriteBatch.Draw(AllTexture, cameraPosition, new Rectangle(128 * num, 0, 128, 128), Color.White * Alffa);
                    }
                    else if (mapData[y, x] == 10)
                    {
                        ScrollX(x);
                        ScrollY(y);
                        spriteBatch.Draw(goal, cameraPosition, Color.White);
                    }
                    else if (mapData[y, x] == 13)
                    {
                        ScrollX(x);
                        ScrollY(y);
                        if (player.D4C == true)
                        {
                            AllTexture = dimensionsCandle;
                        }
                        else
                        {
                            AllTexture = candle;
                        }
                        spriteBatch.Draw(AllTexture, cameraPosition, Color.White);
                    }
                    else if (mapData[y, x] == 14)
                    {
                        ScrollX(x);
                        ScrollY(y);
                        if (player.D4C == true)
                        {
                            AllTexture = dimensionsWindow;
                        }
                        else
                        {
                            AllTexture = window;
                        }
                        spriteBatch.Draw(AllTexture, cameraPosition, Color.White);
                    }
                    else if (mapData[y, x] == 15)
                    {
                        ScrollX(x);
                        ScrollY(y);
                        if (player.D4C == true)
                        {
                            AllTexture = fiveCandle;
                        }
                        else
                        {
                            AllTexture = dimensionsFiveCandle;
                        }
                        spriteBatch.Draw(AllTexture, cameraPosition, Color.White);
                    }
                }
            }
        }

        private void ScrollX(int x)
        {
            if (playerWorldPosition.X <= Screen.Width / 2 - width / 2)
            {

            }
            else if (playerWorldPosition.X >= Screen.Width / 2 - width / 2 && playerWorldPosition.X < rightWall - Screen.Width / 2 - width)
            {
                cameraPosition.X = cameraPosition.X - (playerWorldPosition.X - Screen.Width / 2) - width / 2;
            }
            else if (playerWorldPosition.X >= rightWall - Screen.Width / 2 - width && playerWorldPosition.X <= rightWall)
            {
                cameraPosition.X = cameraPosition.X - (rightWall - Screen.Width) + width / 2;
            }
        }

        private void ScrollY(int Y)
        {
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
        }
    }
}
