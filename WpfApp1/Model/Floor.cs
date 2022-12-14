using OpenTK.Graphics.OpenGL;

namespace WpfApp1.Model;

public class Floor
{
    public readonly float[] Coordinates;
    public readonly float[] TextureOverlayCoordinates;
    public readonly Texture2D FloorTexture;
    public readonly PlaneNormals FloorNormals;
    public readonly float[] floorNormals;


    public Floor(float[] coordinates, float[] textureOverlayCoordinates, string floorTexture)
    {
        Coordinates = coordinates;
        TextureOverlayCoordinates = textureOverlayCoordinates;
        FloorTexture = new Texture2D(floorTexture, textureOverlayCoordinates, TextureWrapMode.Repeat);
        FloorNormals = new PlaneNormals(Coordinates);
    }
    public Floor(float[] coordinates, float[] textureOverlayCoordinates, string floorTexture,float[] norms)
    {
        Coordinates = coordinates;
        TextureOverlayCoordinates = textureOverlayCoordinates;
        FloorTexture = new Texture2D(floorTexture, textureOverlayCoordinates, TextureWrapMode.Repeat);
        FloorNormals = new PlaneNormals(Coordinates);
        floorNormals = norms;
    }
}