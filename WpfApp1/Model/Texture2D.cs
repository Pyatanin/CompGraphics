using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace WpfApp1.Model;

public class Texture2D : IDisposable
{
    public Texture2D(string fileName)
    {
        Bitmap bitmap = new Bitmap(1, 1);
        if (File.Exists(fileName))
        {
            bitmap = (Bitmap)Image.FromFile(fileName);
        }

        bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
        Wight = bitmap.Width;
        Height = bitmap.Height;
        BitmapData data = bitmap.LockBits(new Rectangle(0, 0, Wight, Height), ImageLockMode.ReadOnly,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        Id = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, Id);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
            (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
            (int)TextureMagFilter.Linear);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
            PixelFormat.Bgra,
            PixelType.UnsignedByte, data.Scan0);
        GL.BindTexture(TextureTarget.Texture2D, 0);
        bitmap.UnlockBits(data);
        Coordinates = new float[]
        {
            0, 1,
            1, 1,
            1, 0,
            0, 0
        };
        BufferId = GL.GenTexture();
        GL.BindBuffer(BufferTarget.ArrayBuffer, BufferId);
        GL.BufferData(BufferTarget.ArrayBuffer, Coordinates.Length * sizeof(float), Coordinates,
            BufferUsageHint.StaticDraw);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    public int Id { get; private set; }
    public int Wight { get; private set; }

    public int Height { get; private set; }

    public int BufferId { get; private set; }
    public float[] Coordinates { get; private set; }

    public void Dispose()
    {
        GL.DeleteBuffer(BufferId);
        GL.DeleteTexture(Id);
    }

    public void Bind()
    {
        GL.BindTexture(TextureTarget.Texture2D, Id);
        GL.BindBuffer(BufferTarget.ArrayBuffer, BufferId);
        GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, IntPtr.Zero);
    }

    public void UnBind()
    {
        GL.BindTexture(TextureTarget.Texture2D, 0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }
}