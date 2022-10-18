using OpenTK.Mathematics;

namespace WpfLabs.Model;

public class Var
{
    public float deltaX = 0.0f;
    public float deltaY = 0.0f;
    public float Height = 1000;
    public int mode = 0;

    public bool move = false;
    public bool press = false;
    public Color4 SelectionColor4 = Color4.Red;
    public Color4 SelectionPoinColor4 = Color4.Blue;
    public float SelectionPoinSize = 5.0f;
    public float SelectionSize = 6.0f;
    public Color4 StandartColor4 = Color4.Black;


    public float StandartSize = 3.0f;
    public float Width = 1000;
    public float XPosition = 0.0f;
    public float YPosition = 0.0f;


    public Var()
    {
    }
}