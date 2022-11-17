using OpenTK.Mathematics;

namespace WpfApp1.Model;

public class PlaneNormals
{
    public readonly float[] arrayNormals;
    public readonly Normal[] Normals;


    public PlaneNormals(float[] plane, bool is3dCoord)
    {
        if (is3dCoord)
        {
            Normals = new Normal[plane.Length / 3];
            Normals[0] = new Normal
            (
                new Vector3(plane[^3], plane[^2], plane[^1]),
                new Vector3(plane[0], plane[1], plane[2]),
                new Vector3(plane[3], plane[4], plane[5])
            );

            arrayNormals = new float[plane.Length];

            arrayNormals[0] = Normals[0].EndPointNormirovan.X;
            arrayNormals[1] = Normals[0].EndPointNormirovan.Y;
            arrayNormals[2] = Normals[0].EndPointNormirovan.Z;


            for (int i = 1; i < Normals.Length - 1; i++)
            {
                Normals[i] = new Normal
                (
                    new Vector3(plane[3 * i - 3], plane[3 * i - 2], plane[3 * i - 1]),
                    new Vector3(plane[3 * i], plane[3 * i + 1], plane[3 * i + 2]),
                    new Vector3(plane[3 * i + 3], plane[3 * i + 4], plane[3 * i + 5])
                );

                arrayNormals[3 * i] = Normals[i].EndPointNormirovan.X;
                arrayNormals[3 * i + 1] = Normals[i].EndPointNormirovan.Y;
                arrayNormals[3 * i + 2] = Normals[i].EndPointNormirovan.Z;
            }

            Normals[^1] = new Normal
            (
                new Vector3(plane[^6], plane[^5], plane[^4]),
                new Vector3(plane[^3], plane[^2], plane[^1]),
                new Vector3(plane[0], plane[1], plane[2])
            );

            arrayNormals[^3] = Normals[^1].EndPointNormirovan.X;
            arrayNormals[^2] = Normals[^1].EndPointNormirovan.Y;
            arrayNormals[^1] = Normals[^1].EndPointNormirovan.Z;
        }
        else
        {
            Normals = new Normal[plane.Length / 2];
            Normals[0] = new Normal
            (
                new Vector3(plane[^2], plane[^1], 0),
                new Vector3(plane[0], plane[1], 0),
                new Vector3(plane[2], plane[3], 0)
            );

            for (int i = 1; i < Normals.Length - 1; i++)
            {
                Normals[i] = new Normal
                (
                    new Vector3(plane[2 * i - 2], plane[2 * i - 1], 0),
                    new Vector3(plane[2 * i], plane[2 * i + 1], 0),
                    new Vector3(plane[2 * i + 2], plane[2 * i + 3], 0)
                );
            }

            Normals[^1] = new Normal
            (
                new Vector3(plane[^4], plane[^3], 0),
                new Vector3(plane[^2], plane[^1], 0),
                new Vector3(plane[0], plane[1], 0)
            );
        }
    }
}