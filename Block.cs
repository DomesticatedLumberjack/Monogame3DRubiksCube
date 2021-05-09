using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RubixSolver{
    enum FaceColor{
        Blank,
        White,
        Yellow,
        Red,
        Orange,
        Blue,
        Green
    }
    public class Block{
        public VertexPositionColor[] vertexes {get; set;}
        public Vector3 initCubePosition = Vector3.Zero;
        public Block(Vector3 initPosition, Vector3 arrayPos)
        {
            vertexes = new VertexPositionColor[36];
            Vector2 Texcoords = new Vector2(0f, 0f);
            Vector3[] face = new Vector3[6];
            initCubePosition = arrayPos;

            //TopLeft
            face[0] = new Vector3(-1f, 1f, 0.0f);
            //BottomLeft
            face[1] = new Vector3(-1f, -1f, 0.0f);
            //TopRight
            face[2] = new Vector3(1f, 1f, 0.0f);
            //BottomLeft
            face[3] = new Vector3(-1f, -1f, 0.0f);
            //BottomRight
            face[4] = new Vector3(1f, -1f, 0.0f);
            //TopRight
            face[5] = new Vector3(1f, 1f, 0.0f);


            //front face
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i] = new VertexPositionColor(face[i] + Vector3.UnitZ,Color.White);
                vertexes[i+3] = new VertexPositionColor(face[i + 3] + Vector3.UnitZ, Color.White);
            }
            //Back face
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i + 6] = new VertexPositionColor(face[2 - i] - Vector3.UnitZ, Color.Yellow);
                vertexes[i + 6 + 3] = new VertexPositionColor(face[5 - i] - Vector3.UnitZ, Color.Yellow);
            }
            //left face
            Matrix RotY90 = Matrix.CreateRotationY(-MathHelper.Pi / 2f);
            Matrix RotX90 = Matrix.CreateRotationX(-MathHelper.Pi / 2f);
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i + 12] = new VertexPositionColor(Vector3.Transform(face[i], RotY90) - Vector3.UnitX, Color.Orange);
                vertexes[i + 12 + 3] = new VertexPositionColor(Vector3.Transform(face[i + 3], RotY90) - Vector3.UnitX, Color.Orange);
            }
            //Right face
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i + 18] = new VertexPositionColor(Vector3.Transform(face[2 - i], RotY90) + Vector3.UnitX, Color.Red);
                vertexes[i + 18 + 3] = new VertexPositionColor(Vector3.Transform(face[5 - i], RotY90) + Vector3.UnitX, Color.Red);
            }
            //Top face
            
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i + 24] = new VertexPositionColor(Vector3.Transform(face[i], RotX90) + Vector3.UnitY, Color.Blue);
                vertexes[i + 24 + 3] = new VertexPositionColor(Vector3.Transform(face[i + 3], RotX90) + Vector3.UnitY, Color.Blue);
            }
            //Bottom face
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i + 30] = new VertexPositionColor(Vector3.Transform(face[2 - i], RotX90) - Vector3.UnitY, Color.Green);
                vertexes[i + 30 + 3] = new VertexPositionColor(Vector3.Transform(face[5 - i], RotX90) - Vector3.UnitY, Color.Green);
            }

            if(initPosition != Vector3.Zero)
            {
                for(int i = 0; i < vertexes.Length; i++)
                {
                    vertexes[i].Position += initPosition;
                }
            }
        }

        public void MoveCubeDirectionVector(Vector3 moveVect)
        {
            for(int i = 0; i < vertexes.Length; i++)
            {
                vertexes[i].Position += moveVect;
            }
        }

        public void MoveCubeToVector(Vector3 moveVect)
        {
            for(int i = 0; i < vertexes.Length; i++)
            {
                vertexes[i].Position = moveVect;
            }
        }
    }
}