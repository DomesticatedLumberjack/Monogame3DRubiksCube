using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame3DRubiksCube.Rubiks{
    public class Block{
        private static int totalBlocks = 0;
        private VertexPositionColor[] vertexes;
        private int id;
        public Block(Vector3 initPosition)
        {
            id = totalBlocks++;
            vertexes = new VertexPositionColor[36];
            Vector2 Texcoords = new Vector2(0f, 0f);
            Vector3[] face = new Vector3[6];
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

        public void SetCubePostition(Vector3 newPos)
        {
            for(int i = 0; i < vertexes.Length; i++)
            {
                vertexes[i].Position = newPos;
            }
        }

        public Vector3 GetBlockCenter()
        {
            Vector3 centerPos = Vector3.Zero;
            for(int i = 0; i < vertexes.Length; i++)
            {
                centerPos += vertexes[i].Position;
            }
            centerPos /= vertexes.Length;
            return centerPos;
        }

        public void RotateAboutPoint(Vector3 pointOfRotation, Vector3 rotationAngle)
        {
            for(int i = 0; i < vertexes.Length; i++)
            {
                float distance = Vector3.Distance(vertexes[i].Position, pointOfRotation);
                Vector3 orbitOffset = vertexes[i].Position - pointOfRotation;
                Matrix rotation = Matrix.CreateFromYawPitchRoll(rotationAngle.X, rotationAngle.Y, rotationAngle.Z);
                Vector3.Transform(ref orbitOffset, ref rotation, out orbitOffset);
                vertexes[i].Position = pointOfRotation + orbitOffset;
            }
        }

        public void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertexes, 0, 12);
        }
    }
}