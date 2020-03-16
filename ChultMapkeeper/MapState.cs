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
        private Dictionary<int, PointOfInterest> pointsOfInterest;

        public MapState()
        {
            hexagons = new List<Hexagon>();
            partyIndicator = new PartyIndicator();
            pointsOfInterest = new Dictionary<int, PointOfInterest>(Static.DefaultMapState.PointsOfInterest);
        }

        public void CreateDefaultHexLayout()
        {
            for (int i = 0; i < 36; i++)
            {
                for (int j = 0; j < 85; j++)
                {
                    hexagons.Add(new Hexagon(216 + (115 * i), 162 + (66.75 * j)) { HexNumber = (int)(2 * (i + 1) * 100 + j + 1), Hidden = true });
                }
            }

            for (int i = 0; i < 36; i++)
            {
                for (int j = 0; j < 85; j++)
                {
                    hexagons.Add(new Hexagon(158.5 + (115 * i), 195.375 + (66.75 * j)) { HexNumber = (int)(200 * i + j + 101), Hidden = true });
                }
            }

            foreach (int n in Static.DefaultMapState.defaultHexes)
            {
                foreach(Hexagon h in hexagons)
                {
                    if(h.HexNumber == n)
                    {
                        h.Hidden = false;
                    }
                }
            }
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

        public Dictionary<int, PointOfInterest> PointsOfInterest
        {
            set
            {
                pointsOfInterest = value;
            }
            get
            {
                return pointsOfInterest;
            }
        }

    }
}
