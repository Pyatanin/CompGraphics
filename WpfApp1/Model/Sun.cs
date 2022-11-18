namespace WpfApp1.Model;
using OpenTK.Graphics.OpenGL;

public class Sun
{
    public float SunPosition = 0.0f;
    public readonly float[] SunCoordinates;
    public readonly float[] TextureOverlayCoordinates;
    public readonly Texture2D SunTexture;

    public Sun(float[] sunCoordinates, string sunTexture, float[] textureOverlayCoordinates)
    {
        SunCoordinates = sunCoordinates;
        TextureOverlayCoordinates = textureOverlayCoordinates;
        SunTexture = new Texture2D(sunTexture, textureOverlayCoordinates, TextureWrapMode.Clamp);
    }
}