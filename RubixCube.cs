using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RubixSolver
{
    public class RubixCube
    {
        public Block[,,] blocks {get; set;}
        public RubixCube()
        {
            blocks = new Block[3, 3, 3];
            
            for(int x = 0; x < 3; x++)
            {
                for(int y = 0; y < 3; y++)
                {
                    for(int z = 0; z < 3; z++)
                    {
                        blocks[x, y, z] = new Block(new Vector3(x * 2, y * 2, z* 2));
                    }
                }
            }
        }
        
        private Block[,] GetFaceBlocks(int face)
        {
            Block[,] returnBlocks = new Block[3, 3];
            switch(face){
                case 0:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            returnBlocks[w,h] = blocks[w, h, 0];
                        }
                    }
                    break;
                case 1:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            returnBlocks[w,h] = blocks[0, w, h];
                        }
                    }
                    break;
            }
            return returnBlocks;
        }

        public void RotateFace(int face)
        {
            Block[,] blocks = GetFaceBlocks(face);
            Vector3 pointOfRotation = Vector3.Zero;

            // Determine point of rotation for vertexes
            foreach(VertexPositionNormalTexture v in blocks[1, 1].vertexes)
            {
                pointOfRotation += v.Position;
            }
            pointOfRotation /= blocks[1, 1].vertexes.Length;

            char axis = ' ';

            switch(face / 2)
            {
                case 0:
                    axis = 'z';
                    break;
                case 1:
                    axis = 'y';
                    break;
                case 2:
                    axis = 'z';
                    break;
            }


            
            foreach(Block b in blocks)
            {
                for(int i = 0; i < b.vertexes.Length; i++)
                {
                    Vector3 orbitPos = b.vertexes[i].Position;
                    float yaw = 0.01f, pitch = 0, roll = 0;
                    float distance = Vector3.Distance(orbitPos, pointOfRotation);
                    Vector3 orbitOffset = orbitPos - pointOfRotation;
                    Matrix rotation = Matrix.CreateFromYawPitchRoll(yaw, pitch, roll);
                    Vector3.Transform(ref orbitOffset, ref rotation, out orbitOffset);
                    b.vertexes[i].Position = pointOfRotation + orbitOffset;
                }
            }
        }
    }
}