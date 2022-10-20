using OpenTK.Mathematics;

namespace WpfLabs.Model;

public class MyPoint
{
    public Color4 Color = Color4.White;
    public Vector2 Coordinates;
    public bool Selection = false;

    public MyPoint(Vector2 vector2, bool selection)
    {
        Coordinates = vector2;
        Selection = selection;
    }
}