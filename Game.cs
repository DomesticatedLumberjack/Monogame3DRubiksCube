using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RubixSolver
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Cube cube;

        //Camera
        //Renderer
        public struct Renderer{
            public BasicEffect BasicEffect; //3D equivalent to spritebatch
            public VertexPositionColor[] TriangleVertices;
            public VertexBuffer VertexBuffer;
        }
        
        //Objects
        Renderer ren;

        float angleX;
        float angleY;
        bool orbit;
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
            ren.BasicEffect.AmbientLightColor = new Vector3(0.8f, 0.8f, 0.8f);

            //Orbit
            angleX = 0;
            angleY = 0;

            cube = new Cube();
            orbit = false;
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            if(Keyboard.GetState().IsKeyDown(Keys.Right))
                angleY -= 0.05f;
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
                angleY += 0.05f;
            if(Keyboard.GetState().IsKeyDown(Keys.Up))
                angleX -= 0.05f;
            if(Keyboard.GetState().IsKeyDown(Keys.Down))
                angleX += 0.05f;

            if(Keyboard.GetState().IsKeyDown(Keys.Space))
                orbit = !orbit;
            
            if(orbit)
                angleY += 0.005f;
            
            if (angleY > 2 * MathHelper.Pi) angleY = 0;
            if (angleX > 2 * MathHelper.Pi) angleX = 0;
            Matrix R = Matrix.CreateRotationY(angleY) * Matrix.CreateRotationX(0.4f + angleX);
            Matrix T = Matrix.CreateTranslation(0.0f, 0f, 5f);
            ren.BasicEffect.World = R * T;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.CullClockwiseFace;
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach(EffectPass pass in ren.BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.GraphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, cube.vertexPositions, 0, 12);
            }

            base.Draw(gameTime);
        }
    }
}