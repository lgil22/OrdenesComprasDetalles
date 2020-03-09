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
using OrdenesCompras.Entidades;
using OrdenesCompras.BLL;

namespace OrdenesCompras.UI.Registros
{
    /// <summary>
    /// Interaction logic for rOrdenes.xaml
    /// </summary>
    public partial class rOrdenes : Window
    {
        public List<OrdenDetalles> Detalles   { get; set; }
        public rOrdenes()
        {
            InitializeComponent();
            this.Detalles = new List<OrdenDetalles>();
            CargarGrid();
        }

        private void Limpiar()
        {
            OrdenIdTextBox.Text = "0";
            ClienteIdTextBox.Text = "0";
            FechaDatePicker.SelectedDate = DateTime.Now;
            DescripcionTextBox.Text = string.Empty;
            TotalTextBox.Text = "0";

            this.Detalles = new List<OrdenDetalles>();
            CargarGrid();

        }

        private Orden LlenaClase()
        {
            Orden orden = new Orden();
            orden.OrdenId = Convert.ToInt32(OrdenIdTextBox.Text);
            orden.ClienteId = Convert.ToInt32(ClienteIdTextBox.Text);
            orden.OrdenFecha = (DateTime)FechaDatePicker.SelectedDate;
            //orden.Descripcion
            orden.Total = decimal.Parse(TotalTextBox.Text);


           // orden.Detalles = this.Detalles;

            return orden;
        }
        private void LlenaCampo(Orden orden) 
        {
            OrdenIdTextBox.Text = Convert.ToString(orden.OrdenId);
            FechaDatePicker.SelectedDate = orden.OrdenFecha;
            ClienteIdTextBox.Text = Convert.ToString(orden.ClienteId);
           // DescripcionTextBox.Text = orden.Descripcion;
            TotalTextBox.Text = Convert.ToString(orden.Total);

            this.Detalles = orden.Detalles;
            CargarGrid();
        }

        private bool Validar()
        {
            bool paso = true;

            if (string.IsNullOrEmpty(FechaDatePicker.Text))
            {
                MessageBox.Show("EL campo Fecha orden no puede estar vacio", "Aviso", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                FechaDatePicker.Focus();
                paso = false;
            }
            if (string.IsNullOrEmpty(TotalTextBox.Text))
            {
                MessageBox.Show("EL campo total no puede estar vacio", "Aviso", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                TotalTextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrEmpty(DescripcionTextBox.Text))
            {
                MessageBox.Show("EL campo almacen no puede estar vacio", "Aviso", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                DescripcionTextBox.Focus();
                paso = false;
            }
            if (this.Detalles.Count == 0)
            {
                MessageBox.Show("Debe agregar un Telefono", "Aviso", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                ProductoIdTexBox.Focus();
                CantidadTextBox.Focus();
                paso = false;
            }
            return paso;
        }


        //Metodo para cargar el Grid
        private void CargarGrid() 
        {
            DetalleDataGrid.ItemsSource = null;
            DetalleDataGrid.ItemsSource = this.Detalles;
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.ItemsSource != null)
            {
                this.Detalles = (List<OrdenDetalles>)DetalleDataGrid.ItemsSource;
            }

            //Agregar un nuevo detalle con los datos introducidos
            this.Detalles.Add(new OrdenDetalles
            {
                OrdenId = OrdenIdTextBox.Text.ToInt(),
                ProductoId = Convert.ToInt32(ProductoIdTexBox.Text),
                Cantidad = Convert.ToInt32(CantidadTextBox.Text),
                

            });
            CargarGrid();
            ProductoIdTexBox.Focus();
            ProductoIdTexBox.Clear();
            CantidadTextBox.Clear();
            int valor = Convert.ToInt32(CantidadTextBox.Text.ToInt());
            int id = Convert.ToInt32(ProductoIdTexBox.Text.ToInt());
            ProductosBLL.CalcInventario(id, valor);
            ProductoIdTexBox.Text = string.Empty;
            CantidadTextBox.Text = string.Empty;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Orden ordenes = OrdenBLL.Buscar((int)OrdenIdTextBox.Text.ToInt());
            return (ordenes != null);
        }

        private void EliminarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.Items.Count > 0 && DetalleDataGrid.SelectedItem != null)
            {
                //remover la fila
                Detalles.RemoveAt(DetalleDataGrid.SelectedIndex);
                CargarGrid();
            }
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            Orden ordenes;
            bool paso = false;

            if (!Validar())
                return;

            ordenes = LlenaClase();
            Limpiar();

            //Determinar si es guardar o modificar

            if (string.IsNullOrWhiteSpace(OrdenIdTextBox.Text) || OrdenIdTextBox.Text == "0")
                paso = OrdenBLL.Guardar(ordenes);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar una persona que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                paso = OrdenBLL.Modificar(ordenes);
            }

            //Informar el resultado
            if (paso)
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int.TryParse(OrdenIdTextBox.Text, out id);

            Limpiar();

            if (OrdenBLL.Eliminar(id))
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show(OrdenIdTextBox.Text, "No se puede eliminar una orden que no existe");
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            int id;
            Orden ordes = new Orden();
            int.TryParse(OrdenIdTextBox.Text, out id);

            Limpiar();

            ordes = OrdenBLL.Buscar(id);

            if (ordes != null)
            {
                MessageBox.Show("Persona Encontrada");
                LlenaCampo(ordes);
            }

            else
            {
                MessageBox.Show("Persona no Encontrada");
            }
        }
    }
}
