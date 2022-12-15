using WpfApp1.Model;

namespace WpfApp1.Light;

public class PointLight
{
    public string Name { get; set; } = "Point";
    public string Position { get; set; } = "0, 0, 1";
    public string Color { get; set; } = "1, 1, 1";

    public float ConstantAttenuation { get; set; }
    public float LinearAttenuation { get; set; }
    public float QuadraticAttenuation { get; set; }

    public LightType LightType;

    public float[] PositionArray { get; set; } = { 0, 0, 1, 1 };
    public float[] ColorArray { get; set; } = { 1, 1, 1 };

    public PointLight(string name, float[] colorArray, float[] position)
    {
        PositionArray = new float[4];
        for (var i = 0; i < 3; i++)
        {
            PositionArray[i] = position[i];
        }

        PositionArray[^1] = 1;
        Name = name;
        ColorArray = colorArray;
        LightType = LightType.PointLightIntensiveOff;
    }

    public PointLight(string name, float[] colorArray, float[] position, float constantAttenuation,
        float linearAttenuation, float quadraticAttenuation)
    {
        PositionArray = new float[4];
        for (var i = 0; i < 3; i++)
        {
            PositionArray[i] = position[i];
        }

        PositionArray[^1] = 1;
        Name = name;
        ColorArray = colorArray;
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