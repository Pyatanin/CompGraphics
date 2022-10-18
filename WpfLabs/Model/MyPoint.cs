using OpenTK.Mathematics;

namespace WpfLabs.Model;

public class MyPoint
{
    public Color4 color = Color4.White;
    public Vector2 coordinates;
    public bool selection = false;

    public MyPoint(Color4 color4, Vector2 vector2, bool Selection)
    {
        coordinates = vector2;
        color = color4;
        selection = Selection;
    }

    public MyPoint(Vector2 vector2, bool Selection)
    {
        coordinates = vector2;
        selection = Selection;
    }

    public MyPoint()
    {
    }
}