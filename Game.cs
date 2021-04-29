using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RubixSolver
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        VertexPositionNormalTexture[] cube;

        //Camera
        //Renderer
        public struct Renderer{
            public BasicEffect BasicEffect; //3D equivalent to spritebatch
            public VertexPositionColor[] TriangleVertices;
            public VertexBuffer VertexBuffer;
        }
        
        //Objects
        Renderer ren;

        float angle;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
            //Init objects
            ren = new Renderer();

            //Setup Renderer
            ren.BasicEffect = new BasicEffect(GraphicsDevice);
            ren.BasicEffect.AmbientLightColor = Vector3.One;
            ren.BasicEffect.DirectionalLight0.Enabled = true;
            ren.BasicEffect.DirectionalLight0.DiffuseColor = Vector3.One;
            ren.BasicEffect.DirectionalLight0.Direction = Vector3.Normalize(Vector3.One);
            ren.BasicEffect.LightingEnabled = true;

            //Setup Camera
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.Pi / 4.0f, (float)this.Window.ClientBounds.Width / (float)this.Window.ClientBounds.Height, 1f, 10f);
            ren.BasicEffect.Projection = projection;
            Matrix V = Matrix.CreateTranslation(0f,0f,-10f);
            ren.BasicEffect.View = V;

            ren.VertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
            ren.BasicEffect.AmbientLightColor = new Vector3(0.0f, 0.0f, 0.0f);

            //Orbit
            angle = 0;
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Cube cubeMaker = new Cube();
            cube = cubeMaker.MakeCube();
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            angle = angle + 0.05f;
            if (angle > 2 * MathHelper.Pi) angle = 0;
            Matrix R = Matrix.CreateRotationY(angle) * Matrix.CreateRotationX(.4f);
            Matrix T = Matrix.CreateTranslation(0.0f, 0f, 5f);
            ren.BasicEffect.World = R * T;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            RasterizerState rasterizerState = new RasterizerState();
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach(EffectPass pass in ren.BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.GraphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, cube, 0, 12);
            }

            base.Draw(gameTime);
        }
    }
}