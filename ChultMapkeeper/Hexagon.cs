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
using System.Windows.Shapes;

namespace ChultMapkeeper
{
    //Hexagons are stored on (216 + (115*i), 162 + (66.75*j) or (158.5 + (115*i), 195.375 + (66.75*j)
    [Serializable]
    public class Hexagon
    {
        private static Geometry shape = Geometry.Parse("M8.660254,0 L17.320508,5 17.320508,15 8.660254,20 0,15 0,5 8.660254,0 z");
        private static SolidColorBrush fill = new SolidColorBrush(Color.FromRgb(218, 165, 32));
        private static Thickness thickness = new Thickness(0);
        private static RotateTransform rotate = new RotateTransform(90);

        [NonSerialized]
        public Path Path;

        private bool hidden;
        private double leftPos, topPos;

        public Hexagon(double leftPos, double topPos)
        {
            this.leftPos = leftPos;
            this.topPos = topPos;

            Path = AddHex(leftPos, topPos);
            hidden = false;
        }

        [OnDeserialized()]
        public void onDeserialized(StreamingContext context)
        {
            Path = AddHex(leftPos, topPos);
        }

        public Path AddHex(double leftPos, double topPos)
        {
            Path toReturn = new Path(){Height = 75, Width = 66, Margin = thickness, HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, Stretch = Stretch.Fill, Fill = fill, Data = shape, LayoutTransform = rotate};

            Canvas.SetLeft(toReturn, leftPos);
            Canvas.SetTop(toReturn, topPos);

            toReturn.MouseLeftButtonDown += OnClick;

            return toReturn;
        }

        public void OnClick(object sender, MouseButtonEventArgs e)
        {
            UIElement element = sender as UIElement;

            if (Static.WindowStates.MapMode == InteractMode.RevealMode)
            {
                if (element.Opacity == 1)
                {
                    element.Opacity = 0;
                    hidden = true;
                }
                else
                {
                    element.Opacity = 1;
                    hidden = false;
                }
            }
        }

        public bool Hidden
        {
            set
            {
                hidden = value;
            }
            get
            {
                return hidden;
            }
        }
    }
}
