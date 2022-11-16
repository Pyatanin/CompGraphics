﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            if (MyVar.Perspective)
            {
                GL.Frustum(-0.1, 0.1, -0.1, 0.1, 0.2, 1000);
                MyVar.Perspective = false;
            }

            if (MyVar.Orthogonal)
            {
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

            CreateGrani();
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

        public static void Kvadrat()
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.NormalArray);

            GL.VertexPointer(3, VertexPointerType.Float, 0, MyVar.Kvadrat);
            GL.NormalPointer(NormalPointerType.Float, 0, MyVar.normFool);
            GL.DrawArrays(BeginMode.TriangleFan, 0, 4);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.NormalArray);
        }

        public static void CreateGrani()
        {
            for (int i = 0; i < MyVar.sechenie2D.Count / 2 - 1; i++)
            {
                MyVar.Grans.Add(new List<float>());
                MyVar.Grans[i].Add(MyVar.sechenie2D[2 * i]);
                MyVar.Grans[i].Add(MyVar.sechenie2D[2 * i + 1]);
                MyVar.Grans[i].Add(0);

                MyVar.Grans[i].Add(MyVar.sechenie2D[2 * i + 2]);
                MyVar.Grans[i].Add(MyVar.sechenie2D[2 * i + 3]);
                MyVar.Grans[i].Add(0);

                MyVar.Grans[i].Add(MyVar.sechenie2D[2 * i + 2] + MyVar.traektoria3D[0]);
                MyVar.Grans[i].Add(MyVar.sechenie2D[2 * i + 3] + MyVar.traektoria3D[1]);
                MyVar.Grans[i].Add(MyVar.traektoria3D[2]);

                MyVar.Grans[i].Add(MyVar.sechenie2D[2 * i] + MyVar.traektoria3D[0]);
                MyVar.Grans[i].Add(MyVar.sechenie2D[2 * i + 1] + MyVar.traektoria3D[1]);
                MyVar.Grans[i].Add(MyVar.traektoria3D[2]);
            }

            MyVar.Grans.Add(new List<float>());
            MyVar.Grans.Last().Add(MyVar.sechenie2D[0]);
            MyVar.Grans.Last().Add(MyVar.sechenie2D[1]);
            MyVar.Grans.Last().Add(0);

            MyVar.Grans.Last().Add(MyVar.sechenie2D[^2]);
            MyVar.Grans.Last().Add(MyVar.sechenie2D[^1]);
            MyVar.Grans.Last().Add(0);

            MyVar.Grans.Last().Add(MyVar.sechenie2D[^2] + MyVar.traektoria3D[0]);
            MyVar.Grans.Last().Add(MyVar.sechenie2D[^1] + MyVar.traektoria3D[1]);
            MyVar.Grans.Last().Add(MyVar.traektoria3D[2]);

            MyVar.Grans.Last().Add(MyVar.sechenie2D[0] + MyVar.traektoria3D[0]);
            MyVar.Grans.Last().Add(MyVar.sechenie2D[1] + MyVar.traektoria3D[1]);
            MyVar.Grans.Last().Add(MyVar.traektoria3D[2]);
        }

        public static void CreateNormalFool()
        {
            for (int i = 0; i < MyVar.sechenie2D.Count / 2 - 1; i++)
            {
                MyVar.Grans.Add(new List<float>());
                MyVar.Grans[i].Add(MyVar.sechenie2D[2 * i]);
                MyVar.Grans[i].Add(MyVar.sechenie2D[2 * i + 1]);
                MyVar.Grans[i].Add(0);

                MyVar.Grans[i].Add(MyVar.sechenie2D[2 * i + 2]);
                MyVar.Grans[i].Add(MyVar.sechenie2D[2 * i + 3]);
                MyVar.Grans[i].Add(0);

                MyVar.Grans[i].Add(MyVar.sechenie2D[2 * i + 2] + MyVar.traektoria3D[0]);
                MyVar.Grans[i].Add(MyVar.sechenie2D[2 * i + 3] + MyVar.traektoria3D[1]);
                MyVar.Grans[i].Add(MyVar.traektoria3D[2]);

                MyVar.Grans[i].Add(MyVar.sechenie2D[2 * i] + MyVar.traektoria3D[0]);
                MyVar.Grans[i].Add(MyVar.sechenie2D[2 * i + 1] + MyVar.traektoria3D[1]);
                MyVar.Grans[i].Add(MyVar.traektoria3D[2]);
            }

            MyVar.Grans.Add(new List<float>());
            MyVar.Grans.Last().Add(MyVar.sechenie2D[0]);
            MyVar.Grans.Last().Add(MyVar.sechenie2D[1]);
            MyVar.Grans.Last().Add(0);

            MyVar.Grans.Last().Add(MyVar.sechenie2D[^2]);
            MyVar.Grans.Last().Add(MyVar.sechenie2D[^1]);
            MyVar.Grans.Last().Add(0);

            MyVar.Grans.Last().Add(MyVar.sechenie2D[^2] + MyVar.traektoria3D[0]);
            MyVar.Grans.Last().Add(MyVar.sechenie2D[^1] + MyVar.traektoria3D[1]);
            MyVar.Grans.Last().Add(MyVar.traektoria3D[2]);

            MyVar.Grans.Last().Add(MyVar.sechenie2D[0] + MyVar.traektoria3D[0]);
            MyVar.Grans.Last().Add(MyVar.sechenie2D[1] + MyVar.traektoria3D[1]);
            MyVar.Grans.Last().Add(MyVar.traektoria3D[2]);
        }

        public static void ShowNormal(Vector3 start, Vector3 end)
        {
            GL.LineWidth(10);
            GL.Color3(Color.Black);
            GL.Begin(BeginMode.LineLoop);

            GL.Vertex3(start);
            GL.Vertex3(end);

            GL.End();

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.Translate(end);
            GL.Scale(0.05, 0.05, 0.05);

            GL.VertexPointer(3, VertexPointerType.Float, 0, MyVar.Piramida);
            GL.DrawArrays(BeginMode.TriangleFan, 0, 6);
            GL.DisableClientState(ArrayCap.VertexArray);
        }

        public static void ShowFool(bool texture)
        {
            Texture2D MyTexture = new Texture2D("Texture/3.jpg");

            if (texture)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, MyTexture.BufferId);
                GL.BufferData(BufferTarget.ArrayBuffer, MyVar.floorCoord.Length * sizeof(float), MyVar.floorCoord,
                    BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.EnableClientState(ArrayCap.TextureCoordArray);
                GL.Enable(EnableCap.Texture2D);
                MyTexture.Bind();
                GL.BindBuffer(BufferTarget.ArrayBuffer, MyVar.vertexBufferId);
            }


            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.NormalArray);


            GL.VertexPointer(3, VertexPointerType.Float, 0, MyVar.floor);
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
            Texture2D Up = new Texture2D("Texture/Up.jpg");
            Texture2D Down = new Texture2D("Texture/Down.jpg");
            Texture2D Side = new Texture2D("Texture/Side.jpg");
            // ОСнование
            if (texture)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, Down.BufferId);
                GL.BufferData(BufferTarget.ArrayBuffer, MyVar.floorCoord.Length * sizeof(float), MyVar.floorCoord,
                    BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.EnableClientState(ArrayCap.TextureCoordArray);
                GL.Enable(EnableCap.Texture2D);
                Down.Bind();
                GL.BindBuffer(BufferTarget.ArrayBuffer, MyVar.vertexBufferId);
            }

            GL.PushMatrix();
            GL.VertexPointer(2, VertexPointerType.Float, 0, MyVar.sechenie2D.ToArray());
            GL.NormalPointer(NormalPointerType.Float, 0, MyVar.normFool);
            GL.DrawArrays(BeginMode.TriangleFan, 0, MyVar.sechenie2D.Count / 2);
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
                GL.BufferData(BufferTarget.ArrayBuffer, MyVar.floorCoord.Length * sizeof(float), MyVar.floorCoord,
                    BufferUsageHint.StaticDraw);
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.EnableClientState(ArrayCap.TextureCoordArray);
                GL.Enable(EnableCap.Texture2D);
                Up.Bind();
                GL.BindBuffer(BufferTarget.ArrayBuffer, MyVar.vertexBufferId);
            }

            GL.VertexPointer(2, VertexPointerType.Float, 0, MyVar.sechenie2D.ToArray());
            GL.NormalPointer(NormalPointerType.Float, 0, MyVar.normFool);
            GL.Translate(MyVar.traektoria3D[0], MyVar.traektoria3D[1], MyVar.traektoria3D[2]);
            GL.DrawArrays(BeginMode.TriangleFan, 0, MyVar.sechenie2D.Count / 2);
            if (texture)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                Up.UnBind();
            }

            GL.PopMatrix();

            // Грани
            foreach (var gran in MyVar.Grans)
            {
                GL.PushMatrix();
                if (texture)
                {
                    GL.BindBuffer(BufferTarget.ArrayBuffer, Side.BufferId);
                    GL.BufferData(BufferTarget.ArrayBuffer, MyVar.floorCoord.Length * sizeof(float), MyVar.floorCoord,
                        BufferUsageHint.StaticDraw);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                    GL.EnableClientState(ArrayCap.TextureCoordArray);
                    GL.Enable(EnableCap.Texture2D);
                    Side.Bind();
                    GL.BindBuffer(BufferTarget.ArrayBuffer, MyVar.vertexBufferId);
                }

                GL.VertexPointer(3, VertexPointerType.Float, 0, gran.ToArray());
                GL.DrawArrays(BeginMode.TriangleFan, 0, gran.Count / 3);
                if (texture)
                {
                    GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                    Side.UnBind();
                }

                GL.PopMatrix();
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

            for (int i = 0; i < MyVar.sechenie2D.Count / 2; i++)
            {
                var dot = MyVar.sechenie2D.GetRange(2 * i, 2);
                GL.Vertex3(dot[0], dot[1], 0);
            }

            GL.End();

            GL.Begin(BeginMode.LineLoop);

            for (int i = 0; i < MyVar.sechenie2D.Count / 2; i++)
            {
                var dot = MyVar.sechenie2D.GetRange(2 * i, 2);
                GL.Vertex3(dot[0] + MyVar.traektoria3D[0], dot[1] + MyVar.traektoria3D[1], MyVar.traektoria3D[2]);
            }

            GL.End();


            foreach (var gran in MyVar.Grans)
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
            GL.Rotate(-MyVar.Xalfa, 1, 0, 0);
            GL.Rotate(-MyVar.Zalfa, 0, 0, 1);

            GL.Translate(-MyVar.pos.X, -MyVar.pos.Y, MyVar.Yalfa);
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
                GL.LoadIdentity();
                GL.Frustum(-0.1, 0.1, -0.1, 0.1, 0.2, 1000);
                MyVar.Perspective = false;
            }

            if (MyVar.Orthogonal)
            {
                GL.LoadIdentity();
                GL.Ortho(-10, 10, -10, 10, 10, -10);
                MyVar.Orthogonal = false;
            }

