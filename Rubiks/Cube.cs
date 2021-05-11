using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Monogame3DRubiksCube.Rubiks
{
    public class Cube
    {
        public Block[,,] blocks {get; set;}
        private Face face;
        int rotatingFace;
        int dim;
        public Cube()
        {
            dim = 3;
            blocks = new Block[dim, dim, dim];
            face = new Face();
            rotatingFace = -1;

            //Init all blocks
            for(int x = 0; x < dim; x++)
            {
                for(int y = 0; y < dim; y++)
                {
                    for(int z = 0; z < dim; z++)
                    {
                        blocks[x, y, z] = new Block(new Vector3(x * 2, y * 2, z* 2));
                    }
                }
            }
        }

        private Block[,] GetFace(int face)
        {
            Block[,] faceBlocks = new Block[dim, dim];
            switch(face)
            {
                case 0:
                    for(int x = 0; x < dim; x++)
                    {
                        for(int y = 0; y < dim; y++)
                        {
                            faceBlocks[x, y] = blocks[x, y, 0];
                        }
                    }
                    break;
                case 1:
                    for(int x = 0; x < dim; x++)
                    {
                        for(int z = 0; z < dim; z++)
                        {
                            faceBlocks[x, z] = blocks[x, 0, z];
                        }
                    }
                    break;
                case 2:
                    for(int y = 0; y < dim; y++)
                    {
                        for(int z = 0; z < dim; z++)
                        {
                            faceBlocks[y, z] = blocks[0, y, z];
                        }
                    }
                    break;
                case 3:
                    for(int x = 0; x < dim; x++)
                    {
                        for(int y = 0; y < dim; y++)
                        {
                            faceBlocks[x, y] = blocks[x, y, dim - 1];
                        }
                    }
                    break;
                case 4:
                    for(int x = 0; x < dim; x++)
                    {
                        for(int z = 0; z < dim; z++)
                        {
                            faceBlocks[x, z] = blocks[x, dim - 1, z];
                        }
                    }
                    break;
                case 5:
                    for(int y = 0; y < dim; y++)
                    {
                        for(int z = 0; z < dim; z++)
                        {
                            faceBlocks[y, z] = blocks[dim - 1, y, z];
                        }
                    }
                    break;
            }
            return faceBlocks;
        }

        private void SetFace(Block[,] faceBlocks, int face)
        {
            switch(face)
            {
                case 0:
                    for(int x = 0; x < dim; x++)
                    {
                        for(int y = 0; y < dim; y++)
                        {
                            blocks[x, y, 0] = faceBlocks[x, y];
                        }
                    }
                    break;
                case 1:
                    for(int x = 0; x < dim; x++)
                    {
                        for(int z = 0; z < dim; z++)
                        {
                            blocks[x, 0, z] = faceBlocks[x, z];
                        }
                    }
                    break;
                case 2:
                    for(int y = 0; y < dim; y++)
                    {
                        for(int z = 0; z < dim; z++)
                        {
                            blocks[0, y, z] = faceBlocks[y, z];
                        }
                    }
                    break;
                case 3:
                    for(int x = 0; x < dim; x++)
                    {
                        for(int y = 0; y < dim; y++)
                        {
                            blocks[x, y, dim - 1] = faceBlocks[x, y];
                        }
                    }
                    break;
                case 4:
                    for(int x = 0; x < dim; x++)
                    {
                        for(int z = 0; z < dim; z++)
                        {
                            blocks[x, dim - 1, z] = faceBlocks[x, z];
                        }
                    }
                    break;
                case 5:
                    for(int y = 0; y < dim; y++)
                    {
                        for(int z = 0; z < dim; z++)
                        {
                            blocks[dim - 1, y, z] = faceBlocks[y, z];
                        }
                    }
                    break;
            }
        }
        public void Update()
        {
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
                Block[,] returnedBlocks = face.Rotate(GetFace(rotatingFace), rotatingFace);
                if(returnedBlocks != null){ 
                    SetFace(returnedBlocks, rotatingFace);
                    rotatingFace = -1;
                }
            }
        }
    }
}