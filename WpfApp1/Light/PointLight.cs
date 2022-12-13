using WpfApp1.Model;
namespace WpfApp1.Light;

public class PointLight
{
    public float[] Position { get; set; } = { -1, -1, -1, 0 };
    public float[] Color { get; set; } = new float[] { 0, 0, 1 };
    public string Name { get; set; } = "New Light 1";
    public LightType LightType;

    public float ConstantAttenuation;
    public float LinearAttenuation;
    public float QuadraticAttenuation;

    public PointLight(string name, float[] color, float[] position)
    {
        Position = new float[4];
        for (var i = 0; i < 3; i++)
        {
            Position[i] = position[i];
        }

        Position[^1] = 1;
        Name = name;
        Color = color;
        LightType = LightType.PointLightIntensiveOff;
    }

    public PointLight(string name, float[] color, float[] position, float constantAttenuation, float linearAttenuation, float quadraticAttenuation)
    {
        Position = new float[4];
        for (var i = 0; i < 3; i++)
        {
            Position[i] = position[i];
        }
        Position[^1] = 1;
        Name = name;
        Color = color;
        ConstantAttenuation = constantAttenuation;
        LinearAttenuation = linearAttenuation;
        QuadraticAttenuation = quadraticAttenuation;
        LightType = LightType.PointLightIntensiveOn;
    }

    public PointLight(LightType type)
    {
        LightType = type;
    }

}