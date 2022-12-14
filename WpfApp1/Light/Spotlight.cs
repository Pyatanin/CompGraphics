using WpfApp1.Model;

namespace WpfApp1.Light;

public class Spotlight
{
    public string Name  { get; set; } = "New Light 3";
    public float[] PositionArray   = { 1, 1, 1, 0 };
    public string Position { get; set; } = "0, 0, 1, 0";
    public float[] ColorArray  = { 1, 0, 0 };
    public string Color { get; set; } = "1, 1, 1";

    public LightType LightType;
    public float ConstantAttenuation  { get; set; } = 1;
    public float LinearAttenuation  { get; set; }
    public float QuadraticAttenuation  { get; set; }
    public float SpotCutoff  { get; set; } = 1;
    public float Exponent  { get; set; }
    public float[] SpotDirection  { get; set; } = { 0,0,-1 };

    public Spotlight(string name, float[] colorArray, float[] position, float spotCutoff, float[] spotDirection)
    {
        PositionArray = new float[4];
        for (var i = 0; i < 3; i++)
        {
            PositionArray[i] = position[i];
        }

        PositionArray[^1] = 1;
        Name = name;
        ColorArray = colorArray;
        SpotCutoff = spotCutoff;
        SpotDirection = spotDirection;
        LightType = LightType.SpotlightIntensiveOff;
    }

    public Spotlight(string name, float[] colorArray, float[] position, float spotCutoff, float[] spotDirection,
        float exponent, float constantAttenuation, float linearAttenuation, float quadraticAttenuation)
    {
        PositionArray = new float[4];
        for (var i = 0; i < 3; i++)
        {
            PositionArray[i] = position[i];
        }
        PositionArray[^1] = 1;
        Name = name;
        ColorArray = colorArray;
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