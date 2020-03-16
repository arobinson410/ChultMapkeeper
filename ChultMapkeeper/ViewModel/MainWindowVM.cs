using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChultMapkeeper.ViewModel
{
    [Serializable]
    public class MainWindowVM: Interfaces.IMonitorWindowState, INotifyPropertyChanged
    {
        private MapState mapState;
        private Point currMousePos;

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowVM(MapState m)
        {
            currMousePos = new Point(0, 0);
            Static.WindowStates.WindowStateChanged += OnWindowStateChanged;
            mapState = m;
        }

        public MapState MapState
        {
            get
            {
                return mapState;
            }
        }

        public List<Hexagon> HexagonList
        {
            set
            {
                mapState.Hexagons = value;
            }
            get
            {
                return mapState.Hexagons;
            }
        }

        public PartyIndicator PartyIndicator
        {
            set
            {
                mapState.PartyIndicator = value;
            }
            get
            {
                return mapState.PartyIndicator;
            }
        }

        public InteractMode MapMode
        {
            set
            {
                Static.WindowStates.MapMode = value;
            }
            get
            {
                return Static.WindowStates.MapMode;
            }
        }

        public double MouseX
        {
            set
            {
                currMousePos.X = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MouseX"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HexQ"));
            }
            get
            {
                return currMousePos.X;
            }
        }

        public double MouseY
        {
            set
            {
                currMousePos.Y = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MouseY"));

            }
            get
            {
                return currMousePos.Y;
            }
        }

        public void OnWindowStateChanged(object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MapMode"));
        }
    }
}
