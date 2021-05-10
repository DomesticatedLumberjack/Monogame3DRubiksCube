using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame3DRubixCube.Rubix
{
    public class Cube
    {
        public Block[,,] blocks {get; set;}
        private Face[] faces;
        int rotatingFace;
        public Cube()
        {
            blocks = new Block[3, 3, 3];
            faces = new Face[6];
            rotatingFace = -1;

            //Init all blocks
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

            //Init faces
            for(int i = 0; i < faces.Length; i++)
            {
                int face = i;
                faces[face] = new Face();
                switch(face){
                case 0:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            faces[face].blocks[w,h] = blocks[w, h, 0];
                        }
                    }
                    break;
                case 1:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            faces[face].blocks[w,h] = blocks[0, w, h];
                        }
                    }
                    break;
                case 2:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            faces[face].blocks[w,h] = blocks[w, 0, h];
                        }
                    }
                    break;
                case 3:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            faces[face].blocks[w,h] = blocks[w, h, 2];
                        }
                    }
                    break;
                case 4:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            faces[face].blocks[w,h] = blocks[2, w, h];
                        }
                    }
                    break;
                case 5:
                    for(int w = 0; w < 3; w++)
                    {
                        for(int h = 0; h < 3; h++)
                        {
                            faces[face].blocks[w,h] = blocks[w, 2, h];
                        }
                    }
                    break;
            }
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
                bool rotationCompleted = faces[rotatingFace].Rotate(rotatingFace, false);
                if(rotationCompleted) rotatingFace = -1;
            }
        }
    }
}