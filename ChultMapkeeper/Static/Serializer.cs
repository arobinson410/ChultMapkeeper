#define SERIALIZE

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
            MapState toReturn;

            try
            {
                FileStream stream = new FileStream("saved", FileMode.Open, FileAccess.Read);
                BinaryFormatter formatter = new BinaryFormatter();
                toReturn = formatter.Deserialize(stream) as MapState;
                stream.Close();
                return toReturn;
            }
            catch (FileNotFoundException)
            {
                toReturn = new MapState();
                toReturn.CreateDefaultHexLayout();
                return toReturn;
            }
            catch(Exception e)
            {
                MessageBox.Show("Error Loading Saved Map. Your save file may be corrupted or out of date", "Error Loading", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                return null;
            }
        }
        
        public static void SaveMap(MapState map)
        {
            #if SERIALIZE
            FileStream stream = File.Create("saved");
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, map);
            stream.Close();
            #endif
        }
    }
}
