﻿using WpfApp1.Model;
namespace WpfApp1.Light;

public class PointLight
{
    public float[] Position { get; set; }
    public float[] Color { get; set; }
    public string Name { get; set; }
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
}