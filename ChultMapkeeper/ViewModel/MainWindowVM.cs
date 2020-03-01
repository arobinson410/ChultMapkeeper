using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChultMapkeeper.ViewModel
{
    [Serializable]
    public class MainWindowVM
    {
        private List<Hexagon> hexagons;
        private PartyIndicator partyIndicator;

        public MainWindowVM()
        {
            hexagons = new List<Hexagon>();
            partyIndicator = new PartyIndicator();
        }

        public List<Hexagon> HexagonList
        {
            set
            {
                hexagons = value;
            }
            get
            {
                return hexagons;
            }
        }

        public PartyIndicator PartyIndicator
        {
            set
            {
                partyIndicator = value;
            }
            get
            {
                return partyIndicator;
            }
        }
    }
}
