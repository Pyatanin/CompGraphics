using OpenTK.Mathematics;

namespace WpfApp1.Model;

public class PlaneNormals
{
    public readonly Normal[] Normals;

    public PlaneNormals(float[] plane)
    {
        Normals = new Normal[plane.Length / 3];
        Normals[0] = new Normal
        (
            new Vector3(plane[^3], plane[^2], plane[^1]),
            new Vector3(plane[0], plane[1], plane[2]),
            new Vector3(plane[3], plane[4], plane[5])
        );

        for (var i = 1; i < Normals.Length - 1; i++)
        { 
            Normals[i] = new Normal
            (
                new Vector3(plane[3 * i - 3], plane[3 * i - 2], plane[3 * i - 1]),
                new Vector3(plane[3 * i], plane[3 * i + 1], plane[3 * i + 2]),
                new Vector3(plane[3 * i + 3], plane[3 * i + 4], plane[3 * i + 5])
            );

        }
        Normals[^1] = new Normal
        (
            new Vector3(plane[^6], plane[^5], plane[^4]),
            new Vector3(plane[^3], plane[^2], plane[^1]),
            new Vector3(plane[0], plane[1], plane[2])
        );
    }

    public float[] ToArray()
    {
        var normalsArray = new float[Normals.Length * 3];
        for (var i = 0; i < Normals.Length; i++)
        {
            normalsArray[i] = Normals[i].EndPoint.X;
            normalsArray[i + 1] = Normals[i].EndPoint.Y;
            normalsArray[i + 2] = Normals[i].EndPoint.Z;
        }

        return normalsArray;
    }
}