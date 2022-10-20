using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using OpenTK.Mathematics;
using OpenTK.Wpf;
using WpfLabs.Model;

namespace WpfLabs;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public sealed partial class MainWindow
{
    public static readonly Var MyVar = new();
    public static readonly Primitives Primitives = new();
    private readonly List<MyPoint> _points = new();

    public MainWindow()
    {
        InitializeComponent();

        var mainSettings = new GLWpfControlSettings { MajorVersion = 2, MinorVersion = 1 };
        OpenTkControl.Start(mainSettings);
    }

    private void OpenTkControl_OnRender(TimeSpan delta)
    {
        ExampleScene.Render();
        MyTextBlock.Text = $"{MyWindow.Height};{MyWindow.Width}";
        MyGrid.Height = MyWindow.Height;
        MyGrid.Width = MyWindow.Width;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.D0:
            {
                if (MyVar.Mode != 4 && Primitives.ListPrimitive.Count != 0)
                {
                    Primitives.ListPrimitive[^1].Selection = false;
                }

                if (MyVar.Mode == 4 && Primitives.ListPrimitive[^1].Coordinates.Count != 0)
                {
                    Primitives.ListPrimitive[^1].Selection = false;
                    Primitives.ListPrimitive.Add
                    (
                        new Primitive
                        (
                            MyVar.Mode,
                            new List<MyPoint>(),
                            MyVar.StandardColor4,
                            true
                        )
                    );
                }

                MyVar.Mode = 0;
                break;
            }
            case Key.D1:
                MyVar.Mode = 1;
                break;
            case Key.D2:
                MyVar.Mode = 2;
                break;
            case Key.D3:
                MyVar.Mode = 3;
                break;
            case Key.D4:
                MyVar.Mode = 4;
                break;
            case Key.D9:
                MyVar.Mode = 9;
                break;
            case Key.D8:
                MyVar.Mode = 8;
                break;
            case Key.Space:
            {
                if (MyVar.Mode == 4 && Primitives.ListPrimitive[^1].Coordinates.Count != 0)
                {
                    Primitives.ListPrimitive.Add
                    (new Primitive
                        (
                            MyVar.Mode,
                            new List<MyPoint>(),
                            MyVar.StandardColor4,
                            true
                        )
                    );
                }

                break;
            }
            case Key.Delete:
            {
                for (var i = Primitives.ListPrimitive.Count - 1; i >= 0; i--)
                {
                    if (Primitives.ListPrimitive[i].Selection)
                    {
                        Primitives.ListPrimitive.RemoveAt(i);
                    }
                }

                break;
            }
        }

        if (e.Key == Key.Z || Equals(sender, Ctrl_Z))
        {
            if (Primitives.ListPrimitive.Count != 0)
            {
                Primitives.ListPrimitive.Remove(Primitives.ListPrimitive.Last());
            }
        }

        if (e.Key == Key.Back)
        {
            for (var i = Primitives.ListPrimitive.Count - 1; i >= 0; i--)
            {
                if (Primitives.ListPrimitive[i].Type == MyVar.Mode)
                {
                    Primitives.ListPrimitive.RemoveAt(i);
                }
            }
        }
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        MyVar.Height = (float)MyGrid.ActualHeight;
        MyVar.Width = (float)MyGrid.ActualWidth;
        if (e.LeftButton == MouseButtonState.Pressed && MyVar.Mode == 0)
        {
            var lUp = new Vector2(MyVar.XPosition - 10, MyVar.YPosition + 10);

            var rDown = new Vector2(MyVar.XPosition + 10, MyVar.YPosition - 10);
            if (Primitives.ListPrimitive.Count != 0)
            {
                foreach (var p in Primitives.ListPrimitive)
                {
                    foreach (var coord in p.Coordinates)
                    {
                        if (coord.Coordinates.X > lUp.X && coord.Coordinates.Y < lUp.Y)
                        {
                            if (coord.Coordinates.X < rDown.X && coord.Coordinates.Y > rDown.Y)
                            {
                                p.Selection = !p.Selection;
                            }
                        }
                    }
                }
            }
        }

        if (e.LeftButton == MouseButtonState.Pressed && MyVar.Mode == 9)
        {
            MyVar.Move = true;
            var lUp = new Vector2(MyVar.XPosition - 5, MyVar.YPosition + 5);
            var rDown = new Vector2(MyVar.XPosition + 5, MyVar.YPosition - 5);
            if (Primitives.ListPrimitive.Count != 0)
            {
                foreach (var coord in Primitives.ListPrimitive.Where(p => p.Selection).SelectMany(p => p.Coordinates))
                {
                    if (coord.Coordinates.X > lUp.X && coord.Coordinates.Y < lUp.Y)
                    {
                        if (coord.Coordinates.X < rDown.X && coord.Coordinates.Y > rDown.Y)
                        {
                            coord.IsSelected = !coord.IsSelected;
                        }
                        else
                            coord.IsSelected = false;
                    }
                    else
                        coord.IsSelected = false;
                }
            }
        }

