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

        private Vector3 AveragePoint(VertexPositionNormalTexture[] vertexPositionNormalTextures)
        {
            Vector3 avgPoint = Vector3.Zero;
            ReadOnlySpan<VertexPositionNormalTexture> span = vertexPositionNormalTextures.AsSpan();
            foreach(VertexPositionNormalTexture v in span)
            {
                avgPoint += v.Position;
            }
            avgPoint /= vertexPositionNormalTextures.Length;

            return avgPoint;
        }

        private Vector3 RotateAboutPoint(Vector3 pointOfRotation, Vector3 initPosition, float angle)
        {
            Vector3 finalPoint = Vector3.Zero;

            finalPoint.X = (initPosition.X * MathF.Cos(angle)) - (initPosition.Y * MathF.Sin(angle));
            finalPoint.Y = (initPosition.Y * MathF.Cos(angle)) + (initPosition.Y * MathF.Sin(angle));

            return finalPoint;
        }

        public void RotateFace(int face)
        {
            Block[,] blocks = GetFaceBlocks(face);
            Vector3 rotationPoint = AveragePoint(blocks[1,1].vertexPositions);
            
            foreach(Block b in blocks)
            {
                for(int i = 0; i < b.vertexPositions.Length; i++)
                {
                    b.vertexPositions[i].Position = RotateAboutPoint(rotationPoint, b.vertexPositions[i].Position, 0.01f);
                }
            }
        }
    }
}