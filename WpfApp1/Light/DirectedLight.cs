using WpfApp1.Model;

namespace WpfApp1.Light;

public class DirectedLight
{
    public string Name { get; set; } = "New Light 0";
    public float[] Position { get; set; } = new float[] { 0, 0, 1, 0 };
    public float[] Color { get; set; } = new float[] { 1, 1, 1 };
    public LightType LightType = LightType.DirectedLight;

    public DirectedLight(string name, float[] color, float[] position)
    {
        Position = new float[4];
        for (var i = 0; i < 3; i++)
        {
            Position[i] = position[i];
        }
        Position[^1] = 0;
        Name = name;
        Color = color;
    }

    public DirectedLight()
    {
    }
}