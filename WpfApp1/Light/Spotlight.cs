using WpfApp1.Model;

namespace WpfApp1.Light;

public class Spotlight
{
    public string Name  { get; set; } = "Spot";
    public string Position { get; set; } = "0, 0, 1";
    public string Color { get; set; } = "1, 1, 1";
    public string SpotDirection  { get; set; } = "0, 0, -1";

    public float ConstantAttenuation  { get; set; }
    public float LinearAttenuation  { get; set; }
    public float QuadraticAttenuation  { get; set; }
    public float SpotCutoff  { get; set; }
    public float Exponent  { get; set; }
    
    public LightType LightType;
    public float[] SpotDirectionArray  { get; set; } = { 0, 0, -1 };
    public float[] PositionArray { get; set; }  = { 0, 0, 1, 1 };
    public float[] ColorArray { get; set; } = { 1, 1, 1 };



    public Spotlight(string name, float[] colorArray, float[] position, float spotCutoff, float[] spotDirectionArray)
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
        SpotDirectionArray = spotDirectionArray;
        LightType = LightType.SpotlightIntensiveOff;
    }

    public Spotlight(string name, float[] colorArray, float[] position, float spotCutoff, float[] spotDirectionArray,
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
        SpotDirectionArray = spotDirectionArray;
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