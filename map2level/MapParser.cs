using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;

namespace map2level
{
    public class entity
    {
        public List<List<Vector3>> brushes = new List<List<Vector3>>();
        public List<Vector2> brushTextureShifts = new List<Vector2>();
        public List<Vector2> brushTextureStretches = new List<Vector2>();

        public List<string> BrushTextures = new List<string>();
        public List<int> hiddenSurfaceStartVerts = new List<int>();

        public List<string> properties = new List<string>();
        public List<string> propertyValues = new List<string>();

        public List<float> brushSizes = new List<float>();

        public int optimize(int maxJoin, float maxSize)
        {

            int startBrushCount = brushes.Count;
            calcBrushSizes();

            List<List<Vector3>> NewBrushes = new List<List<Vector3>>();
            List<Vector2> NewBrushTextureShifts = new List<Vector2>();
            List<Vector2> NewBrushTextureStretches = new List<Vector2>();

            List<string> NewBrushTextures = new List<string>();
            List<int> NewHiddenSurfaceStartVerts = new List<int>();

            while (brushes.Count > 0)
            {
                List<int> indexesToMerge = new List<int>();
                string mergeTexture = BrushTextures[0];
                for (int i = 0; i < brushes.Count; i++)
                {
                    if (brushSizes[0] > maxSize)
                    {
                        indexesToMerge.Add(0);
                        break;
                    }

                    if (indexesToMerge.Count > maxJoin) break;
                    if (BrushTextures[i] == mergeTexture && brushSizes[i] < maxSize)
                    {
                        indexesToMerge.Add(i - indexesToMerge.Count);
                    }
                }

                List<Vector3> mergedBrush = new List<Vector3>();

                for (int i = 0; i < indexesToMerge.Count; i++)
                {
                    for (int j = 0; j < brushes[indexesToMerge[i] + i].Count; j++)
                    {
                        mergedBrush.Add(brushes[indexesToMerge[i] + i][j]);
                    }
                }
                NewBrushes.Add(mergedBrush);

                if (brushTextureShifts.Count > indexesToMerge[0])
                {
                    NewBrushTextureShifts.Add(brushTextureShifts[indexesToMerge[0]]);
                }

                if (brushTextureStretches.Count > indexesToMerge[0])
                {
                    NewBrushTextureStretches.Add(brushTextureStretches[indexesToMerge[0]]);
                }

                NewBrushTextures.Add(BrushTextures[indexesToMerge[0]]);
                //NewHiddenSurfaceStartVerts.Add(hiddenSurfaceStartVerts[indexesToMerge[0]]);

                for (int i = 0; i < indexesToMerge.Count; i++)
                {
                    brushes.RemoveAt(indexesToMerge[i]);

                    if (brushTextureShifts.Count > indexesToMerge[i])
                    {
                        brushTextureShifts.RemoveAt(indexesToMerge[i]);
                    }
                    if (brushTextureStretches.Count > indexesToMerge[i])
                    {
                        brushTextureStretches.RemoveAt(indexesToMerge[i]);
                    }
                    BrushTextures.RemoveAt(indexesToMerge[i]);

                }

            }

            brushes = NewBrushes;
            BrushTextures = NewBrushTextures;
            brushTextureShifts = NewBrushTextureShifts;
            brushTextureStretches = NewBrushTextureStretches;

            return startBrushCount - brushes.Count;
        }

        public void calcBrushSizes()
        {
            Vector3 high = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            Vector3 low = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 v;
            for (int i = 0; i < brushes.Count; i++)
            {
                low = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
                high = new Vector3(float.MinValue, float.MinValue, float.MinValue);

                for (int j = 0; j < brushes[i].Count; j++)
                {
                    v = brushes[i][j];
                    if (v.X > high.X) high.X = v.X;
                    if (v.Y > high.Y) high.Y = v.Y;
                    if (v.Z > high.Z) high.Z = v.Z;
                    if (v.X < low.X) low.X = v.X;
                    if (v.Y < low.Y) low.Y = v.Y;
                    if (v.Z < low.Z) low.Z = v.Z;
                }

                brushSizes.Add(Vector3.Distance(high, low));
            }
        }

    }

    public class MapParser
    {
        public MapParser()
        {
        }

