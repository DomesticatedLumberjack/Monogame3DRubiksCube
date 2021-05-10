using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame3DRubixCube.Rubix
{
    public class Face
    {
        public Block[,] blocks{get; set;}
        private float rotationProgress;
        private int dim;
        private float rotateSpeed;

        public Face()
        {
            rotationProgress = 0.0f;
            dim = 3;
            rotateSpeed = 0.075f;
            blocks = new Block[dim, dim];
        }

        public bool Rotate(int face, bool reverseRotation)
        {
            bool completed = false;
            float frameRotation = 0.0f; //Rotation done in one frame

            //------------Determine rotation distance if less than rotation speed--------------
            if(rotationProgress + rotateSpeed >= MathHelper.PiOver2)
            {
                frameRotation = MathHelper.PiOver2 - rotationProgress;
                rotationProgress = 0.0f;
                completed = true;
            }
            else{
                frameRotation = rotateSpeed;
                rotationProgress += rotateSpeed;
            }

            //--------------Rotate block placments in 3d array----------------------
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

            //------------------Rotate vertexes about rotation point------------------------

            //Determine point of rotation for face
            Vector3 pointOfRotation = Vector3.Zero;
            foreach(VertexPositionColor v in blocks[1, 1].vertexes)
            {
                pointOfRotation += v.Position;
            }
            pointOfRotation /= blocks[1, 1].vertexes.Length;
            
            //Determine axis of Rotation
            Vector3 rotationAngle = Vector3.Zero;
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

            //Rotate blocks vertexes in 3d space
            foreach(Block b in blocks)
            {
                for(int i = 0; i < b.vertexes.Length; i++)
                {
                    float distance = Vector3.Distance(b.vertexes[i].Position, pointOfRotation);
                    Vector3 orbitOffset = b.vertexes[i].Position - pointOfRotation;
                    Matrix rotation = Matrix.CreateFromYawPitchRoll(rotationAngle.X, rotationAngle.Y, rotationAngle.Z);
                    Vector3.Transform(ref orbitOffset, ref rotation, out orbitOffset);
                    b.vertexes[i].Position = pointOfRotation + orbitOffset;
                }
            }
            return completed;
        }
    }
}