using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RubixSolver{
    public class Block{
        public VertexPositionNormalTexture[] vertexes {get; set;}
        Matrix Rotation;
        public Block(Vector3 initPosition)
        {
            vertexes = new VertexPositionNormalTexture[36];
            Vector2 Texcoords = new Vector2(0f, 0f);
            Vector3[] face = new Vector3[6];
            Rotation = new Matrix();
            

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
                vertexes[i] = new VertexPositionNormalTexture(face[i] + Vector3.UnitZ, Vector3.UnitZ, Texcoords);
                vertexes[i+3] = new VertexPositionNormalTexture(face[i + 3] + Vector3.UnitZ, Vector3.UnitZ, Texcoords);
            }
            //Back face
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i + 6] = new VertexPositionNormalTexture(face[2 - i] - Vector3.UnitZ, -Vector3.UnitZ, Texcoords);
                vertexes[i + 6 + 3] = new VertexPositionNormalTexture(face[5 - i] - Vector3.UnitZ, -Vector3.UnitZ, Texcoords);
            }
            //left face
            Matrix RotY90 = Matrix.CreateRotationY(-MathHelper.Pi / 2f);
            Matrix RotX90 = Matrix.CreateRotationX(-MathHelper.Pi / 2f);
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i + 12] = new VertexPositionNormalTexture(Vector3.Transform(face[i], RotY90) - Vector3.UnitX, Vector3.UnitX, Texcoords);
                vertexes[i + 12 + 3] = new VertexPositionNormalTexture(Vector3.Transform(face[i + 3], RotY90) - Vector3.UnitX, Vector3.UnitX, Texcoords);
            }
            //Right face
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i + 18] = new VertexPositionNormalTexture(Vector3.Transform(face[2 - i], RotY90) + Vector3.UnitX, -Vector3.UnitX, Texcoords);
                vertexes[i + 18 + 3] = new VertexPositionNormalTexture(Vector3.Transform(face[5 - i], RotY90) + Vector3.UnitX, -Vector3.UnitX, Texcoords);
            }
            //Top face
            
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i + 24] = new VertexPositionNormalTexture(Vector3.Transform(face[i], RotX90) + Vector3.UnitY, Vector3.UnitY, Texcoords);
                vertexes[i + 24 + 3] = new VertexPositionNormalTexture(Vector3.Transform(face[i + 3], RotX90) + Vector3.UnitY, Vector3.UnitY, Texcoords);
            }
            //Bottom face
            for (int i = 0; i <= 2; i++)
            {
                vertexes[i + 30] = new VertexPositionNormalTexture(Vector3.Transform(face[2 - i], RotX90) - Vector3.UnitY, -Vector3.UnitY, Texcoords);
                vertexes[i + 30 + 3] = new VertexPositionNormalTexture(Vector3.Transform(face[5 - i], RotX90) - Vector3.UnitY, -Vector3.UnitY, Texcoords);
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