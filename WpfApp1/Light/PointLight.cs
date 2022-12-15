using WpfApp1.Model;

namespace WpfApp1.Light;

public class PointLight
{
    public string Name { get; set; } = "Point";
    public string Position { get; set; } = "0, 0, 10";
    public string Color { get; set; } = "0, 1, 0";

    public float ConstantAttenuation { get; set; } = 0.01f;
    public float LinearAttenuation { get; set; } = 0.05f;
    public float QuadraticAttenuation { get; set; } = 0;

    public LightType LightType { get; set; }

    public float[] PositionArray { get; set; } = { 0, 0, 10, 1 };
    public float[] ColorArray { get; set; } = { 0, 1, 0 };

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