using WpfApp1.Model;

namespace WpfApp1.Light;

public class DirectedLight
{
    public float[] Position { get; set; }
    public float[] Color { get; set; }
    public string Name { get; set; }
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
}