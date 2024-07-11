using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chess_Queen
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

        private void runAlgorythim_Click(object sender, RoutedEventArgs e)
        {
            StaticFunctions.size = int.Parse(setSixe.Text);
            StaticFunctions.runboard();
            foreach (EndScenerio end in StaticFunctions.endpointlist)
            {
                export.Text += $"{end.amount.ToString()} ";
                exportb.Text += $"{StaticFunctions.dispBoard(end)}";
            }


        }
    }
}