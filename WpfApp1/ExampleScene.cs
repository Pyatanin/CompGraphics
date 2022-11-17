using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Controls;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using WpfApp1.Model;
using static WpfLabs.MainWindow;

namespace WpfLabs
{
    /// Example class handling the rendering for OpenGL.
    public class ExampleScene
    {
        public static void Ready()
        {
            if (MyVar.Perspective)
            {
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Frustum(-0.1, 0.1, -0.1, 0.1, 0.2, 1000);
                MyVar.Perspective = false;
            }

            if (MyVar.Orthogonal)
            {
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Ortho(-10, 10, -10, 10, 10, -10);
                MyVar.Orthogonal = false;
            }

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Enable(EnableCap.DepthTest);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.ColorMaterial);

            GL.Enable(EnableCap.Normalize);

            CreateNormal();
        }

        public static void WmSize(Grid MyGrid, MainWindow MyWindow)
        {
            if (MyWindow.ActualWidth > MyWindow.ActualHeight)
            {
                MyGrid.Width = MyWindow.ActualHeight;
                MyGrid.Height = MyWindow.ActualHeight;
            }
            else
            {
                MyGrid.Width = MyWindow.ActualWidth;
                MyGrid.Height = MyWindow.ActualWidth;
            }
        }

        public static void Kvadrat(bool texture)
        {
            Texture2D MyTexture = new Texture2D("Texture/4.png", new float[]
            {
                0, 1,
                1, 1,
                1, 0,
                0, 0
            }, TextureWrapMode.Clamp);

            if (texture)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, MyTexture.BufferId);
                GL.BufferData(BufferTarget.ArrayBuffer, MyVar.MyFigura3D.CoordOverlay.Length * sizeof(float),
                    MyVar.MyFigura3D.CoordOverlay,
                    BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.EnableClientState(ArrayCap.TextureCoordArray);
                GL.Enable(EnableCap.Texture2D);
                MyTexture.Bind();
                GL.BindBuffer(BufferTarget.ArrayBuffer, MyVar.vertexBufferId);
            }

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.NormalArray);

            GL.VertexPointer(3, VertexPointerType.Float, 0, MyVar.Kvadrat);
            GL.NormalPointer(NormalPointerType.Float, 0, MyVar.normFool);
            GL.DrawArrays(BeginMode.TriangleFan, 0, 4);

            if (texture)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                MyTexture.UnBind();
            }

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.NormalArray);
            if (texture)
            {
                GL.DisableClientState(ArrayCap.TextureCoordArray);
            }
        }


        public static void CreateNormal()
        {
            MyVar.floorNormals = new PlaneNormals(MyVar.MyFloor.Coordinat, true);
            MyVar.FootingNormals = new PlaneNormals(MyVar.MyFigura3D.Footing.ToArray(), true);
            MyVar.ReplicationNormals = new PlaneNormals(MyVar.MyFigura3D.Replication.ToArray(), true);
            MyVar.GranNormals = new List<PlaneNormals>();
            foreach (var granNorms in MyVar.MyFigura3D.Grans)
            {
                MyVar.GranNormals.Add(new PlaneNormals(granNorms.ToArray(), true));
            }
        }


        public static void ShowNormas()
        {
            //Floor
            foreach (var normal in MyVar.floorNormals.Normals)
            {
                GL.PushMatrix();
                ShowNormal(normal.StartPoint, normal.EndPointNormirovan, normal.XAxisAngle, normal.YAxisAngle,
                    normal.ZAxisAngle);
                GL.PopMatrix();
            }

            foreach (var normal in MyVar.FootingNormals.Normals)
            {
                GL.PushMatrix();
                ShowNormal(normal.StartPoint, normal.EndPointNormirovan, normal.XAxisAngle, normal.YAxisAngle,
                    normal.ZAxisAngle);
                GL.PopMatrix();
            }

            foreach (var normal in MyVar.ReplicationNormals.Normals)
            {
                GL.PushMatrix();
                ShowNormal(normal.StartPoint, normal.EndPointNormirovan, normal.XAxisAngle, normal.YAxisAngle,
                    normal.ZAxisAngle);
                GL.PopMatrix();
            }

            foreach (var normalGran in MyVar.GranNormals)
            {
                foreach (var normal in normalGran.Normals)
                {
                    GL.PushMatrix();
                    ShowNormal(normal.StartPoint, normal.EndPointNormirovan, normal.XAxisAngle, normal.YAxisAngle,
                        normal.ZAxisAngle);
                    GL.PopMatrix();
                }
            }
        }

        public static void ShowNormal(Vector3 start, Vector3 end, float Xalfa, float Yalfa, float Zalfa)
        {
            GL.LineWidth(5);
            GL.Color3(Color.Black);
            GL.Begin(BeginMode.LineLoop);

            GL.Vertex3(start);
            GL.Vertex3(end);

            GL.End();

            GL.PointSize(10);
            GL.Color3(Color.Red);

            GL.Begin(BeginMode.Points);

            GL.Vertex3(end);

            GL.End();
        }

        public static void ShowFool(bool texture)
        {
            Texture2D MyTexture = new Texture2D("Texture/3.jpg", new float[]
            {
                0, 1,
                1, 1,
                1, 0,
                0, 0
            }, TextureWrapMode.Repeat);

            if (texture)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, MyTexture.BufferId);
                GL.BufferData(BufferTarget.ArrayBuffer, MyVar.MyFloor.CoordOverlay.Length * sizeof(float),
                    MyVar.MyFloor.CoordOverlay,
                    BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.EnableClientState(ArrayCap.TextureCoordArray);
                GL.Enable(EnableCap.Texture2D);
                MyTexture.Bind();
                GL.BindBuffer(BufferTarget.ArrayBuffer, MyVar.vertexBufferId);
            }


            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.NormalArray);

            GL.VertexPointer(3, VertexPointerType.Float, 0, MyVar.MyFloor.Coordinat);
            GL.NormalPointer(NormalPointerType.Float, 0, MyVar.normFool);
            GL.DrawArrays(BeginMode.TriangleFan, 0, 4);
            if (texture)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                MyTexture.UnBind();
            }

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.NormalArray);
            if (texture)
            {
                GL.DisableClientState(ArrayCap.TextureCoordArray);
            }
        }

        public static void Show3d(bool texture)
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.NormalArray);
            Texture2D Up = new Texture2D("Texture/Up.jpg", new float[]
            {
                0, 1,
                1, 1,
                1, 0,
                0, 0
            }, TextureWrapMode.Clamp);
            Texture2D Down = new Texture2D("Texture/Down.jpg", new float[]
            {
                0, 1,
                1, 1,
                1, 0,
                0, 0
            }, TextureWrapMode.Clamp);
            Texture2D Side = new Texture2D("Texture/Side.jpg", new float[]
            {
                0, 1,
                1, 1,
                1, 0,
                0, 0
            }, TextureWrapMode.Clamp);
            // ОСнование
            if (texture)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, Down.BufferId);
                GL.BufferData(BufferTarget.ArrayBuffer, MyVar.MyFigura3D.CoordOverlay.Length * sizeof(float),
                    MyVar.MyFigura3D.CoordOverlay,
                    BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.EnableClientState(ArrayCap.TextureCoordArray);
                GL.Enable(EnableCap.Texture2D);
                Down.Bind();
                GL.BindBuffer(BufferTarget.ArrayBuffer, MyVar.vertexBufferId);
            }

            GL.PushMatrix();
            GL.VertexPointer(3, VertexPointerType.Float, 0, MyVar.MyFigura3D.Footing.ToArray());
            GL.NormalPointer(NormalPointerType.Float, 0, MyVar.FootingNormals.arrayNormals);
            GL.DrawArrays(BeginMode.TriangleFan, 0, MyVar.MyFigura3D.Footing.Count / 3);
            if (texture)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                Down.UnBind();
            }

            GL.PopMatrix();

            //Тиражирование
            GL.PushMatrix();
            if (texture)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, Up.BufferId);
                GL.BufferData(BufferTarget.ArrayBuffer, MyVar.MyFigura3D.CoordOverlay.Length * sizeof(float),
                    MyVar.MyFigura3D.CoordOverlay,
                    BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.EnableClientState(ArrayCap.TextureCoordArray);
                GL.Enable(EnableCap.Texture2D);
                Up.Bind();
                GL.BindBuffer(BufferTarget.ArrayBuffer, MyVar.vertexBufferId);
            }

            GL.VertexPointer(3, VertexPointerType.Float, 0, MyVar.MyFigura3D.Replication.ToArray());
            GL.NormalPointer(NormalPointerType.Float, 0, MyVar.ReplicationNormals.arrayNormals);
            GL.DrawArrays(BeginMode.TriangleFan, 0, MyVar.MyFigura3D.Replication.Count / 3);
            if (texture)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                Up.UnBind();
            }

            GL.PopMatrix();

            // Грани
            var i = 0;
            foreach (var gran in MyVar.MyFigura3D.Grans)
            {
                GL.PushMatrix();
                if (texture)
                {
                    GL.BindBuffer(BufferTarget.ArrayBuffer, Side.BufferId);
                    GL.BufferData(BufferTarget.ArrayBuffer, MyVar.MyFigura3D.CoordOverlay.Length * sizeof(float),
                        MyVar.MyFigura3D.CoordOverlay,
                        BufferUsageHint.StaticDraw);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                    GL.EnableClientState(ArrayCap.TextureCoordArray);
                    GL.Enable(EnableCap.Texture2D);
                    Side.Bind();
                    GL.BindBuffer(BufferTarget.ArrayBuffer, MyVar.vertexBufferId);
                }

                GL.VertexPointer(3, VertexPointerType.Float, 0, gran.ToArray());
                GL.NormalPointer(NormalPointerType.Float, 0, MyVar.GranNormals[i].arrayNormals);

                GL.DrawArrays(BeginMode.TriangleFan, 0, gran.Count / 3);
                if (texture)
                {
                    GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                    Side.UnBind();
                }

                GL.PopMatrix();
                i++;
            }

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.NormalArray);
            if (texture)
            {
                GL.DisableClientState(ArrayCap.TextureCoordArray);
            }
        }

        public static void Karkas()
        {
            GL.LineWidth(10);
            GL.Begin(BeginMode.LineLoop);

            for (int i = 0; i < MyVar.MyFigura3D.Footing.Count / 3; i++)
            {
                var dot = MyVar.MyFigura3D.Footing.GetRange(3 * i, 3);
                GL.Vertex3(dot[0], dot[1], dot[2]);
            }

            GL.End();

            GL.Begin(BeginMode.LineLoop);

            for (int i = 0; i < MyVar.MyFigura3D.Replication.Count / 3; i++)
            {
                var dot = MyVar.MyFigura3D.Replication.GetRange(3 * i, 3);
                GL.Vertex3(dot[0], dot[1], dot[2]);
            }

            GL.End();


            foreach (var gran in MyVar.MyFigura3D.Grans)
            {
                GL.Begin(BeginMode.LineLoop);

                for (int i = 0; i < gran.Count / 3; i++)
                {
                    var dot = gran.GetRange(3 * i, 3);
                    GL.Vertex3(dot[0], dot[1], dot[2]);
                }

                GL.End();
            }
        }

        public static void MoveCamera()
        {
            if (MyVar.speed != 0)
            {
                MyVar.pos.X += Math.Sin(MyVar.ugol) * MyVar.speed;
                MyVar.pos.Y += Math.Cos(MyVar.ugol) * MyVar.speed;
            }

            MyVar.ugol = -MyVar.Zalfa / 180 * Math.PI;

            if (MyVar.Mouse)
            {
                GL.Rotate(-MyVar.Xalfa, 1, 0, 0);
                GL.Rotate(-MyVar.Zalfa, 0, 0, 1);
                GL.Translate(-MyVar.pos.X, -MyVar.pos.Y, MyVar.Yalfa);
            }
            else
            {
                GL.Rotate(MyVar.Xalfa, 1, 0, 0);
                GL.Rotate(MyVar.Zalfa, 0, 0, 1);
                GL.Translate(MyVar.pos.X, MyVar.pos.Y, MyVar.Yalfa);
            }

            MyVar.speed = 0;
        }

        public static void MoveMouse()
        {
            if (!MyVar.MouseSelect) return;
            var cur = GetCursorPosition();

            MyVar.Zalfa += (MyVar.Baza.X - cur.X) / 30;
            if (MyVar.Zalfa < 0) MyVar.Zalfa += 360;
            if (MyVar.Zalfa > 360) MyVar.Zalfa -= 360;
            MyVar.Xalfa += (MyVar.Baza.Y - cur.Y) / 30;
            if (MyVar.Xalfa < 0) MyVar.Xalfa = 0;
            if (MyVar.Xalfa > 180) MyVar.Xalfa = 180;
            SetCursorPos((int)MyVar.Baza.X, (int)MyVar.Baza.Y);
        }

        public static void Render(float alpha = 1.0f)
        {
            GL.ClearColor(Color4.LightBlue);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            GL.Enable(EnableCap.PointSmooth);

            GL.PushMatrix();
            MoveMouse();
            MoveCamera();

            if (MyVar.Perspective)
            {
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Frustum(-0.1, 0.1, -0.1, 0.1, 0.2, 1000);
                MyVar.Perspective = false;
                GL.MatrixMode(MatrixMode.Modelview);

                MyVar.Mouse = true;
            }

            if (MyVar.Orthogonal)
            {
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Ortho(-30, 30, -30, 30, 30, -30);
                MyVar.Orthogonal = false;
                GL.MatrixMode(MatrixMode.Modelview);

                MyVar.Mouse = false;
            }

            if (MyVar.NormalDis)
            {
                GL.Disable(EnableCap.Normalize);
                MyVar.NormalDis = false;
            }

            if (MyVar.NormalEn)
            {
                GL.Enable(EnableCap.Normalize);
                MyVar.NormalEn = false;
            }


            //Солнышко
            GL.PushMatrix();
            GL.Rotate(MyVar.SunRotate, 0, 1, 0);
            float[] pos = new float[] { 0, 0, 1, 0 };
            GL.Light(LightName.Light0, LightParameter.Position, pos);
            GL.Translate(0, 0, 20);
            GL.Color3(Color.White);
            Kvadrat(MyVar.Textures);
            GL.PopMatrix();

            GL.PushMatrix();
            //GL.Scale(10, 10, 10);
            ShowFool(MyVar.Textures);
            GL.PopMatrix();

            GL.Scale(MyVar.MyFigura3D.Scale[0], MyVar.MyFigura3D.Scale[1], MyVar.MyFigura3D.Scale[2]);
            GL.Rotate(MyVar.MyFigura3D.Rotate[0], MyVar.MyFigura3D.Rotate[1], MyVar.MyFigura3D.Rotate[2],
                MyVar.MyFigura3D.Rotate[3]);
            if (MyVar.Skeleton)
            {
                GL.Color3(Color.Black);
                Karkas();
            }

            if (MyVar.Object)
            {
                GL.Color3(Color.Orange);
                Show3d(MyVar.Textures);
            }

            if (MyVar.Normals)
            {
                ShowNormas();
            }

            GL.PopMatrix();
            MyVar.SunRotate++;
        }
    }
}