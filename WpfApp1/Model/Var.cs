using System.Collections.Generic;
using System.Windows;

namespace WpfApp1.Model;

public class Var
{
    public Point Baza = new Point(800, 600);
    public double constSpeed = 0.4;
    public double constSpeedRotation = 2;
    public Point Cur = new Point(800, 600);
    public float[] floor = { 10, 10, -0.1f, 10, -10, -0.1f, -10, -10, -0.1f, -10, 10, -0.1f };

    public List<List<float>> Grans = new List<List<float>>();

    public float HYU = 0.0f;
    public float[] Kvadrat = { 1, 1, -0.1f, 1, -1, -0.1f, -1, -1, -0.1f, -1, 1, -0.1f };

    public double[] mashtab =
    {
        1, 1, 1
    };


    public bool MouseSelect = false;
    public float[] norm = { 1, 1, 3, 1, -1, 3, -1, -1, 3, -1, 1, 3 };
    public bool Normals = false;


    public bool Object = false;
    public bool Orthogonal = false;

    public bool Perspective = true;

    public float[] Piramida =
    {
        0, 0, 2,
        1, 1, 0,
        1, -1, 0,
        -1, -1, 0,
        -1, 1, 0,
        1, 1, 0
    };

    // public Point pos = new(-2, -3);
    public Point pos = new(0, 0);

    public float[] rotate =
    {
        0, 1, 1, 1
    };

    public List<float> sechenie2D = new List<float>()
    {
        1, 1,
        1, -1,
        -1, -1,
        -1, 1,
    };

    public bool Skeleton = false;


    public double speed = 0;
    public bool Textures = false;

    public List<float> traektoria3D = new List<float>() { 0, 0, 2 };
    public double ugol = 0;

    public double Xalfa = 0;

    // public double Yalfa = -2;
    public double Yalfa = -10;

    public double Zalfa = 0;
    // public bool Perspective = false;
    // public bool Orthogonal = true;
}