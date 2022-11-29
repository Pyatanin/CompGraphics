using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using OpenTK.Wpf;
using WpfApp1.Model;

namespace WpfApp1;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public sealed partial class MainWindow
{
    public static readonly RenderingSettings RenderingSettings = new();
    public static readonly CameraState CameraState = new();

    public MainWindow()
    {
        InitializeComponent();
        var mainSettings = new GLWpfControlSettings { MajorVersion = 2, MinorVersion = 1 };
        OpenTkControl.Start(mainSettings);
    }

    [DllImport("User32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out Point lpPoint);

    public static System.Windows.Point GetCursorPosition()
    {
        GetCursorPos(out var lpPoint);
        return lpPoint;
    }

    private void OpenTkControl_OnRender(TimeSpan delta)
    {
        ExampleScene.Render();
    }

    private void OpenTkControl_OnReady()
    {
        ExampleScene.Ready();
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Up:
                if (CameraState.HorizontalCameraAngle < 180)
                    CameraState.HorizontalCameraAngle += Constants.RotationSpeed;
                break;
            case Key.Down:
                if (CameraState.HorizontalCameraAngle > 0)
                    CameraState.HorizontalCameraAngle -= Constants.RotationSpeed;
                break;
            case Key.Left:
                CameraState.VerticalCameraAngle += Constants.RotationSpeed;
                break;
            case Key.Right:
                CameraState.VerticalCameraAngle -= Constants.RotationSpeed;
                break;
            case Key.Space:
                CameraState.ZAxisCameraPosition -= 0.2f;
                break;
            case Key.Z:
                CameraState.ZAxisCameraPosition += 0.2f;
                break;
            case Key.W:
                CameraState.CurrentCameraSpeed = Constants.Speed;
                ;
                break;
            case Key.S:
                CameraState.CurrentCameraSpeed = -Constants.Speed;
                ;
                break;
            case Key.D:
                CameraState.CurrentCameraSpeed = Constants.Speed;
                ;
                CameraState.CurrentCameraAngle += Math.PI * 0.5;
                break;
            case Key.A:
                CameraState.CurrentCameraSpeed = Constants.Speed;
                ;
                CameraState.CurrentCameraAngle -= Math.PI * 0.5;
                break;
        }
    }

    private void MainWindow_OnMouseEnter(object sender, MouseEventArgs e)
    {
        RenderingSettings.IsMouseOnScreen = true;
    }

    private void MainWindow_OnMouseLeave(object sender, MouseEventArgs e)
    {
        RenderingSettings.IsMouseOnScreen = false;
    }

    private void OnClickChangeRenderingSettings(object sender, RoutedEventArgs e)
    {
        if (Equals(sender, Light0))
        {
            RenderingSettings.light = 0;
        }

        if (Equals(sender, Light1))
        {
            RenderingSettings.light = 1;
        }

        if (Equals(sender, Light2))
        {
            RenderingSettings.light = 2;
        }

        if (Equals(sender, Orthogonal))
        {
            RenderingSettings.IsOrthogonalProjectionOn = true;
        }

        if (Equals(sender, Perspective))
        {
            RenderingSettings.IsPerspectiveProjectionOn = true;
        }

        if (Equals(sender, Object))
        {
            RenderingSettings.IsObjectVisible = !RenderingSettings.IsObjectVisible;
        }

        if (Equals(sender, Skeleton))
        {
            RenderingSettings.IsCarcaseVisible = !RenderingSettings.IsCarcaseVisible;
        }

        if (Equals(sender, Textures))
        {
            RenderingSettings.IsTexturesVisible = !RenderingSettings.IsTexturesVisible;
        }

        if (Equals(sender, Normals))
        {
            RenderingSettings.IsNormalsVisible = !RenderingSettings.IsNormalsVisible;
        }

        if (Equals(sender, Dis))
        {
            RenderingSettings.IsNormalSmoothingDisabled = true;
        }

        if (Equals(sender, En))
        {
            RenderingSettings.IsNormalSmoothingEnabled = true;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int X;
        public int Y;

        public static implicit operator System.Windows.Point(Point point)
        {
            return new System.Windows.Point(point.X, point.Y);
        }
    }
}