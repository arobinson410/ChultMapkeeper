using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChultMapkeeper.ViewModel
{
    [Serializable]
    public class MainWindowVM: Interfaces.IMonitorWindowState, INotifyPropertyChanged
    {
        private MapState mapState;

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowVM(MapState m)
        {
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

        public void OnWindowStateChanged(object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MapMode"));
        }
    }
}
