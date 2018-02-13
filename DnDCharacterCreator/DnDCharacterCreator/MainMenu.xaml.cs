/*
 * Created by David Cagnacci
 * Latest Revision 8/26/2016
 * Contact: david.cagnacci@outlook.com
 */

using System;
using System.Collections.Generic;
using System.Linq;
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

namespace DnDCharacterCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow = new CharacterSheet();
            App.Current.MainWindow.Show();
            this.Close();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow = new CharacterSheet();
            App.Current.MainWindow.Show();
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
