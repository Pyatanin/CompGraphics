using OpenTK.Mathematics;

namespace WpfApp1.Model;

public class MyPoint
{
    public Vector3 Coordinates;
    public bool IsSelected;

    public MyPoint(Vector3 vector3, bool isSelected)
    {
        Coordinates = vector3;
        IsSelected = isSelected;
    }
}