using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using OpenTK.Wpf;
using WpfApp1.Light;
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

    public void DeleteLightItemAction()
    {
        MainWindowVm.LightItems.RemoveAt(LightComboBox.SelectedIndex);
    }
    
    private void SaveLightItemCommand(object sender, RoutedEventArgs e)
    {
        switch (LightComboBox.SelectedItem)
        {
            case DirectedLight directedLight:
            {
                var a =LightItemsControl.ItemsSource.Cast<SearchFieldInfo>().ToArray();
                var value = a[0].Value;
                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("Name")?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], value);
                break;
            }
            case PointLight pointLight:
            {
                break;
            }
            case Spotlight spotlight:
            {
                break;
            }
            default:
                throw new TypeLoadException("No such light type");
        }
    }
    
    public void DeleteLightItemCommand(object sender, RoutedEventArgs e)
    {
        MainWindowVm.LightItems.RemoveAt(LightComboBox.SelectedIndex);
        LightComboBox.SelectedItem = MainWindowVm.LightItems.Last();
    }

    public void AddLight0ItemCommand(object sender, RoutedEventArgs e)
    {
        MainWindowVm.LightItems.Add(typeof(DirectedLight));
    }

    public void AddLight1ItemCommand(object sender, RoutedEventArgs e)
    {
        MainWindowVm.LightItems.Add(new PointLight(LightType.PointLightIntensiveOff));
    }


    public void AddLight2ItemCommand(object sender, RoutedEventArgs e)
    {
        MainWindowVm.LightItems.Add(new PointLight(LightType.PointLightIntensiveOn));
    }


    public void AddLight3ItemCommand(object sender, RoutedEventArgs e)
    {
        MainWindowVm.LightItems.Add(new Spotlight(LightType.SpotlightIntensiveOff));
    }


    public void AddLight4ItemCommand(object sender, RoutedEventArgs e)
    {
        MainWindowVm.LightItems.Add(new Spotlight(LightType.SpotlightIntensiveOn));
    }

    #region Events

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
        if (RenderingSettings.IsRenderingUnderControl)
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
                case Key.D0:
                    RenderingSettings.lightMode = 0;
                    break;
                case Key.D1:
                    RenderingSettings.lightMode = 1;
                    break;
                case Key.D2:
                    RenderingSettings.lightMode = 2;
                    break;
                case Key.D3:
                    RenderingSettings.lightMode = 3;
                    break;
                case Key.D4:
                    RenderingSettings.lightMode = 4;
                    break;
                case Key.D5:
                    RenderingSettings.lightMode = 5;
                    break;
                case Key.E:
                    if (RenderingSettings.RotetSun == 1)
                    {
                        RenderingSettings.RotetSun = 0;
                    }
                    else if (RenderingSettings.RotetSun == 0)
                    {
                        RenderingSettings.RotetSun = 1;
                    }

                    break;
            }

        if (e.Key == Key.Escape)
        {
            RenderingSettings.IsRenderingUnderControl = !RenderingSettings.IsRenderingUnderControl;
            if (MyWindow.Cursor == Cursors.Arrow)
            {
                MyWindow.Cursor = Cursors.None;
                SetCursorPos((int)Constants.BaseCursorPoint.X, (int)Constants.BaseCursorPoint.Y);
            }
            else
            {
                MyWindow.Cursor = Cursors.Arrow;
            }
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
            RenderingSettings.lightMode = 0;
        }

        if (Equals(sender, Light1))
        {
            RenderingSettings.lightMode = 1;
        }

        if (Equals(sender, Light2))
        {
            RenderingSettings.lightMode = 2;
        }

        if (Equals(sender, Light3))
        {
            RenderingSettings.lightMode = 3;
        }

        if (Equals(sender, Light4))
        {
            RenderingSettings.lightMode = 4;
        }

        if (Equals(sender, Light5))
        {
            RenderingSettings.lightMode = 5;
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

    private void LightList_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        throw new NotImplementedException();
    }

    #endregion

    
}