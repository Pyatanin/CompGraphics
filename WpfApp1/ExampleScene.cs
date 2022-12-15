using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using WpfApp1.Light;
using WpfApp1.Model;

namespace WpfApp1;

/// Example class handling the rendering for OpenGL.
public static class ExampleScene
{
    #region MyRegion

    private static Figure3D _renderingReplicatedFigure3D;

    private static readonly Floor RenderingFloor = new
    (
        new float[]
        {
            10, 10, -2,
            10, -10, -2,
            -10, -10, -2,
            -10, 10, -1
        },
        new float[]
        {
            10, 0,
            0, 0,
            0, 10,
            10, 10
        },
        "Texture/Up.jpg"
    );

    private static readonly Sun RenderingSun = new
    (
        new float[] { 1, 1, 0, 1, -1, 0, -1, -1, 0, -1, 1, 0 },
        "Texture/4.png",
        new float[] { 1, 0, 0, 0, 0, 1, 1, 1 }
    );

    public static void Ready()
    {
        var inputData = JsonSerializer.Deserialize<InputData>(File.ReadAllText("File/InputData.json"));
        _renderingReplicatedFigure3D = new Figure3D
        (
            inputData.BasicPlane,
            inputData.ReplicationVector,
            inputData.RotationVector,
            inputData.ScaleVector,
            inputData.TextureOverlayCoords,
            "Texture/Down.jpg",
            "Texture/Up.jpg",
            "Texture/Side.jpg"
        );
        if (MainWindow.RenderingSettings.IsPerspectiveProjectionOn)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Frustum(-0.1, 0.1, -0.1, 0.1, 0.2, 1000);
            MainWindow.RenderingSettings.IsPerspectiveProjectionOn = false;
        }

