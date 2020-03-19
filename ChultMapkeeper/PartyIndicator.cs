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
        [NonSerialized]
        private List<DependencyObject> hitResultsList = new List<DependencyObject>();

        private static BitmapImage warrior = new BitmapImage(new Uri($"pack://application:,,,/Pictures/warrior.png"));

        [NonSerialized]
        public Rectangle renderable = new Rectangle() { Width = 55, Height = 55, Fill = new ImageBrush() { ImageSource = warrior }, HorizontalAlignment = HorizontalAlignment.Stretch };

        public double top, left;

        public PartyIndicator()
        {
            Static.WindowStates.WindowStateChanged += OnWindowStateChanged;
            top = 1473.5;
            left = 2473;
            Canvas.SetTop(renderable, top);
            Canvas.SetLeft(renderable, left);
        }

        [OnDeserialized()]
        public void onDeserialized(StreamingContext context)
        {
            hitResultsList = new List<DependencyObject>();
            Static.WindowStates.WindowStateChanged += OnWindowStateChanged;
            renderable = new Rectangle() { Width = 55, Height = 55, Fill = new ImageBrush() { ImageSource = warrior }, HorizontalAlignment = HorizontalAlignment.Stretch };
            Canvas.SetTop(renderable, top);
            Canvas.SetLeft(renderable, left);
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

            VisualTreeHelper.HitTest(((Rectangle)sender).Parent as HexCanvas, null, new HitTestResultCallback(MyHitTestResult), new PointHitTestParameters(newPoint));

            Path newHexagon = hitResultsList[0] as Path;

            if (newHexagon != null)
            {
                Vector newPos = VisualTreeHelper.GetOffset(newHexagon);

                ((ViewModel.MainWindowVM)((HexCanvas)((Rectangle)sender).Parent).DataContext).PartyIndicator.left = newPos.X;
                ((ViewModel.MainWindowVM)((HexCanvas)((Rectangle)sender).Parent).DataContext).PartyIndicator.top = newPos.Y;

                Canvas.SetLeft(draggedRectangle, newPos.X);
                Canvas.SetTop(draggedRectangle, newPos.Y);

                drag = false;
            }
        }

        public HitTestResultBehavior MyHitTestResult(HitTestResult result)
        {
            hitResultsList.Clear();
            // Add the hit test result to the list that will be processed after the enumeration.
            hitResultsList.Add(result.VisualHit);

            // Set the behavior to return visuals at all z-order levels.
            return HitTestResultBehavior.Continue;
        }

        public void OnWindowStateChanged(object sender, EventArgs e)
        {
            InteractMode state = (InteractMode)sender;

            switch (state)
            {
                case InteractMode.MoveParty:
                    renderable.MouseDown += rectangle_MouseDown;
                    renderable.MouseMove += rectangle_MouseMove;
                    renderable.MouseUp += rectangle_MouseUp;
                    break;
                case InteractMode.RevealMode:
                case InteractMode.MoveMapMode:
                    renderable.MouseDown -= rectangle_MouseDown;
                    renderable.MouseMove -= rectangle_MouseMove;
                    renderable.MouseUp -= rectangle_MouseUp;
                    break;
            }
        }
    }
}
