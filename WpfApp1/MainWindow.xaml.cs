using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AgentOctal.WpfLib;
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

    private void SaveLightItemCommand(object sender, RoutedEventArgs e)
    {
        var select = LightComboBox.SelectedIndex;
        switch (LightComboBox.SelectedItem)
        {
            case DirectedLight:
            {
                var item = LightItemsControl.ItemsSource.Cast<SearchFieldInfo>().ToArray();
                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("Name")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], item[0].Value);
                LightComboBox.SelectedValue = item[0].Value;

                var pos = item[1].Value.Split(", ");
                if (pos.Length != 3)
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                var position = new float[4];
                if (!float.TryParse(pos[0], out position[0]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(pos[1], out position[1]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(pos[2], out position[2]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                position[3] = 0;

                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("Position")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], item[1].Value);
                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("PositionArray")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], position);

                var col = item[2].Value.Split(", ");
                if (col.Length != 3)
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                var colors = new float[3];
                if (!float.TryParse(col[0], out colors[0]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(col[1], out colors[1]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(col[2], out colors[2]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (colors.Any(color => color is < 0 or > 1))
                {
                    MessageBox.Show("Color must be in range [0; 1].");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("Color")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], item[2].Value);
                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("ColorArray")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], colors);

                break;
            }
            case PointLight:
            {
                var item = LightItemsControl.ItemsSource.Cast<SearchFieldInfo>().ToArray();
                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("Name")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], item[0].Value);
                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("Position")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], item[1].Value);
                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("Color")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], item[2].Value);

                var a = float.Parse(item[3].Value);
                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("ConstantAttenuation")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], float.Parse(item[3].Value));
                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("LinearAttenuation")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], float.Parse(item[4].Value));
                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("QuadraticAttenuation")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], float.Parse(item[5].Value));

                var pos = item[1].Value.Split(", ");
                if (pos.Length != 3)
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                var position = new float[4];
                if (!float.TryParse(pos[0], out position[0]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(pos[1], out position[1]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(pos[2], out position[2]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                position[3] = 1;

                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("PositionArray")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], position);

                var col = item[2].Value.Split(", ");
                var colors = new float[3];
                if (col.Length != 3)
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(col[0], out colors[0]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(col[1], out colors[1]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(col[2], out colors[2]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (colors.Any(color => color is < 0 or > 1))
                {
                    MessageBox.Show("Color must be in range [0; 1].");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("ColorArray")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], colors);

                break;
            }
            case Spotlight:
            {
                var item = LightItemsControl.ItemsSource.Cast<SearchFieldInfo>().ToArray();
                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("Name")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], item[0].Value);
                if (float.TryParse(item[4].Value, out var constantAttentuation))
                {
                    MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("ConstantAttenuation")
                        ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], constantAttentuation);
                }
                else
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (float.TryParse(item[5].Value, out var linearAttenuation))
                {
                    MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("LinearAttenuation")
                        ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], linearAttenuation);
                }
                else
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (float.TryParse(item[6].Value, out var quadraticAttenuation))
                {
                    MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("QuadraticAttenuation")
                        ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], quadraticAttenuation);
                }
                else
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (float.TryParse(item[7].Value, out var spotCutoff))
                {
                    if (spotCutoff is <= 180 or >= 0)
                    {
                        MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("SpotCutoff")
                            ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], spotCutoff);
                    }
                    else
                    {
                        MessageBox.Show("SpotCutoff must be in range [0; 180].");
                        LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (float.TryParse(item[8].Value, out var exponent))
                {
                    if (exponent >= 0)
                    {
                        MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("Exponent")
                            ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], exponent);
                    }
                    else
                    {
                        MessageBox.Show("Unable to save parameters, try again.");
                        LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                var pos = item[1].Value.Split(", ");
                var position = new float[4];
                if (pos.Length != 3)
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(pos[0], out position[0]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(pos[1], out position[1]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(pos[2], out position[2]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                position[3] = 1;


                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("Position")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], item[1].Value);
                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("PositionArray")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], position);

                var col = item[2].Value.Split(", ");
                var colors = new float[3];
                if (col.Length != 3)
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(col[0], out colors[0]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(col[1], out colors[1]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(col[2], out colors[2]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (colors.Any(color => color is < 0 or > 1))
                {
                    MessageBox.Show("Color must be in range [0; 1].");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("Color")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], item[2].Value);
                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("ColorArray")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], colors);

                var spot = item[3].Value.Split(", ");
                var spotDir = new float[3];
                if (spot.Length != 3)
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(spot[0], out spotDir[0]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(spot[1], out spotDir[1]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                if (!float.TryParse(spot[2], out spotDir[2]))
                {
                    MessageBox.Show("Unable to save parameters, try again.");
                    LightComboBox.SelectedItem = LightComboBox.SelectedItem;
                    return;
                }

                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("SpotDirection")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], item[3].Value);
                MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("SpotDirectionArray")
                    ?.SetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex], spotDir);

                break;
            }
            default:
                throw new TypeLoadException("No such light type");
        }

        ObservableCollection<object> Light = new ObservableCollection<object>();

        foreach (var item in LightComboBox.Items)
        {
            Light.Add(item);
        }

        MainWindowVm.LightItems.Clear();
        foreach (var item in Light)
        {
            MainWindowVm.LightItems.Add(item);
        }

        LightComboBox.SelectedItem = LightComboBox.Items[select];
    }

    public void DeleteLightItemCommand(object sender, RoutedEventArgs e)
    {
        var type = MainWindowVm.LightItems[LightComboBox.SelectedIndex].GetType().GetProperty("LightType")
            ?.GetValue(MainWindowVm.LightItems[LightComboBox.SelectedIndex]);

        switch (type)
        {
            case LightType.DirectedLight:
            {
                MenuItemLight0.IsEnabled = true;
                ContextMenuItemLight0.IsEnabled = true;
                break;
            }
            case LightType.PointLightIntensiveOff:
            {
                MenuItemLight1.IsEnabled = true;
                ContextMenuItemLight1.IsEnabled = true;
                break;
            }
            case LightType.PointLightIntensiveOn:
            {
                MenuItemLight2.IsEnabled = true;
                ContextMenuItemLight2.IsEnabled = true;
                break;
            }
            case LightType.SpotlightIntensiveOff:
            {
                MenuItemLight3.IsEnabled = true;
                ContextMenuItemLight3.IsEnabled = true;
                break;
            }
            case LightType.SpotlightIntensiveOn:
            {
                MenuItemLight4.IsEnabled = true;
                ContextMenuItemLight4.IsEnabled = true;
                break;
            }
            case LightType.Light5:
            {
                MenuItemLight5.IsEnabled = true;
                ContextMenuItemLight5.IsEnabled = true;
                break;
            }
            case LightType.Light6:
            {
                MenuItemLight6.IsEnabled = true;
                ContextMenuItemLight6.IsEnabled = true;
                break;
            }
            case LightType.Light7:
            {
                MenuItemLight7.IsEnabled = true;
                ContextMenuItemLight7.IsEnabled = true;
                break;
            }
            default:
                throw new TypeLoadException("No such light type");
        }


        MainWindowVm.LightItems.RemoveAt(LightComboBox.SelectedIndex);

        if (MainWindowVm.LightItems.Any())
        {
            LightComboBox.SelectedItem = MainWindowVm.LightItems.Last();
        }
        else
        {
            DeleteLightItemButton.IsEnabled = false;
            SaveLightItemButton.IsEnabled = false;
            LightComboBox.IsEnabled = false;
        }
    }
    #region AddLight

    public void AddLight0ItemCommand(object sender, RoutedEventArgs e)
    {
        MainWindowVm.LightItems.Add(new DirectedLight(LightType.DirectedLight));
        LightComboBox.SelectedItem = MainWindowVm.LightItems.Last();
        DeleteLightItemButton.IsEnabled = true;
        SaveLightItemButton.IsEnabled = true;
        MenuItemLight0.IsEnabled = false;
        ContextMenuItemLight0.IsEnabled = false;
        LightComboBox.IsEnabled = true;
    }

    public void AddLight1ItemCommand(object sender, RoutedEventArgs e)
    {
        MainWindowVm.LightItems.Add(new PointLight(LightType.PointLightIntensiveOff));
        LightComboBox.SelectedItem = MainWindowVm.LightItems.Last();
        DeleteLightItemButton.IsEnabled = true;
        SaveLightItemButton.IsEnabled = true;
        MenuItemLight1.IsEnabled = false;
        ContextMenuItemLight1.IsEnabled = false;  
        LightComboBox.IsEnabled = true;
    }

    public void AddLight2ItemCommand(object sender, RoutedEventArgs e)
    {
        MainWindowVm.LightItems.Add(new PointLight(LightType.PointLightIntensiveOn));
        LightComboBox.SelectedItem = MainWindowVm.LightItems.Last();
        DeleteLightItemButton.IsEnabled = true;
        SaveLightItemButton.IsEnabled = true;
        MenuItemLight2.IsEnabled = false;
        ContextMenuItemLight2.IsEnabled = false;
        LightComboBox.IsEnabled = true;
    }

    public void AddLight3ItemCommand(object sender, RoutedEventArgs e)
    {
        MainWindowVm.LightItems.Add(new Spotlight(LightType.SpotlightIntensiveOff));
        LightComboBox.SelectedItem = MainWindowVm.LightItems.Last();
        DeleteLightItemButton.IsEnabled = true;
        SaveLightItemButton.IsEnabled = true;
        MenuItemLight3.IsEnabled = false;
        ContextMenuItemLight3.IsEnabled = false;
        LightComboBox.IsEnabled = true;
    }

    public void AddLight4ItemCommand(object sender, RoutedEventArgs e)
    {
        MainWindowVm.LightItems.Add(new Spotlight(LightType.SpotlightIntensiveOn));
        LightComboBox.SelectedItem = MainWindowVm.LightItems.Last();
        DeleteLightItemButton.IsEnabled = true;
        SaveLightItemButton.IsEnabled = true;
        MenuItemLight4.IsEnabled = false;
        ContextMenuItemLight4.IsEnabled = false;
        LightComboBox.IsEnabled = true;
    }

    public void AddLight5ItemCommand(object sender, RoutedEventArgs e)
    {
        if(Equals(sender, MenuPointItemLight5))
            MainWindowVm.LightItems.Add(new PointLight(LightType.Light5));
        if(Equals(sender, MenuSpotItemLight5))
            MainWindowVm.LightItems.Add(new Spotlight(LightType.Light5));

        LightComboBox.SelectedItem = MainWindowVm.LightItems.Last();
        DeleteLightItemButton.IsEnabled = true;
        SaveLightItemButton.IsEnabled = true;
        MenuItemLight5.IsEnabled = false;
        ContextMenuItemLight5.IsEnabled = false;
        LightComboBox.IsEnabled = true;
    }

    public void AddLight6ItemCommand(object sender, RoutedEventArgs e)
    {
        if(Equals(sender, MenuPointItemLight6))
            MainWindowVm.LightItems.Add(new PointLight(LightType.Light6));
        if(Equals(sender, MenuSpotItemLight6))
            MainWindowVm.LightItems.Add(new Spotlight(LightType.Light6));
        LightComboBox.SelectedItem = MainWindowVm.LightItems.Last();
        DeleteLightItemButton.IsEnabled = true;
        SaveLightItemButton.IsEnabled = true;
        MenuItemLight6.IsEnabled = false;
        ContextMenuItemLight6.IsEnabled = false;
        LightComboBox.IsEnabled = true;
    }

    public void AddLight7ItemCommand(object sender, RoutedEventArgs e)
    {
        if(Equals(sender, MenuPointItemLight7))
            MainWindowVm.LightItems.Add(new PointLight(LightType.Light7));
        if(Equals(sender, MenuSpotItemLight7))
            MainWindowVm.LightItems.Add(new Spotlight(LightType.Light7));
        LightComboBox.SelectedItem = MainWindowVm.LightItems.Last();
        DeleteLightItemButton.IsEnabled = true;
        SaveLightItemButton.IsEnabled = true;
        MenuItemLight7.IsEnabled = false;
        ContextMenuItemLight7.IsEnabled = false;
        LightComboBox.IsEnabled = true;
    }
    #endregion

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
                case Key.E:
                    if (RenderingSettings.SunRotation == 1)
                    {
                        RenderingSettings.SunRotation = 0;
                    }
                    else if (RenderingSettings.SunRotation == 0)
                    {
                        RenderingSettings.SunRotation = 1;
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
                GridLight.IsEnabled = false;
                SaveLightItemButton.IsEnabled = false;
                DeleteLightItemButton.IsEnabled = false;
            }
            else
            {
                MyWindow.Cursor = Cursors.Arrow;
                GridLight.IsEnabled = true;
                SaveLightItemButton.IsEnabled = true;
                DeleteLightItemButton.IsEnabled = true;
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
        if (Equals(sender, Orthogonal) || (Equals(sender, OrthogonalMenu)))
        {
            RenderingSettings.IsOrthogonalProjectionOn = true;
        }

        if (Equals(sender, Perspective) || Equals(sender, PerspectiveMenu))
        {
            RenderingSettings.IsPerspectiveProjectionOn = true;
        }

        if (Equals(sender, Object) || Equals(sender, ObjectMenu))
        {
            RenderingSettings.IsObjectVisible = !RenderingSettings.IsObjectVisible;
        }

        if (Equals(sender, Skeleton) || Equals(sender, SkeletonMenu))
        {
            RenderingSettings.IsCarcaseVisible = !RenderingSettings.IsCarcaseVisible;
        }

        if (Equals(sender, Textures) || Equals(sender, TexturesMenu))
        {
            RenderingSettings.IsTexturesVisible = !RenderingSettings.IsTexturesVisible;
        }

        if (Equals(sender, Normals) || Equals(sender, NormalsMenu))
        {
            RenderingSettings.IsNormalsVisible = !RenderingSettings.IsNormalsVisible;
        }

        if (Equals(sender, Dis) || Equals(sender, DisMenu))
        {
            RenderingSettings.IsNormalSmoothingDisabled = true;
        }

        if (Equals(sender, En) || Equals(sender, EnMenu))
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

    private void LightComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        LightComboBox.SelectedItem = LightComboBox.SelectedItem;
    }
}