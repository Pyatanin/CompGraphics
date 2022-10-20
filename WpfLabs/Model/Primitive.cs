using System.Collections.Generic;
using OpenTK.Mathematics;

namespace WpfLabs.Model;

public class Primitive
{
    public readonly List<MyPoint> Coordinates;
    public Color4 Color = Color4.White;
    public bool Selection;
    public int Type;

    public Primitive(int type, List<MyPoint> coordinates, Color4 color, bool selection)
    {
        Type = type;
        Coordinates = coordinates;
        Color = color;
        Selection = selection;
    }

    public void AddPrimitive(MyPoint myPoint)
    {
        Coordinates.Add(myPoint);
    }
}