using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMProject
{
    internal class VertexMesh
    {
        public string Material;
        public BoundingBox cBox;
        public VertexBuffer vBuffer;

        public VertexMesh(GraphicsDevice device, List<Vector3> Verts, Vector3 Position, string Mat)
        {
            Material = Mat;
            vBuffer = new VertexBuffer(device, typeof(VertexPositionTexture), Verts.Count, BufferUsage.None);
            List<VertexPositionTexture> cVerts = new List<VertexPositionTexture>();
            int SetupVerts = 1;
            foreach (Vector3 v in Verts)
            {
                switch (SetupVerts)
                {
                    case 1:
                        cVerts.Add(new VertexPositionTexture(Vector3.Transform(v, Matrix.CreateTranslation(Position)), new Vector2(0.0f, 0.0f)));
                        SetupVerts = 2;
                        break;
                    case 2:
                        cVerts.Add(new VertexPositionTexture(Vector3.Transform(v, Matrix.CreateTranslation(Position)), new Vector2(1.0f, 0.0f)));
                        SetupVerts = 3;
                        break;
                    case 3:
                        cVerts.Add(new VertexPositionTexture(Vector3.Transform(v, Matrix.CreateTranslation(Position)), new Vector2(0.0f, 1.0f)));
                        SetupVerts = 4;
                        break;
                    case 4:
                        cVerts.Add(new VertexPositionTexture(Vector3.Transform(v, Matrix.CreateTranslation(Position)), new Vector2(0.0f, 1.0f)));
                        SetupVerts = 5;
                        break;
                    case 5:
                        cVerts.Add(new VertexPositionTexture(Vector3.Transform(v, Matrix.CreateTranslation(Position)), new Vector2(1.0f, 0.0f)));
                        SetupVerts = 6;
                        break;
                    case 6:
                        cVerts.Add(new VertexPositionTexture(Vector3.Transform(v, Matrix.CreateTranslation(Position)), new Vector2(1.0f, 1.0f)));
                        SetupVerts = 1;
                        break;
                }
                
            }
            vBuffer.SetData<VertexPositionTexture>(cVerts.ToArray());
        }
    }
}
