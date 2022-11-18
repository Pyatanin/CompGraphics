using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL;
namespace WpfApp1.Model;

public class Figure3D
{
    public readonly float[] TextureOverlayCoordinates;
    public readonly float[] BasicPlane;
    public readonly float[][] Edges;
    public readonly float[] ReplicatedPlane;

    public readonly float[] RotationVector;
    public readonly float[] ScaleVector;
    public readonly PlaneNormals BasicPlaneNormals;
    public readonly PlaneNormals ReplicatedPlaneNormals;
    public readonly PlaneNormals[] EdgesNormals;
    public readonly Texture2D BasicPlaneTexture;
    public readonly Texture2D ReplicatedPlaneTexture;
    public readonly Texture2D EdgesTexture;

    public Figure3D(float[] basicPlane, float[] replicationVector, float[] rotationVector, float[] scaleVector,
        float[] textureOverlayCoordinates, string basicPlaneTexture, string replicatedPlaneTexture, string edgeTexture)
    {
        RotationVector = rotationVector;
        ScaleVector = scaleVector;

        TextureOverlayCoordinates = textureOverlayCoordinates;

        BasicPlane = new float[3 * basicPlane.Length / 2];
        ReplicatedPlane = new float[3 * basicPlane.Length / 2];
        var offset = 0;
        if (replicationVector[^1] < 0)
        {
            offset = 0;
            for (var i = 0; i < basicPlane.Length; i += 2)
            {
                BasicPlane[i + offset] = scaleVector[0] * basicPlane[i];
                BasicPlane[i + 1 + offset] = scaleVector[1] * basicPlane[i + 1];
                BasicPlane[i + 2 + offset] = 0;
                offset++;
            }

            offset = basicPlane.Length / 2;
            for (var i = -2 + basicPlane.Length; i >= 0; i -= 2)
            {
                ReplicatedPlane[^(i + offset + 2)] = scaleVector[0] * (basicPlane[i] + replicationVector[0]);
                ReplicatedPlane[^(i + offset + 1)] = scaleVector[1] * (basicPlane[i + 1] + replicationVector[1]);
                ReplicatedPlane[^(i + offset)] = scaleVector[2] * replicationVector[2];
                offset--;
            }
        }
        else
        {
            offset = basicPlane.Length / 2;
            for (var i = -2 + basicPlane.Length; i >= 0; i -= 2)
            {
                BasicPlane[^(i + offset + 2)] = scaleVector[0] * basicPlane[i];
                BasicPlane[^(i + offset + 1)] = scaleVector[1] * basicPlane[i + 1];
                BasicPlane[^(i + offset)] = 0;
                offset--;
            }

            offset = 0;
            for (var i = 0; i < basicPlane.Length; i += 2)
            {
                ReplicatedPlane[i + offset] = scaleVector[0] * (basicPlane[i] + replicationVector[0]);
                ReplicatedPlane[i + offset + 1] = scaleVector[0] * (basicPlane[i + 1] + replicationVector[1]);
                ReplicatedPlane[i + offset + 2] = scaleVector[2] * replicationVector[2];
                offset++;
            }
        }

        Edges = new float[basicPlane.Length / 2][];
        for (var i = 0; i < basicPlane.Length / 2 - 1; i++)
        {
            Edges[i] = new float[12];
            Edges[i][0] = scaleVector[0] * basicPlane[2 * i];
            Edges[i][1] = scaleVector[1] * basicPlane[2 * i + 1];
            Edges[i][2] = 0;

            Edges[i][3] = scaleVector[0] * basicPlane[2 * i + 2];
            Edges[i][4] = scaleVector[1] * basicPlane[2 * i + 3];
            Edges[i][5] = 0;

            Edges[i][6] = scaleVector[0] * (basicPlane[2 * i + 2] + replicationVector[0]);
            Edges[i][7] = scaleVector[1] * (basicPlane[2 * i + 3] + replicationVector[1]);
            Edges[i][8] = scaleVector[2] * replicationVector[2];

            Edges[i][9] = scaleVector[0] * (basicPlane[2 * i] + replicationVector[0]);
            Edges[i][10] = scaleVector[1] * (basicPlane[2 * i + 1] + replicationVector[1]);
            Edges[i][11] = scaleVector[2] * replicationVector[2];
        }

        Edges[^1] = new float[12];
        Edges[^1][0] = scaleVector[0] * basicPlane[^2];
        Edges[^1][1] = scaleVector[1] * basicPlane[^1];
        Edges[^1][2] = 0;

        Edges[^1][3] = scaleVector[0] * basicPlane[0];
        Edges[^1][4] = scaleVector[1] * basicPlane[1];
        Edges[^1][5] = 0;

        Edges[^1][6] = scaleVector[0] * (basicPlane[0] + replicationVector[0]);
        Edges[^1][7] = scaleVector[1] * (basicPlane[1] + replicationVector[1]);
        Edges[^1][8] = scaleVector[2] * replicationVector[2];

        Edges[^1][9] = scaleVector[0] * (basicPlane[^2] + replicationVector[0]);
        Edges[^1][10] = scaleVector[1] * (basicPlane[^1] + replicationVector[1]);
        Edges[^1][11] = scaleVector[2] * replicationVector[2];

        BasicPlaneNormals = new PlaneNormals(BasicPlane);
        ReplicatedPlaneNormals = new PlaneNormals(ReplicatedPlane);
        EdgesNormals = new PlaneNormals[Edges.Length];

        for (var i = 0; i < Edges.Length; i++)
        {
            EdgesNormals[i] = new PlaneNormals(Edges[i]);
        }

        BasicPlaneTexture = new Texture2D(basicPlaneTexture, textureOverlayCoordinates, TextureWrapMode.Clamp);
        ReplicatedPlaneTexture = new Texture2D(replicatedPlaneTexture, textureOverlayCoordinates, TextureWrapMode.Clamp);
        EdgesTexture = new Texture2D(edgeTexture, textureOverlayCoordinates, TextureWrapMode.Clamp);
    }
}