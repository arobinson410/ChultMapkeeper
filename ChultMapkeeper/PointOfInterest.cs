using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ChultMapkeeper
{
    public enum POIType
    {
        NONE,
        City,
        Fort,
        Settlement,
        Ruin,
        Mine,
        POI
    }

    [Serializable]
    public class PointOfInterest
    {
        private static FontFamily textFont = new FontFamily("Elephant");

        private string name = "";
        private int hexNum;
        private bool hidden;
        private double offsetX, offsetY, textOffsetX, textOffsetY;
        private POIType pointType;

        [NonSerialized]
        private List<System.Windows.FrameworkElement> l = new List<System.Windows.FrameworkElement>();

        public PointOfInterest(string name, POIType t, int hexNum, double offsetX = 0, double offsetY = 0, double textOffsetX = 0, double textOffsetY = 0)
        {
            this.name = name;
            this.pointType = t;
            this.hexNum = hexNum;
            this.hidden = true;
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.textOffsetX = textOffsetX;
            this.textOffsetY = textOffsetY;

            CreateElementList();
        }

        private void CreateElementList()
        {
            l.Clear();

            Point pixelLoc = Hexagon.HexNumberToPos(hexNum);

            TextBlock t = new TextBlock() { Text = name, Visibility = Visibility.Collapsed, FontSize = 40, FontFamily=textFont};
            Canvas.SetLeft(t, pixelLoc.X + textOffsetX);
            Canvas.SetTop(t, pixelLoc.Y - textOffsetY);
            l.Add(t);

            switch (pointType)
            {
                case POIType.Fort:
                    Rectangle fort1 = new Rectangle() { Width = 36, Height = 36, Fill = Brushes.Black, Visibility = Visibility.Collapsed };
                    Canvas.SetLeft(fort1, pixelLoc.X + offsetX - 6);
                    Canvas.SetTop(fort1, pixelLoc.Y - offsetY - 6);
                    Rectangle fort2 = new Rectangle() { Width = 24, Height = 24, Fill = Brushes.White, Visibility = Visibility.Collapsed };
                    Canvas.SetLeft(fort2, pixelLoc.X + offsetX);
                    Canvas.SetTop(fort2, pixelLoc.Y - offsetY);
                    l.Add(fort1);
                    l.Add(fort2);
                    break;

                case POIType.Settlement:

                    Ellipse circle = new Ellipse() { Width = 30, Height = 30, Fill=Brushes.Black, Visibility = Visibility.Collapsed };
                    Canvas.SetLeft(circle, pixelLoc.X + offsetX);
                    Canvas.SetTop(circle, pixelLoc.Y - offsetY);
                    l.Add(circle);
                    break;

                case POIType.Ruin:

                    Rectangle ruin = new Rectangle() { Width = 30, Height = 30, Fill = Brushes.Black, Visibility = Visibility.Collapsed };
                    Canvas.SetLeft(ruin, pixelLoc.X + offsetX);
                    Canvas.SetTop(ruin, pixelLoc.Y - offsetY);
                    l.Add(ruin);
                    break;

                case POIType.POI:

                    TextBlock marker = new TextBlock() { Text = "X", Visibility = Visibility.Collapsed, FontSize = 40, FontFamily = textFont };
                    Canvas.SetLeft(marker, pixelLoc.X + offsetX);
                    Canvas.SetTop(marker, pixelLoc.Y - offsetY);
                    l.Add(marker);
                    break;
            }

        }

        [OnDeserialized()]
        public void OnDeserialized(StreamingContext context)
        {
            CreateElementList();
            Hidden = hidden;
        }

        public bool Hidden
        {
            set
            {
                hidden = value;
                foreach(FrameworkElement f in l)
                {
                    f.Visibility = hidden ? Visibility.Collapsed : Visibility.Visible;
                }
            }
            get
            {
                return hidden;
            }
        }

        public List<System.Windows.FrameworkElement> ElementList
        {
            set
            {
                l = value;
            }
            get
            {
                return l;
            }
        }

        public double OffsetX
        {
            set
            {
                offsetX = value;
            }
            get
            {
                return offsetX;
            }
        }
        public double OffsetY
        {
            set
            {
                offsetY = value;
            }
            get
            {
                return offsetY;
            }
        }
    }
}
