using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankGame_IP3D
{
    public class Game1 : Game
    {
        Matrix view;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ClsBattlefield terreno;
        //TesteTerreno terreno;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //terreno = new ClsBattlefield(GraphicsDevice, Content);
            //terreno = new TesteTerreno(GraphicsDevice, Content);
            terreno = new ClsBattlefield(GraphicsDevice, Content);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            terreno.Draw(GraphicsDevice);
            base.Draw(gameTime);
        }
    }
}