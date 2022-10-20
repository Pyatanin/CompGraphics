using OpenTK.Mathematics;

namespace WpfLabs.Model;

public class Var
{
    public const float SelectionPointSize = 5.0f;
    public const float SelectionSize = 6.0f;


    public const float StandardSize = 3.0f;
    public float DeltaX = 0.0f;
    public float DeltaY = 0.0f;
    public float Height = 1000;
    public int Mode = 0;

    public bool Move = false;
    public bool Press = false;
    public Color4 SelectionColor4 = Color4.Red;
    public Color4 SelectionPointColor4 = Color4.Blue;
    public Color4 StandardColor4 = Color4.Black;
    public float Width = 1000;
    public float XPosition = 0.0f;
    public float YPosition = 0.0f;
}