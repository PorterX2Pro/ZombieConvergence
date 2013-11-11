using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMProject
{
    internal class LevelEntity
    {
        /// <summary>
        /// Properties of an entity other then meshes
        /// </summary>
        public Dictionary<string, string> EntityProperties;
        /// <summary>
        /// A list of meshes contained in the current entity
        /// </summary>
        public List<LevelMesh> Meshes;

        public LevelEntity()
        {
            EntityProperties = new Dictionary<string, string>();
            Meshes = new List<LevelMesh>();
        }
    }
}
