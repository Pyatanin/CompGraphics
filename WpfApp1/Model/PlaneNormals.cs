using System.Collections.Generic;
using OpenTK.Mathematics;

namespace WpfApp1.Model;

public class PlaneNormals
{
    public readonly Normal[] Normals;

    public PlaneNormals(float[] plane)
    {
        Normals = new Normal[plane.Length/3];
        Normals[0] = new Normal
        (
            new Vector3(plane[^3], plane[^2], plane[^1]),
            new Vector3(plane[0], plane[1], plane[2]),
            new Vector3(plane[3], plane[4], plane[5])
        );
        
        for (int i = 1; i < Normals.Length - 1; i++)
        {
            Normals[i] = new Normal
            (
                new Vector3(plane[i - 3], plane[i - 2], plane[i - 1]),
                new Vector3(plane[i], plane[i + 1], plane[i + 2]),
                new Vector3(plane[i + 3], plane[i + 4], plane[i + 5])
            );
        }

        Normals[^1] = new Normal
        (
            new Vector3(plane[0], plane[1], plane[2]),
            new Vector3(plane[^3], plane[^2], plane[^1]),
            new Vector3(plane[^6], plane[^5], plane[^4])
        );
    }
}