        if (e.LeftButton == MouseButtonState.Pressed && MyVar.Mode == 8)
        {
            MyVar.Move = true;
            MyVar.Press = true;
            MyVar.DeltaX = MyVar.XPosition;
            MyVar.DeltaY = MyVar.YPosition;
        }

        if (e.LeftButton == MouseButtonState.Pressed && MyVar.Mode == 1)
        {
            if (Primitives.ListPrimitive.Count != 0)
            {
                Primitives.ListPrimitive[^1].Selection = false;
            }

            var dot = new Vector2(MyVar.XPosition, MyVar.YPosition);
            _points.Add(new MyPoint(dot, false));
            var point = new List<MyPoint>(_points);
            Primitives.ListPrimitive.Add(new Primitive(MyVar.Mode, point, MyVar.StandardColor4, true));
            _points.Clear();
        }

        if (e.LeftButton == MouseButtonState.Pressed && MyVar.Mode == 2)
        {
            if (Primitives.ListPrimitive.Count != 0)
            {
                Primitives.ListPrimitive[^1].Selection = false;
            }

            var dot = new Vector2(MyVar.XPosition, MyVar.YPosition);
            _points.Add(new MyPoint(dot, false));
            Primitives.ListPrimitive.Add(new Primitive(1, _points, MyVar.StandardColor4, true));

            if (_points.Count == 2)
            {
                var point = new List<MyPoint>(_points);
                Primitives.ListPrimitive.Add(new Primitive(MyVar.Mode, point, MyVar.StandardColor4, true));
                Primitives.ListPrimitive.RemoveAt(Primitives.ListPrimitive.Count - 2);
                Primitives.ListPrimitive.RemoveAt(Primitives.ListPrimitive.Count - 2);
                _points.Clear();
            }
        }

        if (e.LeftButton == MouseButtonState.Pressed && MyVar.Mode == 3)
        {
            if (Primitives.ListPrimitive.Count != 0)
            {
                Primitives.ListPrimitive[^1].Selection = false;
            }

            var dot = new Vector2(MyVar.XPosition, MyVar.YPosition);
            _points.Add(new MyPoint(dot, false));
            Primitives.ListPrimitive.Add(new Primitive(1, _points, MyVar.StandardColor4, true));
            if (_points.Count == 2)
            {
                Primitives.ListPrimitive.Add(new Primitive(2, _points, MyVar.StandardColor4, true));
            }

            if (_points.Count == 3)
            {
                var point = new List<MyPoint>(_points);
                Primitives.ListPrimitive.Add(new Primitive(MyVar.Mode, point, MyVar.StandardColor4, true));
                Primitives.ListPrimitive.RemoveAt(Primitives.ListPrimitive.Count - 2);
                Primitives.ListPrimitive.RemoveAt(Primitives.ListPrimitive.Count - 2);
                Primitives.ListPrimitive.RemoveAt(Primitives.ListPrimitive.Count - 2);
                Primitives.ListPrimitive.RemoveAt(Primitives.ListPrimitive.Count - 2);
                _points.Clear();
            }
        }

