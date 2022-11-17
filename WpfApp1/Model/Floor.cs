namespace WpfApp1.Model;

public class Floor
{
    public float[] Coordinat;
    public float[] CoordOverlay;

    public Floor(float[] coordinat, float[] coordOverlay)
    {
        Coordinat = coordinat;
        CoordOverlay = coordOverlay;
    }
}