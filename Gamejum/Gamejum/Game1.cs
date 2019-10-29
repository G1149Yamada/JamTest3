using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Gamejum //コミットとプッシュのテスト
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MapChip1 MapChip = new MapChip1();
        Player player = new Player();
        GamePlay mapDraw = new GamePlay();
        Title title = new Title();
        Clear clear = new Clear();
        Texture2D texture;
        Loop loop;
        private int count = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;//フルスクリーン描画
            graphics.PreferredBackBufferWidth = Screen.Width;
            graphics.PreferredBackBufferHeight = Screen.Height;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            player.Initialize();
            mapDraw.Initialize();
            MapChip.Ini();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = Content.Load<Texture2D>("wastanBack");
            mapDraw.texture = Content.Load<Texture2D>("block");
            player.name = Content.Load<Texture2D>("BotRigth");
            player.secoundname = Content.Load<Texture2D>("仮立ち絵２");
            mapDraw.secoundTex = Content.Load<Texture2D>("jumpblock");
            player.iceBlock = Content.Load<Texture2D>("IceMeker(仮)");
            mapDraw.TripleJump = Content.Load<Texture2D>("TripleJumpBlock");

            //titileのアニメーション
            title.title1 = Content.Load<Texture2D>("title1");
            title.title2 = Content.Load<Texture2D>("title2");
            title.title3 = Content.Load<Texture2D>("title3");
            title.title4 = Content.Load<Texture2D>("title4");
            title.title5 = Content.Load<Texture2D>("title5");
            title.title6 = Content.Load<Texture2D>("title6");
            title.title7 = Content.Load<Texture2D>("title7");
            title.title8 = Content.Load<Texture2D>("title8");
            title.title9 = Content.Load<Texture2D>("title9");
            title.title10 = Content.Load<Texture2D>("title10");
            title.title11 = Content.Load<Texture2D>("title11");
            title.title12 = Content.Load<Texture2D>("title12");
            title.title13 = Content.Load<Texture2D>("title13");
            title.title14 = Content.Load<Texture2D>("title14");
            title.title15 = Content.Load<Texture2D>("title15");
            title.space = Content.Load<Texture2D>("pushspace");

            clear.texture = Content.Load<Texture2D>("clear");
            player.rightCatch = Content.Load<Texture2D>("壁張り付き");
            player.leftCatch = Content.Load<Texture2D>("壁張り付き左");
            mapDraw.goal = Content.Load<Texture2D>("goal");
            mapDraw.candle = Content.Load<Texture2D>("candle1");
            mapDraw.dimensionsCandle = Content.Load<Texture2D>("candle2");
            mapDraw.window = Content.Load<Texture2D>("draw1");
            mapDraw.dimensionsWindow = Content.Load<Texture2D>("draw2");
            mapDraw.fiveCandle = Content.Load<Texture2D>("chandelier1");
            mapDraw.dimensionsFiveCandle = Content.Load<Texture2D>("chandelier2");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (loop)
            {
                case Loop.Title:
                    title.Update(gameTime);
                    if (title.isTitle == true)
                    {
                        loop = Loop.GamePlay;
                        title.isTitle = false;
                        title.pushcount = 0;
                    }
                    break;
                case Loop.GamePlay:
                    player.Update(gameTime);
                    mapDraw.Updata(gameTime);
                    if (player.isGamePlay == true)
                    {
                        loop = Loop.Clear;
                        player.isGamePlay = false;
                    }
                    break;
                case Loop.Clear:
                    clear.Update(gameTime);
                    if (clear.isClear == true)
                    {
                        loop = Loop.Title;
                        player.Initialize();
                        mapDraw.Initialize();
                        clear.isClear = false;
                    }
                    break;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (player.D4C == true)
            {
                spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            }
            else
            {
                spriteBatch.Draw(texture, Vector2.Zero, Color.White * 0.5f);
            }
            switch (loop)
            {
                case Loop.Title:
                    title.Draw(spriteBatch);
                    break;
                case Loop.GamePlay:
                    mapDraw.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    break;
                case Loop.Clear:
                    clear.Draw(spriteBatch);
                    player.Initialize();
                    mapDraw.Initialize();
                    break;
            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
