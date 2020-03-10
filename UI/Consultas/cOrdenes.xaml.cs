using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OrdenesCompras.BLL;
using OrdenesCompras.Entidades;
using OrdenesCompras.UI;


namespace OrdenesCompras.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cOrdenes.xaml
    /// </summary>
    public partial class cOrdenes : Window
    {
        public cOrdenes()
        {
            InitializeComponent();
        }

        private void BucarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Orden>();
            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = OrdenBLL.GetList(x => true);
                        break;
                    case 1:
                        int id;
                        id = int.Parse(CriterioTextBox.Text);
                        listado = OrdenBLL.GetList(x => x.OrdenId == id);
                        break;

                    case 2:
                        int clienteid;
                        clienteid = int.Parse(CriterioTextBox.Text);
                        listado = OrdenBLL.GetList(x => x.ClienteId == clienteid);
                        break;
                }
            }
            else
            {
                listado = OrdenBLL.GetList(p => true);
            }
            ConsultarDataGrid.ItemsSource = null;
            ConsultarDataGrid.ItemsSource = listado;
        }
    }
}

  