namespace WpfApp1.Model;

public class Camera
{
    public float X, Y, Z;
    public float Xalfa, Zalfa;

    public Camera(float x, float y, float z, float xalfa, float zalfa)
    {
        X = x;
        Y = y;
        Z = z;
        Xalfa = xalfa;
        Zalfa = zalfa;
    }
}