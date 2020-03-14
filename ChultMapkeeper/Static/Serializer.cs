using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace ChultMapkeeper.Static
{
    public static class Serializer
    {
        public static MapState LoadMap()
        {
            MapState toReturn = new MapState();

            try
            {
                FileStream stream = File.OpenRead("saved");
                var formatter = new BinaryFormatter();
                toReturn = formatter.Deserialize(stream) as MapState;
                stream.Close();
            }
            catch (FileNotFoundException)
            {
                toReturn.CreateDefaultHexLayout();
            }
            catch(Exception e)
            {
                MessageBox.Show("Error Loading Saved Map. Your save file may be corrupted or out of date", "Error Loading", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

            return toReturn;
        }

        public static void SaveMap(MapState map)
        {
            FileStream stream = File.Create("saved");
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, map);
        }

    }
}
