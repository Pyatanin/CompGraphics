using WpfApp1.Model;

namespace WpfApp1.Light;

public class Spotlight
{
    public string Name  { get; set; } = "New Light 3";
    public float[] Position  { get; set; } = { 1, 1, 1, 0 };
    public float[] Color  { get; set; } = new float[] { 1, 0, 0 };
    public LightType LightType;
    public float ConstantAttenuation  { get; set; } = 1;
    public float LinearAttenuation  { get; set; } = 0;
    public float QuadraticAttenuation  { get; set; } = 0;
    public float SpotCutoff  { get; set; } = 1;
    public float Exponent  { get; set; } = 0;
    public float[] SpotDirection  { get; set; } = { 0,0,-1 };

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