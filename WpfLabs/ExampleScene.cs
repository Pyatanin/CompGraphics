using System;
using System.Linq;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using WpfLabs.Model;

namespace WpfLabs
{
    /// Example class handling the rendering for OpenGL.
    public class ExampleScene
    {
        public static void Ready()
        {
            Console.WriteLine("GlWpfControl is now ready");
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ScissorTest);
        }

        public static void Render(float alpha = 1.0f)
        {
            GL.ClearColor(Color4.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Enable(EnableCap.PointSmooth);

            foreach (var p in MainWindow.Primitives.ListPrimitive.Where(p => p.Coordinates.Count != 0))
            {
                GL.Color4(p.Color);
                switch (p.Type)
                {
                    case 1:
                        GL.PointSize(Var.StandardSize);
                        GL.Begin(PrimitiveType.Points);
                        break;
                    case 2:
                        GL.LineWidth(Var.StandardSize);
                        GL.Begin(PrimitiveType.Lines);
                        break;
                    case 3:
                        GL.Begin(PrimitiveType.Triangles);
                        break;
                    case 4:
                        GL.LineWidth(Var.StandardSize);
                        GL.Begin(PrimitiveType.LineStrip);
                        break;
                }

                foreach (var point in p.Coordinates)
                {
                    var x = 2 * point.Coordinates.X / MainWindow.MyVar.Width;
                    var y = 2 * point.Coordinates.Y / MainWindow.MyVar.Height;
                    GL.Vertex2(x, y);
                }


                GL.End();
                if (p.Selection)
                {
                    GL.PointSize(Var.SelectionSize);
                    GL.Color4(MainWindow.MyVar.SelectionColor4);
                    GL.Begin(PrimitiveType.Points);
                    foreach (var point in p.Coordinates)
                    {
                        GL.Vertex2
                        (
                            2 * point.Coordinates.X / MainWindow.MyVar.Width,
                            2 * point.Coordinates.Y / MainWindow.MyVar.Height
                        );
                    }

                    GL.End();
                }

                foreach (var point in p.Coordinates.Where(point => point.IsSelected))
                {
                    GL.PointSize(Var.SelectionPointSize);
                    GL.Color4(MainWindow.MyVar.SelectionPointColor4);
                    GL.Begin(PrimitiveType.Points);
                    GL.Vertex2
                    (
                        2 * point.Coordinates.X / MainWindow.MyVar.Width,
                        2 * point.Coordinates.Y / MainWindow.MyVar.Height
                    );
                    GL.End();
                }
            }
        }
    }
}