using Microsoft.Xna.Framework;

namespace Monogame3DRubiksCube.Rubiks
{
    public class Face
    {
        private static int totalFaces;
        private float rotationProgress;
        private int dim;
        private float rotateSpeed;
        private Vector3 pointOfRotation;
        private int faceNumber;


        public Face(int dim, Block pointOfRotationBlock)
        {
            rotationProgress = 0.0f;
            this.dim = dim;
            rotateSpeed = -0.075f;
            faceNumber = totalFaces++;
            pointOfRotation = pointOfRotationBlock.GetBlockCenter();
        }

        public Block[,] RotateFace(Block[,] blocks, int face)
        {
            Block[,] returnBlocks = null;
            bool completed = false;
            float frameRotation = 0.0f; //Rotation done in one frame

            //------------Determine rotation distance if less than rotation speed--------------
            if(rotationProgress - rotateSpeed <= -MathHelper.PiOver2)
            {
                frameRotation = -MathHelper.PiOver2 - rotationProgress;
                rotationProgress = 0.0f;
                completed = true;
                returnBlocks = blocks;
            }
            else{
                frameRotation = rotateSpeed;
                rotationProgress += rotateSpeed;
            }

            //--------------Rotate block placments in 2d array----------------------
            if(completed){
                for (int i=0; i <= (dim - 1)/2; i++) {
                    for (int j=i; j < dim - i - 1; j++) {
                        Block p1 = blocks[i,j];
                        Block p2 = blocks[j, dim-i-1];
                        Block p3 = blocks[dim-i-1, dim-j-1];
                        Block p4 = blocks[dim-j-1, i];

                        blocks[j, dim-i-1] = p1;
                        blocks[dim-i-1, dim-j-1] = p2;
                        blocks[dim-j-1, i] = p3;
                        blocks[i, j] = p4;
                    }
                }
            }

            //------------------Rotate blocks about rotation point------------------------            
            //Determine axis of Rotation
            Vector3 rotationAngle = Vector3.Zero;
            switch(face)
            {
                case 0 or 3:
                    rotationAngle.Z = frameRotation;
                    break;
                case 1 or 4:
                    rotationAngle.X = -frameRotation;
                    break;
                case 2 or 5:
                    rotationAngle.Y = frameRotation;
                    break;
            }

            //Rotate blocks in 3d space
            foreach(Block b in blocks)
            {
                b.RotateAboutPoint(pointOfRotation, rotationAngle); 
            }
            return returnBlocks;
        }
    }
}