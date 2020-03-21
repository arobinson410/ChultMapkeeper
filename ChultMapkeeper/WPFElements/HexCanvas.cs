using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public List<Hexagon> HexagonList
        {
            get { return (List<Hexagon>)GetValue(HexagonListProperty); }
            set { SetValue(HexagonListProperty, value);}
        }
        
        // Using a DependencyProperty as the backing store for HexagonList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HexagonListProperty = DependencyProperty.Register(
            "HexagonList",
            typeof(List<Hexagon>),
            typeof(HexCanvas),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(UpdateHexagons)));
        
        private static void UpdateHexagons(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HexCanvas canvas = d as HexCanvas;

            if (e.OldValue != null)
            {
                foreach (Hexagon h in ((List<Hexagon>)e.OldValue))
                    canvas.Children.Remove(h.Path);
            }

            foreach(Hexagon h in ((List<Hexagon>)e.NewValue))
                canvas.Children.Add(h.Path);
        }

        public PartyIndicator PartyIndicator
        {
            get { return (PartyIndicator)GetValue(PartyIndicatorProperty); }
            set { SetValue(PartyIndicatorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HexagonList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PartyIndicatorProperty = DependencyProperty.Register(
            "PartyIndicator",
            typeof(PartyIndicator),
            typeof(HexCanvas),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(UpdatePartyIndicator)));

        private static void UpdatePartyIndicator(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HexCanvas canvas = d as HexCanvas;

            if(e.OldValue != null)
                canvas.Children.Remove(((PartyIndicator)e.OldValue).renderable);

            canvas.Children.Add(((PartyIndicator)e.NewValue).renderable);
        }

        public Dictionary<int, PointOfInterest> PointsOfInterest
        {
            get { return (Dictionary<int, PointOfInterest>)GetValue(PointsOfInterestProperty); }
            set { SetValue(PointsOfInterestProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HexagonList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointsOfInterestProperty = DependencyProperty.Register(
            "PointsOfInterest",
            typeof(Dictionary<int, PointOfInterest>),
            typeof(HexCanvas),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.AffectsRender,
                new PropertyChangedCallback(PointsOfInterestIndicator)));

        private static void PointsOfInterestIndicator(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HexCanvas canvas = d as HexCanvas;

            if (e.OldValue != null)
            {
                foreach(KeyValuePair<int, PointOfInterest> p in (Dictionary<int, PointOfInterest>)e.OldValue){
                    foreach (FrameworkElement f in p.Value.ElementList)
                        canvas.Children.Remove(f);
                }
            }

            foreach (KeyValuePair<int, PointOfInterest> p in (Dictionary<int, PointOfInterest>)e.NewValue)
            {
                foreach (FrameworkElement f in p.Value.ElementList)
                    canvas.Children.Add(f);
            }
        }

        /*
        protected override void OnRender(DrawingContext dc)
        {
            if (firstLoad) {
            
                foreach (Hexagon h in HexagonList)
                {
                    base.Children.Add(h.Path);
                }

                foreach(KeyValuePair<int, PointOfInterest> p in ((ViewModel.MainWindowVM)base.DataContext).MapState.PointsOfInterest)
                {
                    foreach (FrameworkElement f in p.Value.ElementList)
                    {
                        //Temporarily Commented for Session
                        //base.Children.Add(f);
                    }
                }

                base.Children.Add(((ViewModel.MainWindowVM)base.DataContext).PartyIndicator.r);

                firstLoad = false;
            }

        }  */

    }
    
}
