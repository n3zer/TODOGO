﻿using System.Windows;
using System.Windows.Controls;


namespace TODOGO
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage(AppViewModel dc)
        {
            InitializeComponent();
            DataContext = dc;
        }

        private void TaskListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("s");
        }
    }
}
