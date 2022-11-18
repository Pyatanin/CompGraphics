using OpenTK.Graphics.OpenGL;

namespace WpfApp1.Model;

public class Floor
{
    public readonly float[] Coordinates;
    public readonly float[] TextureOverlayCoordinates;
    public readonly Texture2D FloorTexture;

    public Floor(float[] coordinates, float[] textureOverlayCoordinates, string floorTexture)
    {
        Coordinates = coordinates;
        TextureOverlayCoordinates = textureOverlayCoordinates;
        FloorTexture = new Texture2D(floorTexture, textureOverlayCoordinates, TextureWrapMode.Repeat);
    }
}