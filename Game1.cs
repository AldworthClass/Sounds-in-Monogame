using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sounds_in_Monogame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseState mouseState;

        string arnoldStatus;

        // Textures
        Texture2D arnoldTexture;
        Rectangle arnoldRect;

        // Fonts
        SpriteFont messageFont;

        // Sound Effects
        SoundEffect backgroundMusic;

        SoundEffect terminatorSound;
        SoundEffectInstance terminatorSoundInstance;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            arnoldStatus = "silent.";
            base.Initialize();
            backgroundMusic.Play();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            arnoldTexture = Content.Load<Texture2D>("terminator");
            arnoldRect = new Rectangle(_graphics.PreferredBackBufferWidth / 2 - 100, _graphics.PreferredBackBufferHeight / 2 - 50, 200, 100);

            terminatorSound = Content.Load<SoundEffect>("T1 Be Back");
            terminatorSoundInstance = terminatorSound.CreateInstance();
            terminatorSoundInstance.IsLooped = false;   // The sound will only play once

            backgroundMusic = Content.Load<SoundEffect>("tng_intro_theme");

            messageFont = Content.Load<SpriteFont>("TextFont");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Plays "I'll be back" when the user clicks on Arnold.
            if (mouseState.LeftButton == ButtonState.Pressed && arnoldRect.Contains(mouseState.X, mouseState.Y))
            {
                arnoldStatus = "talking.";
                terminatorSoundInstance.Play();
            }

            // When the sound is done playing, updates Arnold's status
            if(terminatorSoundInstance.State == SoundState.Stopped)
            {
                arnoldStatus = "silent.";
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.DrawString(messageFont, "Arnold is " + arnoldStatus, new Vector2(320, 75), Color.White);
            _spriteBatch.Draw(arnoldTexture, arnoldRect, Color.White);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
