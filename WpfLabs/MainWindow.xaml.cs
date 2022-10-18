using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using OpenTK.Mathematics;
using OpenTK.Wpf;
using WpfLabs;
using WpfLabs.Model;

namespace Wpf_lab1
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow
    {
        public static Var myVar = new Var();
        public static Primitives primitives = new Primitives();
        public List<MyPoint> temp = new List<MyPoint>();


        public MainWindow()
        {
            InitializeComponent();

            var mainSettings = new GLWpfControlSettings { MajorVersion = 2, MinorVersion = 1 };
            OpenTkControl.Start(mainSettings);
        }

        private void OpenTkControl_OnRender(TimeSpan delta)
        {
            ExampleScene.Render();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D0)
            {
                if (myVar.mode != 4 && primitives.listPrimitive.Count != 0)
                {
                    primitives.listPrimitive[^1].selection = false;
                }

                if (myVar.mode == 4 && primitives.listPrimitive[^1].coordinates.Count != 0)
                {
                    primitives.listPrimitive[^1].selection = false;
                    primitives.listPrimitive.Add(new Primitive(myVar.mode, new List<MyPoint>(), myVar.StandartColor4,
                        true));
                }

                myVar.mode = 0;
            }

            if (e.Key == Key.D1)
            {
                myVar.mode = 1;
            }

            if (e.Key == Key.D2)
            {
                myVar.mode = 2;
            }

            if (e.Key == Key.D3)
            {
                myVar.mode = 3;
            }

            if (e.Key == Key.D4)
            {
                myVar.mode = 4;
            }

            if (e.Key == Key.D9)
            {
                myVar.mode = 9;
            }

            if (e.Key == Key.D8)
            {
                myVar.mode = 8;
            }

            if (e.Key == Key.Space)
            {
                if (myVar.mode == 4 && primitives.listPrimitive[^1].coordinates.Count != 0)
                {
                    primitives.listPrimitive.Add(new Primitive(myVar.mode, new List<MyPoint>(), myVar.StandartColor4,
                        true));
                }
            }

            if (e.Key == Key.Delete)
            {
                Console.WriteLine("Delete");
                for (int i = primitives.listPrimitive.Count - 1; i >= 0; i--)
                {
                    if (primitives.listPrimitive[i].selection)
                    {
                        primitives.listPrimitive.RemoveAt(i);
                    }
                }
            }

            if (e.Key == Key.Z || Equals(sender, Ctrl_Z))
            {
                Console.WriteLine("Click Z");
                if (primitives.listPrimitive.Count != 0)
                {
                    primitives.listPrimitive.Remove(primitives.listPrimitive.Last());
                }
            }

            if (e.Key == Key.Back)
            {
                Console.WriteLine("Backspace");

                for (int i = primitives.listPrimitive.Count - 1; i >= 0; i--)
                {
                    if (primitives.listPrimitive[i].type == myVar.mode)
                    {
                        primitives.listPrimitive.RemoveAt(i);
                    }
                }
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            myVar.Height = (float)MyGrid.ActualHeight;
            myVar.Width = (float)MyGrid.ActualWidth;
            if (e.LeftButton == MouseButtonState.Pressed && myVar.mode == 0)
            {
                var lUp = new Vector2(myVar.XPosition - 10, myVar.YPosition + 10);

                var rDown = new Vector2(myVar.XPosition + 10, myVar.YPosition - 10);
                if (primitives.listPrimitive.Count != 0)
                {
                    foreach (var p in primitives.listPrimitive)
                    {
                        foreach (var coord in p.coordinates)
                        {
                            if (coord.coordinates.X > lUp.X && coord.coordinates.Y < lUp.Y)
                            {
                                if (coord.coordinates.X < rDown.X && coord.coordinates.Y > rDown.Y)
                                {
                                    if (p.selection)
                                        p.selection = false;
                                    else
                                        p.selection = true;
                                }
                            }
                        }
                    }
                }
            }

            if (e.LeftButton == MouseButtonState.Pressed && myVar.mode == 9)
            {
                myVar.move = true;
                var lUp = new Vector2(myVar.XPosition - 5, myVar.YPosition + 5);
                var rDown = new Vector2(myVar.XPosition + 5, myVar.YPosition - 5);
                if (primitives.listPrimitive.Count != 0)
                {
                    foreach (var p in primitives.listPrimitive)
                    {
                        if (p.selection)
                        {
                            foreach (var coord in p.coordinates)
                            {
                                if (coord.coordinates.X > lUp.X && coord.coordinates.Y < lUp.Y)
                                {
                                    if (coord.coordinates.X < rDown.X && coord.coordinates.Y > rDown.Y)
                                    {
                                        if (coord.selection)
                                            coord.selection = false;
                                        else
                                            coord.selection = true;
                                    }
                                    else
                                        coord.selection = false;
                                }
                                else
                                    coord.selection = false;
                            }
                        }
                    }
                }
            }

            if (e.LeftButton == MouseButtonState.Pressed && myVar.mode == 8)
            {
                myVar.move = true;
                myVar.press = true;
                myVar.deltaX = myVar.XPosition;
                myVar.deltaY = myVar.YPosition;
            }

            if (e.LeftButton == MouseButtonState.Pressed && myVar.mode == 1)
            {
                if (primitives.listPrimitive.Count != 0)
                {
                    primitives.listPrimitive[^1].selection = false;
                }

                var dot = new Vector2(myVar.XPosition, myVar.YPosition);
                temp.Add(new MyPoint(dot, false));
                List<MyPoint> point = new List<MyPoint>(temp);
                primitives.listPrimitive.Add(new Primitive(myVar.mode, point, myVar.StandartColor4, true));
                temp.Clear();
            }

            if (e.LeftButton == MouseButtonState.Pressed && myVar.mode == 2)
            {
                if (primitives.listPrimitive.Count != 0)
                {
                    primitives.listPrimitive[^1].selection = false;
                }

                var dot = new Vector2(myVar.XPosition, myVar.YPosition);
                temp.Add(new MyPoint(dot, false));
                primitives.listPrimitive.Add(new Primitive(1, temp, myVar.StandartColor4, true));

                if (temp.Count == 2)
                {
                    List<MyPoint> point = new List<MyPoint>(temp);
                    primitives.listPrimitive.Add(new Primitive(myVar.mode, point, myVar.StandartColor4, true));
                    primitives.listPrimitive.RemoveAt(primitives.listPrimitive.Count - 2);
                    primitives.listPrimitive.RemoveAt(primitives.listPrimitive.Count - 2);
                    temp.Clear();
                }
            }

            if (e.LeftButton == MouseButtonState.Pressed && myVar.mode == 3)
            {
                if (primitives.listPrimitive.Count != 0)
                {
                    primitives.listPrimitive[^1].selection = false;
                }

                var dot = new Vector2(myVar.XPosition, myVar.YPosition);
                temp.Add(new MyPoint(dot, false));
                primitives.listPrimitive.Add(new Primitive(1, temp, myVar.StandartColor4, true));
                if (temp.Count == 2)
                {
                    primitives.listPrimitive.Add(new Primitive(2, temp, myVar.StandartColor4, true));
                }

                if (temp.Count == 3)
                {
                    List<MyPoint> point = new List<MyPoint>(temp);
                    primitives.listPrimitive.Add(new Primitive(myVar.mode, point, myVar.StandartColor4, true));
                    primitives.listPrimitive.RemoveAt(primitives.listPrimitive.Count - 2);
                    primitives.listPrimitive.RemoveAt(primitives.listPrimitive.Count - 2);
                    primitives.listPrimitive.RemoveAt(primitives.listPrimitive.Count - 2);
                    primitives.listPrimitive.RemoveAt(primitives.listPrimitive.Count - 2);
                    temp.Clear();
                }
            }

            if (e.LeftButton == MouseButtonState.Pressed && myVar.mode == 4)
            {
                if (primitives.listPrimitive.Count != 1)
                {
                    primitives.listPrimitive[^2].selection = false;
                }

                if (primitives.listPrimitive[^1].type == 4)
                {
                    Vector2 coord = new Vector2(myVar.XPosition, myVar.YPosition);
                    MyPoint point = new MyPoint(coord, false);
                    primitives.listPrimitive[^1].Primitive_add(point);
                }
                else
                {
                    primitives.listPrimitive.Add(new Primitive(myVar.mode, new List<MyPoint>(), myVar.StandartColor4,
                        true));
                    if (primitives.listPrimitive.Count != 0)
                    {
                        primitives.listPrimitive[^2].selection = false;
                    }

                    Vector2 coord = new Vector2(myVar.XPosition, myVar.YPosition);
                    MyPoint point = new MyPoint(coord, false);
                    primitives.listPrimitive[^1].Primitive_add(point);
                }
            }
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released && myVar.mode == 9)
            {
                myVar.move = false;
                if (primitives.listPrimitive.Count != 0)
                {
                    foreach (var p in primitives.listPrimitive)
                    {
                        if (p.selection)
                        {
                            foreach (var coord in p.coordinates)
                            {
                                if (coord.selection)
                                {
                                    coord.coordinates.X = myVar.XPosition;
                                    coord.coordinates.Y = myVar.YPosition;
                                    coord.selection = false;
                                }
                            }
                        }
                    }
                }
            }

            if (e.LeftButton == MouseButtonState.Released && myVar.mode == 8)
            {
                myVar.move = false;
                myVar.press = false;
                if (primitives.listPrimitive.Count != 0)
                {
                    foreach (var p in primitives.listPrimitive)
                    {
                        if (p.selection)
                        {
                            foreach (var coord in p.coordinates)
                            {
                                coord.coordinates.X -= myVar.deltaX - myVar.XPosition;
                                coord.coordinates.Y -= myVar.deltaY - myVar.YPosition;
                                coord.selection = false;
                            }
                        }
                    }
                }
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            OnMouseDown();

            myVar.XPosition = (float)(Mouse.GetPosition(MyGrid).X - (int)(MyGrid.ActualWidth) / 2);
            myVar.YPosition = (float)(-Mouse.GetPosition(MyGrid).Y + (int)(MyGrid.ActualHeight) / 2);

            if (myVar.mode == 9 && myVar.move == true)
            {
                if (primitives.listPrimitive.Count != 0)
                {
                    foreach (var p in primitives.listPrimitive)
                    {
                        if (p.selection)
                        {
                            foreach (var coord in p.coordinates)
                            {
                                if (coord.selection)
                                {
                                    coord.coordinates.X = myVar.XPosition;
                                    coord.coordinates.Y = myVar.YPosition;
                                }
                            }
                        }
                    }
                }
            }

            if (myVar.mode == 8 && myVar.move == true)
            {
                if (primitives.listPrimitive.Count != 0)
                {
                    foreach (var p in primitives.listPrimitive)
                    {
                        if (p.selection)
                        {
                            foreach (var coord in p.coordinates)
                            {
                                if (Math.Abs(myVar.deltaX - myVar.XPosition) > 0.00001f ||
                                    Math.Abs(myVar.deltaY - myVar.YPosition) > 0.00001f)
                                {
                                    coord.coordinates.X -= myVar.deltaX - myVar.XPosition;
                                    coord.coordinates.Y -= myVar.deltaY - myVar.YPosition;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void OnMouseDown()
        {
            if (myVar.press && myVar.mode == 8)
            {
                myVar.move = true;
                myVar.deltaX = myVar.XPosition;
                myVar.deltaY = myVar.YPosition;
            }
        }

        private void OnClickColor(object sender, RoutedEventArgs e)
        {
            if (primitives.listPrimitive != null && primitives.listPrimitive.Count != 0)
            {
                foreach (var p in primitives.listPrimitive)
                {
                    if (p.selection)
                    {
                        if (Equals(sender, Rеd))
                        {
                            p.color = Color4.Red;
                        }

                        if (Equals(sender, Blue))
                        {
                            p.color = Color4.Blue;
                        }

                        if (Equals(sender, White))
                        {
                            p.color = Color4.White;
                        }

                        if (Equals(sender, Black))
                        {
                            p.color = Color4.Black;
                        }

                        if (Equals(sender, Green))
                        {
                            p.color = Color4.Green;
                        }

                        if (Equals(sender, Wheat))
                        {
                            p.color = Color4.Wheat;
                        }
                    }
                }
            }
        }

        private void OnClickDel(object sender, RoutedEventArgs e)
        {
            if (primitives.listPrimitive.Count != 0 && Equals(sender, Ctrl_Z))
            {
                primitives.listPrimitive.Remove(primitives.listPrimitive.Last());
            }

            if (Equals(sender, Dot) || Equals(sender, Line) || Equals(sender, Trio) || Equals(sender, Loman))
            {
                var type = 0;
                if (sender == Dot)
                    type = 1;
                if (sender == Line)
                    type = 2;
                if (sender == Trio)
                    type = 3;
                if (sender == Loman)
                    type = 4;
                for (int i = primitives.listPrimitive.Count - 1; i >= 0; i--)
                {
                    if (primitives.listPrimitive[i].type == type)
                    {
                        primitives.listPrimitive.RemoveAt(i);
                    }
                }
            }

            if (Equals(sender, Del))
            {
                for (int i = primitives.listPrimitive.Count - 1; i >= 0; i--)
                {
                    if (primitives.listPrimitive[i].selection)
                    {
                        primitives.listPrimitive.RemoveAt(i);
                    }
                }
            }
        }
    }
}