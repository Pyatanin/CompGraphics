using OpenTK.Mathematics;

namespace WpfLabs.Model;

public class MyPoint
{
    public Color4 Color = Color4.White;
    public Vector2 Coordinates;
    public bool IsSelected;

    public MyPoint(Vector2 vector2, bool isSelected)
    {
        Coordinates = vector2;
        IsSelected = isSelected;
    }
}