        if (MainWindow.RenderingSettings.IsOrthogonalProjectionOn)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-10, 10, -10, 10, 10, -10);
            MainWindow.RenderingSettings.IsOrthogonalProjectionOn = false;
        }

        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadIdentity();

        GL.Enable(EnableCap.DepthTest);

        GL.Enable(EnableCap.Lighting);
        GL.Enable(EnableCap.ColorMaterial);

        GL.Enable(EnableCap.Normalize);
    }

    private static void ShowSun(bool texture)
    {
        if (texture)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, RenderingSun.SunTexture.BufferId);
            GL.BufferData(BufferTarget.ArrayBuffer,
                RenderingSun.TextureOverlayCoordinates.Length * sizeof(float),
                RenderingSun.TextureOverlayCoordinates,
                BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.Enable(EnableCap.Texture2D);
            RenderingSun.SunTexture.Bind();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        GL.EnableClientState(ArrayCap.VertexArray);

        GL.VertexPointer(3, VertexPointerType.Float, 0, RenderingSun.SunCoordinates);
        GL.DrawArrays(BeginMode.TriangleFan, 0, 4);

        if (texture)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            RenderingSun.SunTexture.Unbind();
        }

        GL.DisableClientState(ArrayCap.VertexArray);
        if (texture)
        {
            GL.DisableClientState(ArrayCap.TextureCoordArray);
        }
    }


    private static void ShowNormals()
    {
        foreach (var normal in _renderingReplicatedFigure3D.BasicPlaneNormals.Normals)
        {
            GL.PushMatrix();
            ShowNormal(normal.StartPoint, normal.EndPoint);
            GL.PopMatrix();
        }

        foreach (var normal in _renderingReplicatedFigure3D.ReplicatedPlaneNormals.Normals)
        {
            GL.PushMatrix();
            ShowNormal(normal.StartPoint, normal.EndPoint);
            GL.PopMatrix();
        }

        foreach (var edgesNormals in _renderingReplicatedFigure3D.EdgesNormals)
        {
            foreach (var normal in edgesNormals.Normals)
            {
                GL.PushMatrix();
                ShowNormal(normal.StartPoint, normal.EndPoint);
                GL.PopMatrix();
            }
        }

        foreach (var normal in RenderingFloor.FloorNormals.Normals)
        {
            GL.PushMatrix();
            ShowNormal(normal.StartPoint, normal.EndPoint);
            GL.PopMatrix();
        }
    }

    private static void ShowNormal(Vector3 start, Vector3 end)
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

    private static void ShowFloor(bool texture)
    {
        if (texture)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, RenderingFloor.FloorTexture.BufferId);

            GL.BufferData(BufferTarget.ArrayBuffer,
                RenderingFloor.TextureOverlayCoordinates.Length * sizeof(float),
                RenderingFloor.TextureOverlayCoordinates,
                BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.Enable(EnableCap.Texture2D);
            RenderingFloor.FloorTexture.Bind();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        GL.EnableClientState(ArrayCap.VertexArray);
        GL.EnableClientState(ArrayCap.NormalArray);
        GL.PushMatrix();

        GL.VertexPointer(3, VertexPointerType.Float, 0, RenderingFloor.Coordinates);
        GL.NormalPointer(NormalPointerType.Float, 0, RenderingFloor.FloorNormals.ToArray());

        GL.DrawArrays(BeginMode.TriangleFan, 0, 4);
        if (texture)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            RenderingFloor.FloorTexture.Unbind();
        }

        GL.PopMatrix();

        GL.DisableClientState(ArrayCap.VertexArray);
        GL.DisableClientState(ArrayCap.NormalArray);

        if (texture)
        {
            GL.DisableClientState(ArrayCap.TextureCoordArray);
        }
    }

    private static void Show3d(bool texture)
    {
        GL.EnableClientState(ArrayCap.VertexArray);
        GL.EnableClientState(ArrayCap.NormalArray);
        GL.Color3(Color.White);

        // Основание
        if (texture)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _renderingReplicatedFigure3D.BasicPlaneTexture.BufferId);
            GL.BufferData(BufferTarget.ArrayBuffer,
                _renderingReplicatedFigure3D.TextureOverlayCoordinates.Length * sizeof(float),
                _renderingReplicatedFigure3D.TextureOverlayCoordinates,
                BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.Enable(EnableCap.Texture2D);
            _renderingReplicatedFigure3D.BasicPlaneTexture.Bind();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        GL.PushMatrix();
        GL.VertexPointer(3, VertexPointerType.Float, 0, _renderingReplicatedFigure3D.BasicPlane);

        GL.NormalPointer(NormalPointerType.Float, 0, _renderingReplicatedFigure3D.BasicPlaneNormals.ToArray());
        GL.DrawArrays(BeginMode.TriangleFan, 0, _renderingReplicatedFigure3D.BasicPlane.Length / 3);
        if (texture)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            _renderingReplicatedFigure3D.BasicPlaneTexture.Unbind();
        }

        GL.PopMatrix();

        // Тиражирование
        GL.PushMatrix();
        if (texture)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _renderingReplicatedFigure3D.ReplicatedPlaneTexture.BufferId);
            GL.BufferData(BufferTarget.ArrayBuffer,
                _renderingReplicatedFigure3D.TextureOverlayCoordinates.Length * sizeof(float),
                _renderingReplicatedFigure3D.TextureOverlayCoordinates,
                BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.Enable(EnableCap.Texture2D);
            _renderingReplicatedFigure3D.ReplicatedPlaneTexture.Bind();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        GL.VertexPointer(3, VertexPointerType.Float, 0, _renderingReplicatedFigure3D.ReplicatedPlane);
        GL.NormalPointer(NormalPointerType.Float, 0, _renderingReplicatedFigure3D.ReplicatedPlaneNormals.ToArray());
        GL.DrawArrays(BeginMode.TriangleFan, 0, _renderingReplicatedFigure3D.ReplicatedPlane.Length / 3);
        if (texture)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            _renderingReplicatedFigure3D.ReplicatedPlaneTexture.Unbind();
        }

        GL.PopMatrix();

        // Грани
        var i = 0;
        foreach (var edge in _renderingReplicatedFigure3D.Edges)
        {
            GL.PushMatrix();
            if (texture)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, _renderingReplicatedFigure3D.EdgesTexture.BufferId);
                GL.BufferData
                (
                    BufferTarget.ArrayBuffer,
                    _renderingReplicatedFigure3D.TextureOverlayCoordinates.Length * sizeof(float),
                    _renderingReplicatedFigure3D.TextureOverlayCoordinates,
                    BufferUsageHint.StaticDraw
                );
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                GL.EnableClientState(ArrayCap.TextureCoordArray);
                GL.Enable(EnableCap.Texture2D);
                _renderingReplicatedFigure3D.EdgesTexture.Bind();
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            }

            GL.VertexPointer(3, VertexPointerType.Float, 0, edge);

            GL.NormalPointer(NormalPointerType.Float, 0, _renderingReplicatedFigure3D.EdgesNormals[i].ToArray());

            GL.DrawArrays(BeginMode.TriangleFan, 0, edge.Length / 3);
            if (texture)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                _renderingReplicatedFigure3D.EdgesTexture.Unbind();
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

    private static void Carcase()
    {
        GL.LineWidth(10);
        GL.Begin(BeginMode.LineLoop);
        GL.Color3(Color.Black);


        for (var i = 0; i < _renderingReplicatedFigure3D.BasicPlane.Length; i += 3)
        {
            GL.Vertex3(_renderingReplicatedFigure3D.BasicPlane[i], _renderingReplicatedFigure3D.BasicPlane[i + 1],
                _renderingReplicatedFigure3D.BasicPlane[i + 2]);
        }

        GL.End();

        GL.Begin(BeginMode.LineLoop);

        for (var i = 0; i < _renderingReplicatedFigure3D.ReplicatedPlane.Length; i += 3)
        {
            GL.Vertex3
            (
                _renderingReplicatedFigure3D.ReplicatedPlane[i],
                _renderingReplicatedFigure3D.ReplicatedPlane[i + 1],
                _renderingReplicatedFigure3D.ReplicatedPlane[i + 2]
            );
        }

        GL.End();

        foreach (var edge in _renderingReplicatedFigure3D.Edges)
        {
            GL.Begin(BeginMode.LineLoop);

            for (var i = 0; i < edge.Length; i += 3)
            {
                GL.Vertex3(edge[i], edge[i + 1], edge[i + 2]);
            }

            GL.End();
        }
    }

    private static void MoveCamera()
    {
        if (MainWindow.CameraState.CurrentCameraSpeed != 0)
        {
            MainWindow.CameraState.XAxisCameraPosition += Math.Sin(MainWindow.CameraState.CurrentCameraAngle) *
                                                          MainWindow.CameraState.CurrentCameraSpeed;
            MainWindow.CameraState.YAxisCameraPosition += Math.Cos(MainWindow.CameraState.CurrentCameraAngle) *
                                                          MainWindow.CameraState.CurrentCameraSpeed;
        }

        MainWindow.CameraState.CurrentCameraAngle = -MainWindow.CameraState.VerticalCameraAngle / 180 * Math.PI;

        if (MainWindow.RenderingSettings.IsMouseMoveForPerspectiveProjection)
        {
            GL.Rotate(-MainWindow.CameraState.HorizontalCameraAngle, 1, 0, 0);
            GL.Rotate(-MainWindow.CameraState.VerticalCameraAngle, 0, 0, 1);
            GL.Translate(-MainWindow.CameraState.XAxisCameraPosition, -MainWindow.CameraState.YAxisCameraPosition,
                MainWindow.CameraState.ZAxisCameraPosition);
        }
        else
        {
            GL.Rotate(MainWindow.CameraState.HorizontalCameraAngle, 1, 0, 0);
            GL.Rotate(MainWindow.CameraState.VerticalCameraAngle, 0, 0, 1);
            GL.Translate(MainWindow.CameraState.XAxisCameraPosition, MainWindow.CameraState.YAxisCameraPosition,
                MainWindow.CameraState.ZAxisCameraPosition);
        }

        MainWindow.CameraState.CurrentCameraSpeed = 0;
    }

    private static void MoveMouse()
    {
        if (MainWindow.RenderingSettings.IsRenderingUnderControl)
        {
            if (!MainWindow.RenderingSettings.IsMouseOnScreen) return;
            var cur = MainWindow.GetCursorPosition();

            MainWindow.CameraState.VerticalCameraAngle += (Constants.BaseCursorPoint.X - cur.X) / 30;
            if (MainWindow.CameraState.VerticalCameraAngle < 0) MainWindow.CameraState.VerticalCameraAngle += 360;
            if (MainWindow.CameraState.VerticalCameraAngle > 360) MainWindow.CameraState.VerticalCameraAngle -= 360;

            MainWindow.CameraState.HorizontalCameraAngle += (Constants.BaseCursorPoint.Y - cur.Y) / 30;
            if (MainWindow.CameraState.HorizontalCameraAngle < 0) MainWindow.CameraState.HorizontalCameraAngle = 0;
            if (MainWindow.CameraState.HorizontalCameraAngle > 180) MainWindow.CameraState.HorizontalCameraAngle = 180;

            MainWindow.SetCursorPos((int)Constants.BaseCursorPoint.X, (int)Constants.BaseCursorPoint.Y);
        }
    }

    #endregion

    private static void EnableLightSource(DirectedLight lightSource)
    {
        GL.Enable(EnableCap.Light0);
        GL.Light(LightName.Light0, LightParameter.Position, lightSource.PositionArray);
        GL.Light(LightName.Light0, LightParameter.Diffuse, lightSource.ColorArray);
    }

    private static void EnableLightSource(PointLight lightSource)
    {
        switch (lightSource.LightType)
        {
            case LightType.PointLightIntensiveOff:
            {
                GL.Enable(EnableCap.Light1);
                GL.Light(LightName.Light1, LightParameter.Position, lightSource.PositionArray);
                GL.Light(LightName.Light1, LightParameter.Diffuse, lightSource.ColorArray);
                GL.Light(LightName.Light2, LightParameter.ConstantAttenuation, lightSource.ConstantAttenuation);
                GL.Light(LightName.Light2, LightParameter.LinearAttenuation, lightSource.LinearAttenuation);
                GL.Light(LightName.Light2, LightParameter.QuadraticAttenuation, lightSource.QuadraticAttenuation);
                break;
            }
            case LightType.PointLightIntensiveOn:
            {
                GL.Enable(EnableCap.Light2);
                GL.Light(LightName.Light2, LightParameter.Position, lightSource.PositionArray);
                GL.Light(LightName.Light2, LightParameter.Diffuse, lightSource.ColorArray);
                GL.Light(LightName.Light2, LightParameter.ConstantAttenuation, lightSource.ConstantAttenuation);
                GL.Light(LightName.Light2, LightParameter.LinearAttenuation, lightSource.LinearAttenuation);
                GL.Light(LightName.Light2, LightParameter.QuadraticAttenuation, lightSource.QuadraticAttenuation);
                break;
            }
            case LightType.Light5:
            {
                GL.Enable(EnableCap.Light5);
                GL.Light(LightName.Light5, LightParameter.Position, lightSource.PositionArray);
                GL.Light(LightName.Light5, LightParameter.Diffuse, lightSource.ColorArray);
                GL.Light(LightName.Light5, LightParameter.ConstantAttenuation, lightSource.ConstantAttenuation);
                GL.Light(LightName.Light5, LightParameter.LinearAttenuation, lightSource.LinearAttenuation);
                GL.Light(LightName.Light5, LightParameter.QuadraticAttenuation, lightSource.QuadraticAttenuation);
                break;
            }case LightType.Light6:
            {
                GL.Enable(EnableCap.Light6);
                GL.Light(LightName.Light6, LightParameter.Position, lightSource.PositionArray);
                GL.Light(LightName.Light6, LightParameter.Diffuse, lightSource.ColorArray);
                GL.Light(LightName.Light6, LightParameter.ConstantAttenuation, lightSource.ConstantAttenuation);
                GL.Light(LightName.Light6, LightParameter.LinearAttenuation, lightSource.LinearAttenuation);
                GL.Light(LightName.Light6, LightParameter.QuadraticAttenuation, lightSource.QuadraticAttenuation);
                break;
            }case LightType.Light7:
            {
                GL.Enable(EnableCap.Light7);
                GL.Light(LightName.Light7, LightParameter.Position, lightSource.PositionArray);
                GL.Light(LightName.Light7, LightParameter.Diffuse, lightSource.ColorArray);
                GL.Light(LightName.Light7, LightParameter.ConstantAttenuation, lightSource.ConstantAttenuation);
                GL.Light(LightName.Light7, LightParameter.LinearAttenuation, lightSource.LinearAttenuation);
                GL.Light(LightName.Light7, LightParameter.QuadraticAttenuation, lightSource.QuadraticAttenuation);
                break;
            }
        }
    }

    private static void EnableLightSource(Spotlight lightSource)
    {
        switch (lightSource.LightType)
        {
            case LightType.SpotlightIntensiveOff:
            {
                GL.Enable(EnableCap.Light3);
                GL.Light(LightName.Light3, LightParameter.Position, lightSource.PositionArray);
                GL.Light(LightName.Light3, LightParameter.Diffuse, lightSource.ColorArray);
                GL.Light(LightName.Light3, LightParameter.SpotCutoff, lightSource.SpotCutoff);
                GL.Light(LightName.Light3, LightParameter.SpotDirection, lightSource.SpotDirectionArray);
                GL.Light(LightName.Light3, LightParameter.ConstantAttenuation, lightSource.ConstantAttenuation);
                GL.Light(LightName.Light3, LightParameter.LinearAttenuation, lightSource.LinearAttenuation);
                GL.Light(LightName.Light3, LightParameter.QuadraticAttenuation, lightSource.QuadraticAttenuation);
                GL.Light(LightName.Light3, LightParameter.SpotExponent, lightSource.Exponent);
                break;
            }
            case LightType.SpotlightIntensiveOn:
            {
                GL.Enable(EnableCap.Light4);
                GL.Light(LightName.Light4, LightParameter.Position, lightSource.PositionArray);
                GL.Light(LightName.Light4, LightParameter.Diffuse, lightSource.ColorArray);
                GL.Light(LightName.Light4, LightParameter.SpotCutoff, lightSource.SpotCutoff);
                GL.Light(LightName.Light4, LightParameter.SpotDirection, lightSource.SpotDirectionArray);
                GL.Light(LightName.Light4, LightParameter.ConstantAttenuation, lightSource.ConstantAttenuation);
                GL.Light(LightName.Light4, LightParameter.LinearAttenuation, lightSource.LinearAttenuation);
                GL.Light(LightName.Light4, LightParameter.QuadraticAttenuation, lightSource.QuadraticAttenuation);
                GL.Light(LightName.Light4, LightParameter.SpotExponent, lightSource.Exponent);
                break;
            }
            case LightType.Light5:
            {
                GL.Enable(EnableCap.Light5);
                GL.Light(LightName.Light5, LightParameter.Position, lightSource.PositionArray);
                GL.Light(LightName.Light5, LightParameter.Diffuse, lightSource.ColorArray);
                GL.Light(LightName.Light5, LightParameter.SpotCutoff, lightSource.SpotCutoff);
                GL.Light(LightName.Light5, LightParameter.SpotDirection, lightSource.SpotDirectionArray);
                GL.Light(LightName.Light5, LightParameter.ConstantAttenuation, lightSource.ConstantAttenuation);
                GL.Light(LightName.Light5, LightParameter.LinearAttenuation, lightSource.LinearAttenuation);
                GL.Light(LightName.Light5, LightParameter.QuadraticAttenuation, lightSource.QuadraticAttenuation);
                GL.Light(LightName.Light5, LightParameter.SpotExponent, lightSource.Exponent);
                break;
            }
            case LightType.Light6:
            {
                GL.Enable(EnableCap.Light6);
                GL.Light(LightName.Light6, LightParameter.Position, lightSource.PositionArray);
                GL.Light(LightName.Light6, LightParameter.Diffuse, lightSource.ColorArray);
                GL.Light(LightName.Light6, LightParameter.SpotCutoff, lightSource.SpotCutoff);
                GL.Light(LightName.Light6, LightParameter.SpotDirection, lightSource.SpotDirectionArray);
                GL.Light(LightName.Light6, LightParameter.ConstantAttenuation, lightSource.ConstantAttenuation);
                GL.Light(LightName.Light6, LightParameter.LinearAttenuation, lightSource.LinearAttenuation);
                GL.Light(LightName.Light6, LightParameter.QuadraticAttenuation, lightSource.QuadraticAttenuation);
                GL.Light(LightName.Light6, LightParameter.SpotExponent, lightSource.Exponent);
                break;
            }
            case LightType.Light7:
            {
                GL.Enable(EnableCap.Light7);
                GL.Light(LightName.Light7, LightParameter.Position, lightSource.PositionArray);
                GL.Light(LightName.Light7, LightParameter.Diffuse, lightSource.ColorArray);
                GL.Light(LightName.Light7, LightParameter.SpotCutoff, lightSource.SpotCutoff);
                GL.Light(LightName.Light7, LightParameter.SpotDirection, lightSource.SpotDirectionArray);
                GL.Light(LightName.Light7, LightParameter.ConstantAttenuation, lightSource.ConstantAttenuation);
                GL.Light(LightName.Light7, LightParameter.LinearAttenuation, lightSource.LinearAttenuation);
                GL.Light(LightName.Light7, LightParameter.QuadraticAttenuation, lightSource.QuadraticAttenuation);
                GL.Light(LightName.Light7, LightParameter.SpotExponent, lightSource.Exponent);
                break;
            }
        }
    }

    public static void Render(float alpha = 1.0f)
    {
        #region MyRegion

        GL.ClearColor(Color4.LightBlue);
        GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.Clear(ClearBufferMask.DepthBufferBit);

        GL.Enable(EnableCap.PointSmooth);

        GL.PushMatrix();
        MoveMouse();
        MoveCamera();

        if (MainWindow.RenderingSettings.IsPerspectiveProjectionOn)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Frustum(-0.1, 0.1, -0.1, 0.1, 0.2, 1000);
            MainWindow.RenderingSettings.IsPerspectiveProjectionOn = false;
            GL.MatrixMode(MatrixMode.Modelview);

            MainWindow.RenderingSettings.IsMouseMoveForPerspectiveProjection = true;
        }

        if (MainWindow.RenderingSettings.IsOrthogonalProjectionOn)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-30, 30, -30, 30, 30, -30);
            MainWindow.RenderingSettings.IsOrthogonalProjectionOn = false;
            GL.MatrixMode(MatrixMode.Modelview);

            MainWindow.RenderingSettings.IsMouseMoveForPerspectiveProjection = false;
        }

        if (MainWindow.RenderingSettings.IsNormalSmoothingDisabled)
        {
            GL.Disable(EnableCap.Normalize);
            MainWindow.RenderingSettings.IsNormalSmoothingDisabled = false;
        }

        if (MainWindow.RenderingSettings.IsNormalSmoothingEnabled)
        {
            GL.Enable(EnableCap.Normalize);
            MainWindow.RenderingSettings.IsNormalSmoothingEnabled = false;
        }

        #endregion

        // Солнышко
        GL.PushMatrix();

        GL.Rotate(RenderingSun.SunPosition, 0, 1, 0);
        foreach (var light in MainWindowVm.LightItems)
        {
            switch (light)
            {
                case DirectedLight directedLight:
                {
                    EnableLightSource(directedLight);
                    break;
                }
                case PointLight pointLight:
                {
                    EnableLightSource(pointLight);
                    break;
                }
                case Spotlight spotlight:
                {
                    EnableLightSource(spotlight);
                    break;
                }
                default:
                    throw new TypeLoadException("No such light type");
            }
        }
        GL.PopMatrix();


        // GL.PushMatrix();
        // ShowFloor(MainWindow.RenderingSettings.IsTexturesVisible);
        // GL.PopMatrix();

        GL.Scale
        (
            _renderingReplicatedFigure3D.ScaleVector[0],
            _renderingReplicatedFigure3D.ScaleVector[1],
            _renderingReplicatedFigure3D.ScaleVector[2]
        );
        GL.Rotate
        (
            _renderingReplicatedFigure3D.RotationVector[0],
            _renderingReplicatedFigure3D.RotationVector[1],
            _renderingReplicatedFigure3D.RotationVector[2],
            _renderingReplicatedFigure3D.RotationVector[3]
        );

        // GL.Rotate(3 * RenderingSun.SunPosition, 0, 0, 1);
        // GL.Rotate(4 * RenderingSun.SunPosition, 1, 1, 0);
        // GL.Rotate(2 * RenderingSun.SunPosition, 1, 0, 0);

        if (MainWindow.RenderingSettings.IsObjectVisible)
        {
            GL.PushMatrix();
            Show3d(MainWindow.RenderingSettings.IsTexturesVisible);
            GL.PopMatrix();
        }

        if (MainWindow.RenderingSettings.IsCarcaseVisible)
        {
            GL.PushMatrix();
            Carcase();
            GL.PopMatrix();
        }

        if (MainWindow.RenderingSettings.IsNormalsVisible)
        {
            GL.PushMatrix();
            ShowNormals();
            GL.PopMatrix();
        }

        GL.PopMatrix();

        GL.Disable(EnableCap.Light0);
        GL.Disable(EnableCap.Light1);
        GL.Disable(EnableCap.Light2);
        GL.Disable(EnableCap.Light3);
        GL.Disable(EnableCap.Light4);
        GL.Disable(EnableCap.Light5);
        GL.Disable(EnableCap.Light6);
        GL.Disable(EnableCap.Light7);

        // RenderingSun.SunPosition += RenderingSettings.SunRotation;
    }
}