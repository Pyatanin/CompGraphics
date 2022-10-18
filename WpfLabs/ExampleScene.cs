using System;
using System.Diagnostics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;

namespace WpfLabs
{
    /// Example class handling the rendering for OpenGL.
    public class ExampleScene
    {
        private static readonly Stopwatch _stopwatch = Stopwatch.StartNew();

        public static void Ready()
        {
            Console.WriteLine("GlWpfControl is now ready");
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ScissorTest);
        }

        public static void Render(float alpha = 1.0f)
        {
            GL.ClearColor(Color4.LightBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Enable(EnableCap.PointSmooth);
            if (MainWindow.primitives != null)
            {
                foreach (var p in MainWindow.primitives.listPrimitive)
                {
                    if (p.coordinates.Count != 0)
                    {
                        GL.Color4(p.color);
                        switch (p.type)
                        {
                            case 1:
                                GL.PointSize(MainWindow.myVar.StandartSize);
                                GL.Begin(PrimitiveType.Points);
                                break;
                            case 2:
                                GL.LineWidth(MainWindow.myVar.StandartSize);
                                GL.Begin(PrimitiveType.Lines);
                                break;
                            case 3:
                                GL.Begin(PrimitiveType.Triangles);
                                break;
                            case 4:
                                GL.LineWidth(MainWindow.myVar.StandartSize);
                                GL.Begin(PrimitiveType.LineStrip);
                                break;
                        }

                        foreach (var point in p.coordinates)
                        {
                            var x = 2 * point.coordinates.X / MainWindow.myVar.Height;
                            var y = 2 * point.coordinates.Y / MainWindow.myVar.Width;
                            GL.Vertex2(x, y);
                        }


                        GL.End();
                        if (p.selection)
                        {
                            GL.PointSize(MainWindow.myVar.SelectionSize);
                            GL.Color4(MainWindow.myVar.SelectionColor4);
                            GL.Begin(PrimitiveType.Points);
                            foreach (var point in p.coordinates)
                            {
                                GL.Vertex2(2 * point.coordinates.X / MainWindow.myVar.Height,
                                    2 * point.coordinates.Y / MainWindow.myVar.Width);
                            }

                            GL.End();
                        }

                        foreach (var point in p.coordinates)
                        {
                            if (point.selection)
                            {
                                GL.PointSize(MainWindow.myVar.SelectionPoinSize);
                                GL.Color4(MainWindow.myVar.SelectionPoinColor4);
                                GL.Begin(PrimitiveType.Points);
                                GL.Vertex2(2 * point.coordinates.X / MainWindow.myVar.Height,
                                    2 * point.coordinates.Y / MainWindow.myVar.Width);
                                GL.End();
                            }
                        }
                    }
                }
            }
        }
    }
}