        if (e.LeftButton == MouseButtonState.Pressed && MyVar.Mode == 4)
        {
            if (Primitives.ListPrimitive.Count != 1)
            {
                Primitives.ListPrimitive[^2].Selection = false;
            }

            if (Primitives.ListPrimitive[^1].Type == 4)
            {
                var coord = new Vector2(MyVar.XPosition, MyVar.YPosition);
                var point = new MyPoint(coord, false);
                Primitives.ListPrimitive[^1].AddToPrimitive(point);
            }
            else
            {
                Primitives.ListPrimitive.Add
                (
                    new Primitive
                    (
                        MyVar.Mode, 
                        new List<MyPoint>(), 
                        MyVar.StandardColor4, 
                        true
                    )
                );
                
                if (Primitives.ListPrimitive.Count != 0)
                {
                    Primitives.ListPrimitive[^2].Selection = false;
                }

                var coord = new Vector2(MyVar.XPosition, MyVar.YPosition);
                var point = new MyPoint(coord, false);
                Primitives.ListPrimitive[^1].AddToPrimitive(point);
            }
        }
    }

    private void OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Released && MyVar.Mode == 9)
        {
            MyVar.Move = false;
            if (Primitives.ListPrimitive.Count != 0)
            {
                foreach (var p in Primitives.ListPrimitive)
                {
                    if (p.Selection)
                    {
                        foreach (var coord in p.Coordinates)
                        {
                            if (!coord.IsSelected) continue;

                            coord.Coordinates.X = MyVar.XPosition;
                            coord.Coordinates.Y = MyVar.YPosition;
                            coord.IsSelected = false;
                        }
                    }
                }
            }
        }

        if (e.LeftButton == MouseButtonState.Released && MyVar.Mode == 8)
        {
            MyVar.Move = false;
            MyVar.Press = false;

            if (Primitives.ListPrimitive.Count == 0) return;

            foreach (var coord in Primitives.ListPrimitive.Where(p => p.Selection).SelectMany(p => p.Coordinates))
            {
                coord.Coordinates.X -= MyVar.DeltaX - MyVar.XPosition;
                coord.Coordinates.Y -= MyVar.DeltaY - MyVar.YPosition;
                coord.IsSelected = false;
            }
        }
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        OnMouseDown();

        MyVar.XPosition = (float)(Mouse.GetPosition(MyGrid).X - (float)MyGrid.ActualWidth / 2);
        MyVar.YPosition = (float)(-Mouse.GetPosition(MyGrid).Y + (float)MyGrid.ActualHeight / 2);

        if (MyVar.Mode == 9 && MyVar.Move)
        {
            if (Primitives.ListPrimitive.Count != 0)
            {
                foreach (var p in Primitives.ListPrimitive)
                {
                    if (!p.Selection) continue;

                    foreach (var coord in p.Coordinates.Where(coord => coord.IsSelected))
                    {
                        coord.Coordinates.X = MyVar.XPosition;
                        coord.Coordinates.Y = MyVar.YPosition;
                    }
                }
            }
        }

        if (MyVar.Mode == 8 && MyVar.Move)
        {
            if (Primitives.ListPrimitive.Count == 0) return;

            foreach (var p in Primitives.ListPrimitive)
            {
                if (!p.Selection) continue;

                foreach (var coord in p.Coordinates)
                {
                    if (!(Math.Abs(MyVar.DeltaX - MyVar.XPosition) > 0.00001f) &&
                        !(Math.Abs(MyVar.DeltaY - MyVar.YPosition) > 0.00001f)) continue;

                    coord.Coordinates.X -= MyVar.DeltaX - MyVar.XPosition;
                    coord.Coordinates.Y -= MyVar.DeltaY - MyVar.YPosition;
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (MyVar.Press && MyVar.Mode == 8)
        {
            MyVar.Move = true;
            MyVar.DeltaX = MyVar.XPosition;
            MyVar.DeltaY = MyVar.YPosition;
        }
    }

    private void OnClickColor(object sender, RoutedEventArgs e)
    {
        if (Primitives.ListPrimitive.Count == 0) return;

        foreach (var p in Primitives.ListPrimitive.Where(p => p.Selection))
        {
            if (Equals(sender, Rеd))
            {
                p.Color = Color4.Red;
            }

            if (Equals(sender, Blue))
            {
                p.Color = Color4.Blue;
            }

            if (Equals(sender, White))
            {
                p.Color = Color4.White;
            }

            if (Equals(sender, Black))
            {
                p.Color = Color4.Black;
            }

            if (Equals(sender, Green))
            {
                p.Color = Color4.Green;
            }

            if (Equals(sender, Wheat))
            {
                p.Color = Color4.Wheat;
            }
        }
    }

    private void OnClickDel(object sender, RoutedEventArgs e)
    {
        if (Primitives.ListPrimitive.Count != 0 && Equals(sender, Ctrl_Z))
        {
            Primitives.ListPrimitive.Remove(Primitives.ListPrimitive.Last());
        }

        if (Equals(sender, Dot) || Equals(sender, Line) || Equals(sender, Triangle) || Equals(sender, LineStrip))
        {
            var type = 0;
            if (Equals(sender, Dot))
            {
                type = 1;
            }

            if (Equals(sender, Line))
            {
                type = 2;
            }

            if (Equals(sender, Triangle))
            {
                type = 3;
            }

            if (Equals(sender, LineStrip))
            {
                type = 4;
            }

            for (var i = Primitives.ListPrimitive.Count - 1; i >= 0; i--)
            {
                if (Primitives.ListPrimitive[i].Type == type)
                {
                    Primitives.ListPrimitive.RemoveAt(i);
                }
            }
        }

        if (Equals(sender, Del))
        {
            for (var i = Primitives.ListPrimitive.Count - 1; i >= 0; i--)
            {
                if (Primitives.ListPrimitive[i].Selection)
                {
                    Primitives.ListPrimitive.RemoveAt(i);
                }
            }
        }
    }
}