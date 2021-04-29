using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RubixSolver
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        //Camera
        
        struct Camera{
            public Vector3 Target { get; set; }
            public Vector3 Postion { get; set; }
            public Matrix Projection { get; set; }
            public Matrix View { get; set; }
            public Matrix World { get; set; }
        }

        BasicEffect basicEffect; //3D equivalent to spritebatch
        VertexPositionColor[] triangleVertices;
        VertexBuffer vertexBuffer;
        

        Camera cam;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            cam = new Camera();
            base.Initialize();
        }
        protected override void LoadContent()
        {
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}