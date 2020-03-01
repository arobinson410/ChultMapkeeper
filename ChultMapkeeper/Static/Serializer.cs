using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace ChultMapkeeper.Static
{
    public static class Serializer
    {
        public static ViewModel.MainWindowVM LoadMap()
        {
            ViewModel.MainWindowVM toReturn = new ViewModel.MainWindowVM();

            try
            {
                FileStream stream = File.OpenRead("saved");
                var formatter = new BinaryFormatter();
                toReturn = formatter.Deserialize(stream) as ViewModel.MainWindowVM;
                stream.Close();
            }
            catch (FileNotFoundException)
            {
                List<Hexagon> l = new List<Hexagon>();
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ChultMapkeeper.Default.saved");
                var formatter = new BinaryFormatter();
                l = formatter.Deserialize(stream) as List<Hexagon>;
                stream.Close();

                toReturn.HexagonList = l;
            }

            return toReturn;
        }

        public static void SaveMap(ViewModel.MainWindowVM map)
        {
            FileStream stream = File.Create("saved");
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, map);
        }

    }
}
