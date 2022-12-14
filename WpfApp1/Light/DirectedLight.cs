﻿using WpfApp1.Model;

namespace WpfApp1.Light;

public class DirectedLight
{
    public string Name { get; set; } = "New Light 0";
    public float[] PositionArray  = { 0, 0, 1, 0 };
    public string Position { get; set; } = "0, 0, 1, 0";
    public float[] ColorArray  = { 1, 1, 1 };
    public string Color { get; set; } = "1, 1, 1";

    public LightType LightType = LightType.DirectedLight;

    public DirectedLight(string name, float[] colorArray, float[] position)
    {
        PositionArray = new float[4];
        for (var i = 0; i < 3; i++)
        {
            PositionArray[i] = position[i];
        }
        PositionArray[^1] = 0;
        Name = name;
        ColorArray = colorArray;
    }

    public DirectedLight()
    {
    }
}