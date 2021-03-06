﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            List<Hexagon> l = mainWindowVM.HexagonList;
            l[20].HexNumber = 10000;
            l.Add(new Hexagon(9000, 9000));

            mainWindowVM.HexagonList = l;


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

        private void HexCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = Mouse.GetPosition(sender as HexCanvas);
            mainWindowVM.MouseX = p.X;
            mainWindowVM.MouseY = p.Y;
        }

        private void POIEdit_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.EditPOIVM editPOIVM = new ViewModel.EditPOIVM(mainWindowVM.MapState);
            EditPOI editPOI = new EditPOI();
            editPOI.DataContext = editPOIVM;
            editPOI.Show();
        }
    }

}
