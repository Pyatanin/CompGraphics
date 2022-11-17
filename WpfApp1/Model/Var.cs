using System.Collections.Generic;
using System.Windows;

namespace WpfApp1.Model;

public class Var
{
    public Point Baza = new Point(800, 600);

    public double constSpeed = 0.4;
    public double constSpeedRotation = 2;
    public Point Cur = new Point(800, 600);


    public PlaneNormals floorNormals;
    public PlaneNormals FootingNormals;
    public List<PlaneNormals> GranNormals;


    public float[] Kvadrat = { 1, 1, 0, 1, -1, 0, -1, -1, 0, -1, 1, 0 };


    public bool MouseSelect = false;


    public Figura3D MyFigura3D = new Figura3D(
        new List<float>()
        {
            1, 1,
            1, -1,
            -1, -1,
            -1, 1,
        },
        new List<float>()
        {
            1, 1, 2
        },
        new List<float>()
        {
            0, 1, 1, 1
        },
        new List<float>()
        {
            1, 1, 1
        },
        new float[]
        {
            1, 0,
            0, 0,
            0, 1,
            1, 1,
        }
    );

    public Floor MyFloor = new Floor(
        new float[]
        {
            10, 10, -2,
            10, -10, -2,
            -10, -10, -2,
            -10, 10, -2
        },
        new float[]
        {
            10, 0,
            0, 0,
            0, 10,
            10, 10,
        });

    public bool Normals = false;
    public float[] normFool = { 10, 10, 10, 10, -10, 10, -10, -10, 10, -10, 10, 10 };


    public bool Object = true;

    public bool Orthogonal = false;
    public bool Perspective = true;

    public float[] Piramida =
    {
        0, 0, 6,
        1, 1, 0,
        1, -1, 0,
        -1, -1, 0,
        -1, 1, 0,
        1, 1, 0
    };

    public Point pos = new(0, 0);
    public PlaneNormals ReplicationNormals;
    public bool Skeleton = true;

    public double speed = 0;

    public float SunRotate = 0.0f;
    public bool Textures = true;

    public List<float> traektoria3D = new List<float>() { 0, 0, 2 };
    public double ugol = 0;


    public int vertexBufferId;

    public double Xalfa = 0;
    public double Yalfa = -10;
    public double Zalfa = 0;
}