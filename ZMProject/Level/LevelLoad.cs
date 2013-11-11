using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
namespace ZMProject
{
    internal class LevelLoad
    {
        public static Level LoadLevelFromLvl(string File)
        {
            Level CurrentLevel = new Level();
            string LastName = string.Empty;
            LevelEntity CurrentEntity = null;
            LevelMesh CurrentMesh = null;
            using (XmlReader readFile = XmlReader.Create(System.IO.File.OpenRead(File)))
            {
                while (readFile.Read())
                {
                    switch (readFile.NodeType)
                    {
                        case XmlNodeType.Element:
                            switch (readFile.Name)
                            {
                                case "Entity":
                                    CurrentEntity = new LevelEntity();
                                    break;
                                case "Mesh":
                                    CurrentMesh = new LevelMesh();
                                    break;
                            }
                            LastName = readFile.Name;
                            break;
                        case XmlNodeType.EndElement:
                            switch (readFile.Name)
                            {
                                case "Entity":
                                    CurrentLevel.Entities.Add(CurrentEntity);
                                    CurrentEntity = null;
                                    break;
                                case "Mesh":
                                    CurrentEntity.Meshes.Add(CurrentMesh);
                                    CurrentMesh = null;
                                    break;
                            }
                            break;
                        case XmlNodeType.Text:
                            switch (LastName)
                            {
                                case "V":
                                    string[] Verts = readFile.Value.Split(' ');
                                    CurrentMesh.Verts.Add(new Microsoft.Xna.Framework.Vector3(Convert.ToSingle(Verts[0]), Convert.ToSingle(Verts[1]), Convert.ToSingle(Verts[2])));
                                    break;
                                case "Material":
                                    CurrentMesh.Material = readFile.Value;
                                    break;
                                case "TextureShift":
                                    string[] Shiftv = readFile.Value.Split(' ');
                                    CurrentMesh.TextureShift = new Microsoft.Xna.Framework.Vector3(Convert.ToSingle(Shiftv[0]), Convert.ToSingle(Shiftv[1]), Convert.ToSingle(Shiftv[2]));
                                    break;
                                case "TextureStretch":
                                    string[] Stre = readFile.Value.Split(' ');
                                    CurrentMesh.TextureStretch = new Microsoft.Xna.Framework.Vector3(Convert.ToSingle(Stre[0]), Convert.ToSingle(Stre[1]), Convert.ToSingle(Stre[2]));
                                    break;
                                case "Position":
                                    string[] Pos = readFile.Value.Split(' ');
                                    CurrentMesh.Position = new Microsoft.Xna.Framework.Vector3(Convert.ToSingle(Pos[0]), Convert.ToSingle(Pos[1]), Convert.ToSingle(Pos[2]));
                                    break;
                                default:
                                    CurrentEntity.EntityProperties.Add(LastName, readFile.Value);
                                    break;
                            }
                            break;
                    }
                }
            }
            return CurrentLevel;
        }
    }
}
