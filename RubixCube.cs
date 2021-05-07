using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RubixSolver
{
    public class RubixCube
    {
        public Block[,,] blocks {get; set;}
        int rotatingFace;
        float rotateSpeed;
        float rotationProgress;
        public RubixCube()
        {
            blocks = new Block[3, 3, 3];
            rotateSpeed = 0.05f;
            rotatingFace = -1;
            rotationProgress = 0.0f;
            
            for(int x = 0; x < 3; x++)
            {
                for(int y = 0; y < 3; y++)
                {
                    for(int z = 0; z < 3; z++)
                    {
                        blocks[x, y, z] = new Block(new Vector3(x * 2, y * 2, z* 2), new Vector3(x, y, z));
                    }
                }
            }
        }

        public void Update()
        {
            Random random = new Random();
            if(rotatingFace == -1){
                if(Keyboard.GetState().IsKeyDown(Keys.D1))
                    rotatingFace = 0;
                if(Keyboard.GetState().IsKeyDown(Keys.D2))
                    rotatingFace = 1;
                if(Keyboard.GetState().IsKeyDown(Keys.D3))
                    rotatingFace = 2;
                if(Keyboard.GetState().IsKeyDown(Keys.D4))
                    rotatingFace = 3;
                if(Keyboard.GetState().IsKeyDown(Keys.D5))
                    rotatingFace = 4;
                if(Keyboard.GetState().IsKeyDown(Keys.D6))
                    rotatingFace = 5;
            }
            else{
                RotateFace(rotatingFace);
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
                case 2:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            returnBlocks[w,h] = blocks[w, 0, h];
                        }
                    }
                    break;
                case 3:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            returnBlocks[w,h] = blocks[w, h, 2];
                        }
                    }
                    break;
                case 4:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            returnBlocks[w,h] = blocks[2, h, w];
                        }
                    }
                    break;
                case 5:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            returnBlocks[w,h] = blocks[h, 2, w];
                        }
                    }
                    break;
            }
            return returnBlocks;
        }

        private void SetFaceBlocks(Block[,] rotatedBlocks, int face)
        {
            switch(face){
                case 0:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            blocks[w, h, 0] = rotatedBlocks[w, h];
                        }
                    }
                    break;
                case 1:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            blocks[0, w, h] = rotatedBlocks[w, h];
                        }
                    }
                    break;
                case 2:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            blocks[w, 0, h] = rotatedBlocks[w, h];
                        }
                    }
                    break;
                case 3:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            blocks[w, h, 2] = rotatedBlocks[w, h];
                        }
                    }
                    break;
                case 4:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            blocks[2, w, h] = rotatedBlocks[w, h];
                        }
                    }
                    break;
                case 5:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            blocks[w, 2, h] = rotatedBlocks[w, h];
                        }
                    }
                    break;
            }
        }

        private void RotateFace(int face)
        {
            Block[,] faceBlocks = GetFaceBlocks(face);
            Vector3 pointOfRotation = Vector3.Zero;
            Vector3 rotationAngle = Vector3.Zero;

            float frameRotation = 0.0f;

            //Lock rotation into pi/2 from init point
            if(rotationProgress + rotateSpeed >= MathHelper.PiOver2)
            {
                frameRotation = MathHelper.PiOver2 - rotationProgress;
                rotatingFace = -1;
                rotationProgress = 0.0f;

                int N = 3;
                //Update blocks in 3d array
                for (int x = 0; x < N / 2; x++) {
                    for (int y = x; y < N - x - 1; y++) {
                        // store current cell
                        // in temp variable
                        Block temp = faceBlocks[x, y];
                        // move values from
                        // right to top
                        faceBlocks[x, y] = faceBlocks[y, N - 1 - x];
                        // move values from
                        // bottom to right
                        faceBlocks[y, N - 1 - x] = faceBlocks[N - 1 - x, N - 1 - y];
                        // move values from
                        // left to bottom
                        faceBlocks[N - 1 - x, N - 1 - y] = faceBlocks[N - 1 - y, x];
                        // assign temp to left
                        faceBlocks[N - 1 - y, x] = temp;
                    }
                }
                SetFaceBlocks(faceBlocks, face);
            }
            else{
                frameRotation = rotateSpeed;
                rotationProgress += rotateSpeed;
            }

            // Determine point of rotation for vertexes
            foreach(VertexPositionColor v in faceBlocks[1, 1].vertexes)
            {
                pointOfRotation += v.Position;
            }
            pointOfRotation /= faceBlocks[1, 1].vertexes.Length;
            switch(face)
            {
                case 0 or 3:
                    rotationAngle.Z = frameRotation;
                    break;
                case 1 or 4:
                    rotationAngle.Y = frameRotation;
                    break;
                case 2 or 5:
                    rotationAngle.X = frameRotation;
                    break;
            }
            foreach(Block b in faceBlocks)
            {
                //Rotate blocks vertexes in 3d space
                for(int i = 0; i < b.vertexes.Length; i++)
                {
                    float distance = Vector3.Distance(b.vertexes[i].Position, pointOfRotation);
                    Vector3 orbitOffset = b.vertexes[i].Position - pointOfRotation;
                    Matrix rotation = Matrix.CreateFromYawPitchRoll(rotationAngle.X, rotationAngle.Y, rotationAngle.Z);
                    Vector3.Transform(ref orbitOffset, ref rotation, out orbitOffset);
                    b.vertexes[i].Position = pointOfRotation + orbitOffset;
                }
            }

            
        }
    }
}