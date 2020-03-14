using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChultMapkeeper
{
    [Serializable]
    public class MapState
    {
        private List<Hexagon> hexagons;
        private PartyIndicator partyIndicator;

        public MapState()
        {
            hexagons = new List<Hexagon>();
            partyIndicator = new PartyIndicator();
        }

        public void CreateDefaultHexLayout()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ChultMapkeeper.Default.saved");
            var formatter = new BinaryFormatter();
            hexagons = formatter.Deserialize(stream) as List<Hexagon>;
            stream.Close();
        }

        public List<Hexagon> Hexagons
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
