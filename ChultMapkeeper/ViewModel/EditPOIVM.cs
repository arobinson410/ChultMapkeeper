using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChultMapkeeper.ViewModel
{
    public class EditPOIVM : INotifyPropertyChanged
    {
        private MapState mapState;
        private KeyValuePair<int, PointOfInterest> selectedPOI;

        public event PropertyChangedEventHandler PropertyChanged;

        public EditPOIVM(MapState m)
        {
            mapState = m;
        }

        public Dictionary<int, PointOfInterest> PointsOfInterest
        {
            set
            {
                mapState.PointsOfInterest = value;
            }
            get
            {
                return mapState.PointsOfInterest;
            }
        }

        public KeyValuePair<int, PointOfInterest> SelectedPOI
        {
            set
            {
                selectedPOI = value;
                OnPropertyChanged("SelectedPOI");
            }
            get
            {
                return selectedPOI;
            }
        }

        public void OnPropertyChanged(string s)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(s));
        }

    }
}
