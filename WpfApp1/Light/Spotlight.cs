using WpfApp1.Model;

namespace WpfApp1.Light;

public class Spotlight
{
    public float[] Position;
    public float[] Color;
    public string Name;
    public LightType LightType;
    public float ConstantAttenuation = 0;
    public float LinearAttenuation = 0;
    public float QuadraticAttenuation = 0;
    public float SpotCutoff;
    public float Exponent;
    public float[] SpotDirection;

    public Spotlight(string name, float[] color, float[] position, float spotCutoff, float[] spotDirection)
    {
        Position = position;
        Name = name;
        Color = color;
        SpotCutoff = spotCutoff;
        SpotDirection = spotDirection;
        LightType = LightType.SpotlightIntensiveOff;
    }

    public Spotlight(string name, float[] color, float[] position, float spotCutoff, float[] spotDirection,
        float exponent)
    {
        Position = position;
        Name = name;
        Color = color;
        SpotCutoff = spotCutoff;
        SpotDirection = spotDirection;
        Exponent = exponent;
        LightType = LightType.SpotlightIntensiveOnExponent;
    }
    public Spotlight(string name, float[] color, float[] position, float spotCutoff, float[] spotDirection,
        float constantAttenuation, float linearAttenuation, float quadraticAttenuation)
    {
        Position = position;
        Name = name;
        Color = color;
        SpotCutoff = spotCutoff;
        SpotDirection = spotDirection;
        ConstantAttenuation = constantAttenuation;
        LinearAttenuation = linearAttenuation;
        QuadraticAttenuation = quadraticAttenuation;
        LightType = LightType.SpotlightIntensiveOn;
    }
}