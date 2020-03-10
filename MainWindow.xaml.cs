using OrdenesCompras.UI.Registros;
using System.Windows;
using OrdenesCompras.UI.Consultas;

namespace OrdenesCompras
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

        private void MenuRegistros_Click(object sender, RoutedEventArgs e)
        {
            rOrdenes ords = new rOrdenes();
            ords.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            cOrdenes ord = new cOrdenes();
            ord.Show();
        }
    }
}
