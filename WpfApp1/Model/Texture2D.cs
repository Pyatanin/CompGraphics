using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace WpfApp1.Model;

public class Texture2D : IDisposable
{
    private readonly int _id;
    public readonly int BufferId;
    public Texture2D(string fileName, float[] coord, TextureWrapMode mod)
    {
        var bitmap = new Bitmap(1, 1);
        if (File.Exists(fileName))
        {
            bitmap = (Bitmap)Image.FromFile(fileName);
        }

        bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
        var width = bitmap.Width;
        var height = bitmap.Height;
        var data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,
            PixelFormat.Format32bppArgb);
        _id = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, _id);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)mod);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)mod);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
            (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
            (int)TextureMagFilter.Linear);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
            OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
            PixelType.UnsignedByte, data.Scan0);
        GL.BindTexture(TextureTarget.Texture2D, 0);
        bitmap.UnlockBits(data);
        var coordinates = coord;
        BufferId = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, BufferId);
        GL.BufferData(BufferTarget.ArrayBuffer, coordinates.Length * sizeof(float), coordinates,
            BufferUsageHint.StaticDraw);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(BufferId);
        GL.DeleteTexture(_id);
    }

    public void Bind()
    {
        GL.BindTexture(TextureTarget.Texture2D, _id);
        GL.BindBuffer(BufferTarget.ArrayBuffer, BufferId);
        GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, IntPtr.Zero);
    }

    public void Unbind()
    {
        GL.BindTexture(TextureTarget.Texture2D, 0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }
}