//Солнышко
            GL.PushMatrix();
            GL.Rotate(MyVar.SunRotate, 0, 1, 0);
            float[] pos = new float[] { 0, 0, 1, 0 };
            GL.Light(LightName.Light0, LightParameter.Position, pos);
            GL.Translate(0, 0, 20);
            GL.Color3(Color.White);
            Kvadrat();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Scale(10, 10, 10);
            GL.Translate(0, 0, -1);
            ShowFool(MyVar.Textures);
            GL.PopMatrix();

            GL.Scale(MyVar.mashtab[0], MyVar.mashtab[1], MyVar.mashtab[2]);
            GL.Rotate(MyVar.rotate[0], MyVar.rotate[1], MyVar.rotate[2], MyVar.rotate[3]);

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
                GL.PushMatrix();
                ShowNormal(new Vector3(1, 1, 2), new Vector3(1, 1, 3));
                GL.PopMatrix();
                GL.PushMatrix();
                ShowNormal(new Vector3(1, -1, 2), new Vector3(1, -1, 3));
                GL.PopMatrix();
                // ShowNormal(new Vector3(-1,1,2),new Vector3(-1,1,3));
                // ShowNormal(new Vector3(-1,-1,2),new Vector3(-1,-1,3));
            }


            GL.PopMatrix();
            MyVar.SunRotate++;
        }
    }
}