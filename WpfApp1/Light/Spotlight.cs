using WpfApp1.Model;

namespace WpfApp1.Light;

public class Spotlight
{
    public float[] Position= { 1, 1, 1, 0 };
    public float[] Color = new float[] { 1, 0, 0 };
    public string Name = "New Light 3";
    public LightType LightType;
    public float ConstantAttenuation = 1;
    public float LinearAttenuation = 0;
    public float QuadraticAttenuation = 0;
    public float SpotCutoff = 1;
    public float Exponent = 0;
    public float[] SpotDirection = { 0,0,-1 };

    public Spotlight(string name, float[] color, float[] position, float spotCutoff, float[] spotDirection)
    {
        Position = new float[4];
        for (var i = 0; i < 3; i++)
        {
            Position[i] = position[i];
        }

        Position[^1] = 1;
        Name = name;
        Color = color;
        SpotCutoff = spotCutoff;
        SpotDirection = spotDirection;
        LightType = LightType.SpotlightIntensiveOff;
    }

    public Spotlight(string name, float[] color, float[] position, float spotCutoff, float[] spotDirection,
        float exponent, float constantAttenuation, float linearAttenuation, float quadraticAttenuation)
    {
        Position = new float[4];
        for (var i = 0; i < 3; i++)
        {
            Position[i] = position[i];
        }
        Position[^1] = 1;
        Name = name;
        Color = color;
        SpotCutoff = spotCutoff;
        SpotDirection = spotDirection;
        Exponent = exponent;
        ConstantAttenuation = constantAttenuation;
        LinearAttenuation = linearAttenuation;
        QuadraticAttenuation = quadraticAttenuation;
        LightType = LightType.SpotlightIntensiveOn;
    }

    public Spotlight(LightType type)
    {
        LightType = type;
    }
}