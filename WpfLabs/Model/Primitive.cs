using System.Collections.Generic;
using OpenTK.Mathematics;

namespace WpfLabs.Model;

public class Primitive
{
    public Color4 color = Color4.White;
    public List<MyPoint> coordinates;
    public bool selection = false;
    public int type = 0;

    public Primitive(int Type, Color4 Color, bool Selection)
    {
        type = Type;
        color = Color;
        selection = Selection;
    }

    public Primitive(int Type, List<MyPoint> Coordinates, Color4 Color, bool Selection)
    {
        type = Type;
        coordinates = Coordinates;
        color = Color;
        selection = Selection;
    }

    public void Primitive_add(MyPoint myPoint)
    {
        coordinates.Add(myPoint);
    }
}