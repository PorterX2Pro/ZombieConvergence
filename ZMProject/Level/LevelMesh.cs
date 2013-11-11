using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMProject
{
    internal class LevelMesh
    {
        /// <summary>
        /// Center position of the mesh
        /// </summary>
        public Vector3 Position;
        /// <summary>
        /// A list of verts that make up the mesh
        /// </summary>
        public List<Vector3> Verts;
        /// <summary>
        /// The name of the Material without the extension (One name per material extension insensitive)
        /// </summary>
        public string Material;
        /// <summary>
        /// The shift required to make the texture align up
        /// </summary>
        public Vector3 TextureShift;
        /// <summary>
        /// The scale values for a texture
        /// </summary>
        public Vector3 TextureStretch;

        public LevelMesh()
        {
            Verts = new List<Vector3>();
        }

    }
}
