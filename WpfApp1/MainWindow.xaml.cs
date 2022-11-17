using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using OpenTK.Wpf;
using WpfApp1.Model;

namespace WpfLabs;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public sealed partial class MainWindow
{
    public static Var MyVar = new();


    public MainWindow()
    {
        InitializeComponent();
        var mainSettings = new GLWpfControlSettings { MajorVersion = 2, MinorVersion = 1 };
        OpenTkControl.Start(mainSettings);
    }

    [DllImport("User32.dll")]
    public static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out POINT lpPoint);

    public static Point GetCursorPosition()
    {
        POINT lpPoint;
        GetCursorPos(out lpPoint);
        return lpPoint;
    }

    private void OpenTkControl_OnRender(TimeSpan delta)
    {
        ExampleScene.Render();
        MyTextBlock.Text = $"{(int)Mouse.GetPosition(MyGrid).X};{(int)Mouse.GetPosition(MyGrid).Y}";
        MyVar.Cur.X = Mouse.GetPosition(MyGrid).X;
        MyVar.Cur.Y = Mouse.GetPosition(MyGrid).Y;
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
                if (MyVar.Xalfa < 180)
                    MyVar.Xalfa += MyVar.constSpeedRotation;
                break;
            case Key.Down:
                if (MyVar.Xalfa > 0)
                    MyVar.Xalfa -= MyVar.constSpeedRotation;
                break;
            case Key.Left:
                MyVar.Zalfa += MyVar.constSpeedRotation;
                break;
            case Key.Right:
                MyVar.Zalfa -= MyVar.constSpeedRotation;
                break;
            case Key.Space:
                MyVar.Yalfa -= 0.2f;
                break;
            case Key.Z:
                MyVar.Yalfa += 0.2f;
                break;
            case Key.W:
                MyVar.speed = MyVar.constSpeed;
                break;
            case Key.S:
                MyVar.speed = -MyVar.constSpeed;
                break;
            case Key.D:
                MyVar.speed = MyVar.constSpeed;
                MyVar.ugol += Math.PI * 0.5;
                break;
            case Key.A:
                MyVar.speed = MyVar.constSpeed;
                MyVar.ugol -= Math.PI * 0.5;
                break;
        }
    }

    private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        //ExampleScene.WmSize(MyGrid, MyWindow);
    }


    private void MainWindow_OnMouseEnter(object sender, MouseEventArgs e)
    {
        MyVar.MouseSelect = true;
    }

    private void MainWindow_OnMouseLeave(object sender, MouseEventArgs e)
    {
        MyVar.MouseSelect = false;
    }

    private void OnClickColor(object sender, RoutedEventArgs e)
    {
        if (Equals(sender, Orthogonal))
        {
            MyVar.Orthogonal = true;
        }

        if (Equals(sender, Perspective))
        {
            MyVar.Perspective = true;
        }

        if (Equals(sender, Object))
        {
            if (MyVar.Object)
                MyVar.Object = false;
            else
                MyVar.Object = true;
        }

        if (Equals(sender, Skeleton))
        {
            if (MyVar.Skeleton)
                MyVar.Skeleton = false;
            else
                MyVar.Skeleton = true;
        }

        if (Equals(sender, Textures))
        {
            if (MyVar.Textures)
                MyVar.Textures = false;
            else
                MyVar.Textures = true;
        }

        if (Equals(sender, Normals))
        {
            if (MyVar.Normals)
                MyVar.Normals = false;
            else
                MyVar.Normals = true;
        }

        if (Equals(sender, Dis))
        {
            MyVar.NormalDis = true;
        }

        if (Equals(sender, En))
        {
            MyVar.NormalEn = true;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public static implicit operator Point(POINT point)
        {
            return new Point(point.X, point.Y);
        }
    }
}