        List<entity> entities = new List<entity>();
        List<int> HiddenSurfaceStartPoly = new List<int>();

        List<string> BrushTextures = new List<string>();

        public void parse(string filename)
        {
            StreamReader re = File.OpenText(filename);
            string input = null;
            List<Vector3> verticies = new List<Vector3>();
            int brushCount = 0;
            //bool poly = false;

            while ((input = re.ReadLine()) != null)
            {
                
                //Open entity
                if (input.Contains("{"))
                {
                    entity newEntity = new entity();
                    while ((input = re.ReadLine()) != "}")
                    {

                        //Brush
                        if (input.Contains("{"))
                        {
                            string brushTexture = "";
                            float vertShift = 0;
                            float horzShift = 0;
                            float vertStretch = 0;
                            float horzStretch = 0;
                            List<Plane> brushPlanes = new List<Plane>();

                            //Read in a brush
                            while ((input = re.ReadLine()) != "}")
                            {
                                if (input.Contains("("))
                                {
                                    try
                                    {
                                        //( 120 128 0 ) ( 120 -128 0 ) ( -136 128 0 ) e1u2/basic1_3 8 0 0 1 1
                                        string[] points = input.Split('(');
                                        string[] values = points[1].Split(' ');
                                        Vector3 point1 = new Vector3(float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]));
                                        values = points[2].Split(' ');
                                        Vector3 point2 = new Vector3(float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]));
                                        values = points[3].Split(' ');
                                        Vector3 point3 = new Vector3(float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]));

                                        brushPlanes.Add(new Plane(point3, point2, point1));

                                        string[] texture = input.Split(')');
                                        brushTexture = texture[3].Split(' ')[1];
                                        horzShift = float.Parse(texture[3].Split(' ')[2]);
                                        vertShift = float.Parse(texture[3].Split(' ')[3]);

                                        horzStretch = float.Parse(texture[3].Split(' ')[5]);
                                        vertStretch = float.Parse(texture[3].Split(' ')[6]);
                                    }
                                    catch
                                    {
                                        break;
                                    }
                                }
                            }
                            if (brushPlanes.Count > 0)
                            {
                                List<Vector3> vertListT = planeToVerts(brushPlanes);

                                if (vertListT != null)
                                {
                                    newEntity.brushes.Add(vertListT);
                                    brushCount++;
                                    newEntity.BrushTextures.Add(brushTexture);
                                    newEntity.brushTextureShifts.Add(new Vector2(horzShift,vertShift));
                                    newEntity.brushTextureStretches.Add(new Vector2(horzStretch, vertStretch));
                                }

                            }
                        }

                        //Entity Data
                        if (input.StartsWith("\""))
                        {
                            string[] entityData = input.Split('\"');
                            newEntity.properties.Add(entityData[1]);
                            newEntity.propertyValues.Add(entityData[3]);
                        }
                    }
                    entities.Add(newEntity);
                }
                

            }
            
            /*
            form.stats.Text += "Removing Hiden Surfaces...\n";
            RemoveHiddenSurfaces();
            form.stats.Text += "Surfaces Removed :" + surfacesRemoved + "\n";
            */

            re.Close();

        }

        int totalVerts = 0;
        int setCount = 0;
        int nullCount = 0;
        public List<Vector3> planeToVerts(List<Plane> planes)
        {

            List<List<Vector3>> Polys = new List<List<Vector3>>();
            for (int i = 0; i < planes.Count; i++)
            {
                Polys.Add(new List<Vector3>());
            }

            Vector3 point;
            for (int i = 0; i < planes.Count - 2; i++)
            {
                for (int j = i + 1; j < planes.Count - 1; j++)
                {
                    for (int k = j + 1; k < planes.Count; k++)
                    {
                        bool legal = true;
                        point = intersectThreePlanes(planes[i], planes[j], planes[k]);

                        List<Vector3> tempnorm = new List<Vector3>();
                        for (int m = 0; m < planes.Count; m++)
                        {
                            tempnorm.Add(planes[m].Normal);
                            float dot = Vector3.Dot(planes[m].Normal, point);
                            if (dot + planes[m].D > 0.001f || float.IsNaN(dot))
                            {
                                legal = false;
                            }
                        }
                        if (legal)
                        {
                            Polys[i].Add(point); // Add vertex to
                            Polys[j].Add(point); // 3 polygons
                            Polys[k].Add(point); // at a time
                        }
                    }
                }
            }

            List<Vector3> verts = new List<Vector3>();
            for (int i = 0; i < Polys.Count; i++)
            {
                Vector3 center = Vector3.Zero;
                for (int n = 0; n < Polys[i].Count; n++)
                {
                    center += Polys[i][n];
                }
                center = center / Polys[i].Count;

                //Sort the points
                for (int n = 0; n < Polys[i].Count - 2; n++)
                {
                    Vector3 a = Vector3.Normalize(Polys[i][n] - center);
                    Plane p = new Plane(Polys[i][n], center, center + planes[i].Normal);

                    double SmallestAngle = -1;
                    int Smallest = -1;
                    for (int m = n + 1; m < Polys[i].Count; m++)
                    {

                        if (p.Normal.X * Polys[i][m].X + p.Normal.Y * Polys[i][m].Y + p.Normal.Z * Polys[i][m].Z + p.D > 0)
                        {
                            Vector3 b = Vector3.Normalize(Polys[i][m] - center);
                            double Angle = Vector3.Dot(a, b);
                            if (Angle > SmallestAngle)
                            {
                                SmallestAngle = Angle;
                                Smallest = m;
                            }

                        }
                    }
                    if (Smallest != -1)
                    {
                        swap(Polys[i], n + 1, Smallest);
                    }
                }

                //Remove Dupe Points
                for (int n = 0; n < Polys[i].Count; n++)
                {
                    for (int m = n+1; m < Polys[i].Count; m++)
                    {
                        if (almostEqual(Polys[i][n], Polys[i][m]))
                        {
                            Polys[i].RemoveAt(m);
                            m--;
                        }
                    }

                }

                //Create Triangles
                Vector3 vert1, vert2, vert3, n1, n2, normal;
                if (Polys[i].Count == 4)
                {
                    vert1 = Polys[i][0];
                    vert2 = Polys[i][1];
                    vert3 = Polys[i][3];
                    n1 = vert3 - vert1;
                    n2 = vert2 - vert1;
                    normal = Vector3.Normalize(Vector3.Cross(n1, n2));
                    if (normal != planes[i].Normal)
                    {
                        verts.Add(vert3);
                        verts.Add(vert2);
                        verts.Add(vert1);
                    }
                    else
                    {
                        verts.Add(vert1);
                        verts.Add(vert2);
                        verts.Add(vert3);
                    }

                    vert1 = Polys[i][1];
                    vert2 = Polys[i][2];
                    vert3 = Polys[i][3];
                    n1 = vert3 - vert1;
                    n2 = vert2 - vert1;
                    normal = Vector3.Normalize(Vector3.Cross(n1, n2));
                    if (normal != planes[i].Normal)
                    {
                        verts.Add(vert3);
                        verts.Add(vert2);
                        verts.Add(vert1);
                    }
                    else
                    {
                        verts.Add(vert1);
                        verts.Add(vert2);
                        verts.Add(vert3);
                    }

                }
                else if (Polys[i].Count == 3)
                {
                    vert1 = Polys[i][0];
                    vert2 = Polys[i][1];
                    vert3 = Polys[i][2];
                    n1 = vert3 - vert1;
                    n2 = vert2 - vert1;
                    normal = Vector3.Normalize(Vector3.Cross(n1, n2));
                    if (normal != planes[i].Normal)
                    {
                        verts.Add(vert3);
                        verts.Add(vert2);
                        verts.Add(vert1);
                    }
                    else
                    {
                        verts.Add(vert1);
                        verts.Add(vert2);
                        verts.Add(vert3);
                    }

                } else {
                    for (int j = 0; j < Polys[i].Count; j++)
                    {
                        vert1 = center;
                        vert2 = Polys[i][j];
                        int next = j + 1;
                        if (next == Polys[i].Count)
                        {
                            next = 0;
                        }
                        vert3 = Polys[i][next];

                        n1 = vert3 - vert1;
                        n2 = vert2 - vert1;
                        normal = Vector3.Normalize(Vector3.Cross(n1, n2));
                        if (normal != planes[i].Normal)
                        {
                            verts.Add(vert3);
                            verts.Add(vert2);
                            verts.Add(vert1);
                        }
                        else
                        {
                            verts.Add(vert1);
                            verts.Add(vert2);
                            verts.Add(vert3);
                        }
                    }
                }

            }


            if (verts.Count == 0)
            {
                nullCount++;
                return null;
            }

            totalVerts += verts.Count;

            return verts;

        }

        public void swap(List<Vector3> vecList, int indexa, int indexb)
        {
            Vector3 temp = vecList[indexa];
            vecList[indexa] = vecList[indexb];
            vecList[indexb] = temp;
        }

        public Vector3 intersectThreePlanes(Plane p1, Plane p2, Plane p3)
        {

            return (-p1.D * Vector3.Cross(p2.Normal, p3.Normal) - p2.D * Vector3.Cross(p3.Normal, p1.Normal) - p3.D * Vector3.Cross(p1.Normal, p2.Normal)) /
                (Vector3.Dot(p1.Normal, Vector3.Cross(p2.Normal, p3.Normal)));
        }

        /*
        int surfacesRemoved = 0;
        public void RemoveHiddenSurfaces()
        {
            List<List<Vector3>> brushCopy = new List<List<Vector3>>();
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].isBrush){
                    List<Vector3> brushData = new List<Vector3>();
                    brushData.AddRange(entities[i].brush);
                    brushCopy.Add(brushData);
                }
            }

            //Check All Brushes Against Eachother
            Vector3 v1, v2, v3, cv1, cv2, cv3;
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].isBrush)
                {
                    List<Vector3> verts = entities[i].brush;
                    List<Vector3> replacementVerts = new List<Vector3>();
                    List<Vector3> hiddenVerts = new List<Vector3>();
                    for (int face = 0; face < verts.Count / 3; face++)
                    {
                        bool remove = false;
                        v1 = verts[3 * face];
                        v2 = verts[3 * face + 1];
                        v3 = verts[3 * face + 2];
                        for (int j = 0; j < brushCopy.Count; j++)
                        {
                            if (i != j)
                            {
                                if (remove == true)
                                {
                                    break;
                                }

                                List<Vector3> cverts = brushCopy[j];
                                for (int cface = 0; cface < cverts.Count / 3; cface++)
                                {
                                    cv1 = cverts[3 * cface];
                                    cv2 = cverts[3 * cface + 1];
                                    cv3 = cverts[3 * cface + 2];

                                    if (almostEqual(v1, cv1) || almostEqual(v1, cv2) || almostEqual(v1, cv3))
                                    {
                                        if (almostEqual(v2, cv1) || almostEqual(v2, cv2) || almostEqual(v2, cv3))
                                        {
                                            if (almostEqual(v3, cv1) || almostEqual(v3, cv2) || almostEqual(v3, cv3))
                                            {
                                                //Remove v1,v2,v3
                                                surfacesRemoved++;
                                                remove = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                        }

                        if (remove == false)
                        {
                            replacementVerts.Add(v1);
                            replacementVerts.Add(v2);
                            replacementVerts.Add(v3);
                        }
                        else
                        {
                            hiddenVerts.Add(v1);
                            hiddenVerts.Add(v2);
                            hiddenVerts.Add(v3);
                        }

                    }

                    replacementVerts.AddRange(hiddenVerts);
                    entities[i].brush = replacementVerts;
                    entities[i].hiddenSurfaceStartVert = replacementVerts.Count / 3;
                }
            }

            if (surfacesRemoved > 0)
            {
                //yay
            }

        }
        */

        public bool almostEqual(Vector3 p1, Vector3 p2)
        {
            if (Vector3.Distance(p1, p2) < 0.001f)
            {
                return true;
            }
            return false;
        }

        /*
        public void FinalizeParse()
        {

            for (int i = 0; i < brushes.Count; i++)
            {
                Renderable newRenderable = new Renderable(DrawableObjectManager.Instance.createCustomMesh(brushes[i], "Level" + setCount++));
                newRenderable.setScale(0.7f);
                Random random = new Random();
                newRenderable.setPosition(0.01f * (float)random.NextDouble(), -10 + 0.01f * (float)random.NextDouble(), 0.01f * (float)random.NextDouble());
                renderables.Add(newRenderable);
            }
        }
         */

        public string OutputMapXML(float scale, StreamWriter output, int maxOptimizeJoin, float maxOptimizeSize)
        {

            //rotate so Y is up
            Matrix Rotation = CreateYawPitchRoll(0, -1.57f, 0);
            Matrix Scale = Matrix.CreateScale(scale);
            Matrix trans = Scale * Rotation;

            if (maxOptimizeJoin > 0)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    for (int j = 0; j < entities[i].properties.Count; j++)
                    {
                        if (entities[i].properties[j] == "classname")
                        {
                            if (entities[i].propertyValues[j] == "func_room")
                            {
                                int opCount = entities[i].optimize(maxOptimizeJoin, maxOptimizeSize);
                            }
                        }
                    }
                }
            }

            string outputXML = "";

            for (int i = 0; i < entities.Count; i++)
            {
                outputXML = "<Entity>\n";

                for (int j = 0; j < entities[i].properties.Count; j++)
                {
                    if (entities[i].properties[j] == "origin")
                    {
                        string[] Vvalues = entities[i].propertyValues[j].Split(' ');
                        Vector3 origin = new Vector3(float.Parse(Vvalues[0]), float.Parse(Vvalues[1]), float.Parse(Vvalues[2]));
                        origin = Vector3.Transform(origin, trans);
                        outputXML += "<" + entities[i].properties[j] + ">" + origin.X + " " + origin.Y + " " + origin.Z + "</" + entities[i].properties[j] + ">\n";

                    }
                    else
                    {
                        outputXML += "<" + entities[i].properties[j] + ">" + entities[i].propertyValues[j] + "</" + entities[i].properties[j] + ">\n";
                    }

                    output.Write(outputXML);
                    outputXML = "";
                }

                for (int w = 0; w < entities[i].brushes.Count; w++)
                {
                    outputXML += "<Mesh>\n";
                    //Find the brush Center
                    Vector3 center = Vector3.Zero;
                    for (int j = 0; j < entities[i].brushes[w].Count; j++)
                    {
                        entities[i].brushes[w][j] = Vector3.Transform(entities[i].brushes[w][j], trans);
                        center += entities[i].brushes[w][j];
                    }
                    center = center / entities[i].brushes[w].Count;

                    outputXML += "\t<Position>";
                    outputXML += "" + center.X + " " + center.Y + " " + center.Z;
                    outputXML += "</Position>\n";

                    //Translate
                    for (int j = 0; j < entities[i].brushes[w].Count; j++)
                    {
                        outputXML += "\t<V>";
                        entities[i].brushes[w][j] -= center;
                        outputXML += entities[i].brushes[w][j].X + " " + entities[i].brushes[w][j].Y + " " + entities[i].brushes[w][j].Z;
                        outputXML += "</V>\n";
                    }
                    //outputXML += "<HiddenSurfaceStart>" + entities[i].hiddenSurfaceStartVerts[w] + "</HiddenSurfaceStart>\n";
                    outputXML += "<Material>" + entities[i].BrushTextures[w] + "</Material>\n";
                    outputXML += "<TextureShift>" + (entities[i].brushTextureShifts[w].X)+" "+(entities[i].brushTextureShifts[w].Y)+ " 0 </TextureShift>\n";
                    outputXML += "<TextureStretch>" + (entities[i].brushTextureStretches[w].X * scale) + " " + (entities[i].brushTextureStretches[w].Y * scale) + " 0 </TextureStretch>\n";

                    outputXML += "</Mesh>\n";

                    output.Write(outputXML);
                    outputXML = "";
                }

                outputXML += "</Entity>\n";
                
                output.Write(outputXML);
            }

            return outputXML;

        }

        public Matrix CreateYawPitchRoll(float yaw, float pitch, float roll)
        {
            Matrix yawMatrix = Matrix.CreateRotationY(yaw);
            Matrix pitchMatrix = Matrix.CreateRotationX(pitch);
            Matrix rollMatrix = Matrix.CreateRotationZ(roll);

            Matrix combined = Matrix.Multiply(Matrix.Multiply(yawMatrix, pitchMatrix), rollMatrix);
            return combined;
        }

    }
}
