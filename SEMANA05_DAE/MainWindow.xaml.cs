using System.Data;
using System.Data.SqlClient;
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

namespace SEMANA05_DAE
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
        private string connectionString = "Data Source=LAB1504-11\\SQLEXPRESS01; Initial Catalog=NeptunoDB; User Id=Luis; Password=123456";
        public class Cliente
        {
            public string idCliente { get; set; }
            public string NombreCompañia { get; set; }
            public string NombreContacto { get; set; }
            public string CargoContacto { get; set; }
            public string Direccion { get; set; }
            public string Ciudad { get; set; }
            public string Region { get; set; }
            public string CodPostal { get; set; }
            public string Pais { get; set; }
            public string Telefono { get; set; }
            public string Fax { get; set; }
        }

        private void Button_CLIENTE(object sender, RoutedEventArgs e)
        {
            List<Cliente> clientes = new List<Cliente>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("ListarClientes", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string idCliente = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                                string NombreCompañia = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                                string NombreContacto = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                                string CargoContacto = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                                string Direccion = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                                string Ciudad = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                                string Region = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                                string CodPostal = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                                string Pais = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                                string Telefono = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                                string Fax = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);

                                clientes.Add(new Cliente { idCliente = idCliente, NombreCompañia = NombreCompañia, NombreContacto = NombreContacto, 
                                                            CargoContacto = CargoContacto, Direccion = Direccion, Ciudad = Ciudad, Region = Region, 
                                                            CodPostal = CodPostal, Pais = Pais, Telefono = Telefono, Fax = Fax});
                            }
                        }
                    }
                }

                dgvDemo.ItemsSource = clientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al recuperar los clientes: " + ex.Message);
            }
        }

        private void Button_Insertar(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("InsertarCliente", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Asignar valores de los controles de entrada a los parámetros del procedimiento almacenado
                        command.Parameters.AddWithValue("@ID", idCliente.Text);
                        command.Parameters.AddWithValue("@NombreC", NombreCompañia.Text);
                        command.Parameters.AddWithValue("@NombreCo", NombreContacto.Text);
                        command.Parameters.AddWithValue("@CargoCo", CargoContacto.Text);
                        command.Parameters.AddWithValue("@Direccion", Direccion.Text);
                        command.Parameters.AddWithValue("@Ciudad", Ciudad.Text);
                        command.Parameters.AddWithValue("@Region", Region.Text);
                        command.Parameters.AddWithValue("@CodPostal", CodPostal.Text);
                        command.Parameters.AddWithValue("@Pais", Pais.Text);
                        command.Parameters.AddWithValue("@Telefono", Telefono.Text);
                        command.Parameters.AddWithValue("@Fax", Fax.Text);

                        command.ExecuteNonQuery();
                    }
                }

                // Actualizar la vista de la tabla después de la inserción
                Button_CLIENTE(sender, e);

            

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar el cliente: " + ex.Message);
            }
        }
    }
}