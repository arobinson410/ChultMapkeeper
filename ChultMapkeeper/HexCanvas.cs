using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ChultMapkeeper
{
    public class HexCanvas:Canvas
    {
        private bool firstLoad = true;
  
        public List<Hexagon> HexagonList
        {
            get { return (List<Hexagon>)GetValue(HexagonListProperty); }
            set { SetValue(HexagonListProperty, value);}
        }

        // Using a DependencyProperty as the backing store for HexagonList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HexagonListProperty =
            DependencyProperty.Register("HexagonList", typeof(List<Hexagon>), typeof(HexCanvas));
        
        public void updateHexagons()
        {
            base.Children.Clear();

            foreach (Hexagon h in HexagonList)
            {
                base.Children.Add(h.Path);
            }

            base.Children.Add(((ViewModel.MainWindowVM)base.DataContext).PartyIndicator.r);
        }
        
        protected override void OnRender(DrawingContext dc)
        {
            if (firstLoad) {
            
                foreach (Hexagon h in HexagonList)
                {
                    base.Children.Add(h.Path);
                }

                foreach(KeyValuePair<int, PointOfInterest> p in ((ViewModel.MainWindowVM)base.DataContext).MapState.PointsOfInterest)
                {
                    foreach(FrameworkElement f in p.Value.ElementList)
                        base.Children.Add(f);
                }

                base.Children.Add(((ViewModel.MainWindowVM)base.DataContext).PartyIndicator.r);

                firstLoad = false;
            }

        }  
        
    }
    
}
