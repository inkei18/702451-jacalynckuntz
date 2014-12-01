using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace gametry3
{
    public struct PlayerData
    {
        public Vector2 Position;
        public bool IsAlive;
        public Color Color;
        public float Angle;
        public float Power;
    }
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        int screenWidth;
        int screenHeight;
        Texture2D backgroundTexture;
        Texture2D foregroundTexture;
        PlayerData[] players;
        int numberOfPlayers = 4;
        Texture2D armyTexture;
        Texture2D launcherTexture;
        float playerScaling;
        int currentPlayer = 0;
        bool rocketFlying;
        bool grenadeFlying;
        float rocketDirection, rocketPosition, rocketAngle;
        float grenadeDirection, grenadePosition, grenadeAngle;

        public interface Fire
        {
            void Firecannon(float dir, float angle);
        }

        public abstract class ReadyFire
        {
            public Fire fire { get; set; }
            abstract void Go();
        }

        public class RocketFire : ReadyFire
        {
            public override void Go()
            {
                
                if (rocketFlying)
                {
                    Vector2 gravity = new Vector2(0, 1);
                    rocketDirection += gravity / 10.0f;
                    rocketPosition += rocketDirection;
                    rocketAngle = (float)Math.Atan2(rocketDirection.X, -rocketDirection.Y);

                    fire.Firecannon(rocketDirection, rocketAngle)

                }
            }
        }
        public class GrenadeFire : ReadyFire
        {
            public override void Go()
            {
                
                if (grenadeFlying)
                {
                    Vector2 gravity = new Vector2(0, 1);
                    grenadeDirection += gravity / 20.0f;
                    grenadePosition += grenadeDirection;
                    grenadeAngle = (float)Math.Atan2(grenadeDirection.X, -grenadeDirection.Y);

                    fire.Firecannon(grenadeDirection, grenadeAngle)

                }
            }
        }

        public interface Weapon
        {
            string type { get; }
        }
        public class Grenade : Weapon
        {
            public string type
            {
                get {return "Grenade";}
            }
        }
        public class Rocket : Weapon
        {
            public string type
            {
                get {return "Rocket";}
            }
        }
        public abstract class WeaponType : Weapon
        {
            private Weapon weap;
            public WeaponType(Weapon w)
            {
                weap = w;
            }
            public string type
            {
                get { return weap.type; }
            }
        }
        public class GrenadeBuilder : WeaponType
        {
            public GrenadeBuilder(Weapon weapon)
                : base(weapon)
            {

            }
            public void build()
            {
                launcherTexture = Content.Load<Texture2D>("launcher2");
            }
        }
        public class RocketBuilder : WeaponType
        {
            public RocketBuilder(Weapon weapon)
                : base(weapon)
            {

            }
            public void build()
            {
                launcherTexture = Content.Load<Texture2D>("launcher2");
            }
        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1300;
            graphics.PreferredBackBufferHeight = 830;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Game";

            base.Initialize();
        }

        private void SetUpPlayers()
        {
            Color[] playerColors = new Color[10];
            playerColors[0] = Color.HotPink;
            playerColors[1] = Color.BlueViolet;
            playerColors[2] = Color.DarkMagenta;
            playerColors[3] = Color.Plum;
            playerColors[4] = Color.DeepPink;
            playerColors[5] = Color.DarkViolet;
            playerColors[6] = Color.Pink;
            playerColors[7] = Color.Fuchsia;
            playerColors[8] = Color.MediumPurple;
            playerColors[9] = Color.DarkOrchid;

            players = new PlayerData[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players[i].IsAlive = true;
                players[i].Color = playerColors[i];
                players[i].Angle = MathHelper.ToRadians(90);
                players[i].Power = 100;


            }
            players[0].Position = new Vector2(50, 633);
            players[1].Position = new Vector2(440, 582);
            players[2].Position = new Vector2(670, 621);
            players[3].Position = new Vector2(1220, 474);
        }
        
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
            backgroundTexture = Content.Load<Texture2D>("gamebg");
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;
            foregroundTexture = Content.Load<Texture2D>("foreground");
            SetUpPlayers();
            armyTexture = Content.Load<Texture2D>("army2");
            Grenade gre = new Grenade();
            GrenadeBuilder grenade = new GrenadeBuilder(gre);
            grenade.build();
            Rocket roc = new Rocket();
            RocketBuilder rocket = new RocketBuilder(roc);
            rocket.build();
            playerScaling = 40.0f / (float)armyTexture.Width;
            
        }

        
        protected override void UnloadContent()
        {
            
        }

        
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            ProcessKeyboard();

            base.Update(gameTime);
        }

        private void ProcessKeyboard()
        {
            KeyboardState keybState = Keyboard.GetState();
            if (keybState.IsKeyDown(Keys.Left))
                players[currentPlayer].Angle -= 0.01f;
            if (keybState.IsKeyDown(Keys.Right))
                players[currentPlayer].Angle += 0.01f;

            if (players[currentPlayer].Angle > MathHelper.PiOver2)
                players[currentPlayer].Angle = -MathHelper.PiOver2;
            if (players[currentPlayer].Angle < -MathHelper.PiOver2)
                players[currentPlayer].Angle = MathHelper.PiOver2;

            if (keybState.IsKeyDown(Keys.Down))
                players[currentPlayer].Power -= 1;
            if (keybState.IsKeyDown(Keys.Up))
                players[currentPlayer].Power += 1;
            if (keybState.IsKeyDown(Keys.PageDown))
                players[currentPlayer].Power -= 20;
            if (keybState.IsKeyDown(Keys.PageUp))
                players[currentPlayer].Power += 20;

            if (players[currentPlayer].Power > 1000)
                players[currentPlayer].Power = 1000;
            if (players[currentPlayer].Power < 0)
                players[currentPlayer].Power = 0;
            if (keybState.IsKeyDown(Keys.Enter) || keybState.IsKeyDown(Keys.Space))
            {
                rocketFlying = true;

                rocketPosition = players[currentPlayer].Position;
                rocketPosition.X += 25;
                rocketPosition.Y -= 22;
                rocketAngle = players[currentPlayer].Angle;
                Vector2 up = new Vector2(0, -1);
                Matrix rotMatrix = Matrix.CreateRotationZ(rocketAngle);
                rocketDirection = Vector2.Transform(up, rotMatrix);
                rocketDirection *= players[currentPlayer].Power / 50.0f;
            }
            
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            DrawScenery();
            DrawPlayers();
            spriteBatch.End();
            RocketFire rocket = new RocketFire();
            GrenadeFire grenade = new GrenadeFire();

            base.Draw(gameTime);
        }
        private void DrawScenery()
        {
            Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            spriteBatch.Draw(backgroundTexture, screenRectangle, Color.White);
            spriteBatch.Draw(foregroundTexture, screenRectangle, Color.White);


        }
        private void DrawPlayers()
        {
            foreach (PlayerData player in players)
            {
                if (player.IsAlive)
                {
                    int xPos = (int)player.Position.X;
                    int yPos = (int)player.Position.Y;
                    Vector2 cannonOrigin = new Vector2(0, 200);

                    spriteBatch.Draw(launcherTexture, new Vector2(xPos + 40, yPos - 30), null, player.Color, player.Angle, cannonOrigin, playerScaling, SpriteEffects.None, 1);
                    spriteBatch.Draw(armyTexture, player.Position, null, player.Color, 0, new Vector2(0, armyTexture.Height), playerScaling, SpriteEffects.None, 0);
                }
            }
        }

    }
}
