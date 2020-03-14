using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChultMapkeeper
{
    [Serializable]
    public class PartyIndicator : Interfaces.IMonitorWindowState
    {
        private bool drag = false;
        private Point startPoint;

        private static BitmapImage warrior = new BitmapImage(new Uri($"pack://application:,,,/Pictures/warrior.png"));

        [NonSerialized]
        public Rectangle r = new Rectangle() { Width = 55, Height = 55, Fill = new ImageBrush() { ImageSource = warrior }, HorizontalAlignment = HorizontalAlignment.Stretch };

        public double top, left;

        public PartyIndicator()
        {
            Static.WindowStates.WindowStateChanged += OnWindowStateChanged;
            top = 1473.5;
            left = 2473;
            Canvas.SetTop(r, top);
            Canvas.SetLeft(r, left);
        }

        [OnDeserialized()]
        public void onDeserialized(StreamingContext context)
        {
            Static.WindowStates.WindowStateChanged += OnWindowStateChanged;
            r = new Rectangle() { Width = 55, Height = 55, Fill = new ImageBrush() { ImageSource = warrior }, HorizontalAlignment = HorizontalAlignment.Stretch };
            Canvas.SetTop(r, top);
            Canvas.SetLeft(r, left);
        }


        private void rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // start dragging
            drag = true;
            // save start point of dragging
            startPoint = Mouse.GetPosition(((Rectangle)sender).Parent as HexCanvas);
        }

        private void rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            // if dragging, then adjust rectangle position based on mouse movement
            if (drag)
            {
                Rectangle draggedRectangle = sender as Rectangle;
                Point newPoint = Mouse.GetPosition(((Rectangle)sender).Parent as HexCanvas);
                double left = Canvas.GetLeft(draggedRectangle);
                double top = Canvas.GetTop(draggedRectangle);
                Canvas.SetLeft(draggedRectangle, left + (newPoint.X - startPoint.X));
                Canvas.SetTop(draggedRectangle, top + (newPoint.Y - startPoint.Y));

                startPoint = newPoint;
            }
        }

        public void rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle draggedRectangle = sender as Rectangle;
            Point newPoint = Mouse.GetPosition(((Rectangle)sender).Parent as HexCanvas);

            double left = Canvas.GetLeft(draggedRectangle);
            double top = Canvas.GetTop(draggedRectangle);

            double[] nearestPos = getNearestHexPos(left, top);

            ((ViewModel.MainWindowVM)((HexCanvas)((Rectangle)sender).Parent).DataContext).PartyIndicator.left = nearestPos[0];
            ((ViewModel.MainWindowVM)((HexCanvas)((Rectangle)sender).Parent).DataContext).PartyIndicator.top = nearestPos[1];

            Canvas.SetLeft(draggedRectangle, nearestPos[0]);
            Canvas.SetTop(draggedRectangle, nearestPos[1]);

            drag = false;
        }

        public double[] getNearestHexPos(double left, double top)
        {
            double left1, top1;

            left1 = left - left % 57.5 + 0.5;
            top1 = top - top % 33.375 + 5;

            return new double[2] { left1, top1 };
            //return testNearestHex(left, top);
        }

        public double[] testNearestHex(double left, double top)
        {
            double a = 33;
            double b = 37.5;
            double c = 21.65;

            int row = (int)(top / b);
            int column = (int)(left / (a + c));

            double dy = left - row * b;
            double dx = top - column * (a + c);

            if (((row ^ column) & 1) == 0)
                dy = b - dy;
            int right = dy * (a - c) < b * (dx - c) ? 1 : 0;

            row += (column ^ row ^ right) & 1;
            column += right;

            return new double[] { column * 57.5 , row * 33.375};
        }

        public void OnWindowStateChanged(object sender, EventArgs e)
        {
            InteractMode state = (InteractMode)sender;

            switch (state)
            {
                case InteractMode.MoveParty:
                    r.MouseDown += rectangle_MouseDown;
                    r.MouseMove += rectangle_MouseMove;
                    r.MouseUp += rectangle_MouseUp;
                    break;
                case InteractMode.RevealMode:
                case InteractMode.MoveMapMode:
                    r.MouseDown -= rectangle_MouseDown;
                    r.MouseMove -= rectangle_MouseMove;
                    r.MouseUp -= rectangle_MouseUp;
                    break;
            }
        }

        public static double DistanceTo(double aX, double aY, double bX, double bY)
        {
            return Math.Pow(Math.Pow((aX - bX), 2) + Math.Pow((aY - bY), 2), 0.5);
        }
    }
}
