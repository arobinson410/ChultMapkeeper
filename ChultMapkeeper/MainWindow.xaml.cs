using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChultMapkeeper
{
    public partial class MainWindow : Window
    {
        public ViewModel.MainWindowVM mainWindowVM;

        public MainWindow()
        {
            mainWindowVM = new ViewModel.MainWindowVM(Static.Serializer.LoadMap());
            this.DataContext = mainWindowVM;

            InitializeComponent();

            this.KeyDown += MainWindow_KeyDown;
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Static.Serializer.SaveMap(mainWindowVM.MapState);
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.M)
            {
                Static.WindowStates.MapMode = InteractMode.MoveMapMode;
            }
            else if(e.Key == Key.P)
            {
                Static.WindowStates.MapMode = InteractMode.MoveParty;
            }
            else if(e.Key == Key.R)
            {
                Static.WindowStates.MapMode = InteractMode.RevealMode;
            }
        }
    }
}
