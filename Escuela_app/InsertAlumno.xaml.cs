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
using System.Windows.Shapes;

using LogicaNegocio;

namespace Escuela_app
{
    /// <summary>
    /// Lógica de interacción para InsertAlumno.xaml
    /// </summary>
    public partial class InsertAlumno : Window
    {
        public InsertAlumno()
        {
            InitializeComponent();
            AsignacionComboBox();
        }
        // instancias //
        private Alumno _MetodosAlumno = new Alumno();
        private metodos_eventos methods_event = new metodos_eventos();
        MainWindow ventana;
        Image imagenBoton;
        string rutaimage = "";
        // methods
        public void AsignacionComboBox()
        {
            
            comboBox_provincia.Items.Clear();
            comboBox_provincia.SelectedIndex = 0;
            comboBox_sangre.SelectedIndex = 0;
            comboBox_provincia.ItemsSource = _MetodosAlumno.GetListProvincias();
            comboBox_sangre.ItemsSource = _MetodosAlumno.getBloodType();
            imagenBoton = button_representante.FindName("imagen_repre") as Image;
            rutaimage = imagen_repre.Source.ToString();
            imagenBoton.Source = (methods_event.DrawBitmapGreyscale(imagen_repre.Source.ToString()));
            textBox_cedula.Focus();
        }

        int calcularEdad(DatePicker fecha)
        {
            DateTime fechaActual = DateTime.Today;
            int edad = fechaActual.Year - fecha.SelectedDate.Value.Year;
            if (fechaActual < fecha.SelectedDate.Value.AddYears(edad)) edad--;
            return edad;
        }
        void validateTextFilled()
        {
            bool validData = true;
            foreach (Control control in grilla.Children.OfType<TextBox>())
            {
                if (control is TextBox)
                {
                    TextBox textbox = control as TextBox;
                    validData &= !string.IsNullOrWhiteSpace(textbox.Text);
                }
            }

            button_representante.IsEnabled = validData;
            if(validData)
            {
                imagenBoton.Source = methods_event.DrawImage(rutaimage);
            }
        }
        // events 
        private void Button_representante_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("click");
            button_representante.IsEnabled = false;
            // imagenBoton = button_representante.FindName("imagen_repre") as Image;
            // imagenBoton.Source = methods_event.DrawImage(rutaimage);
            // imagen_repre.IsEnabled = false;

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Console.WriteLine("Cerrar");
            ventana = new MainWindow();
            ventana.Show();
            this.Close();
        }

        private void ComboBox_provincia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ListaCiudades = new List<string>();
            comboBox_ciudad.IsEnabled = true;
            string item = comboBox_provincia.SelectedItem.ToString();
            if (_MetodosAlumno.retrieveAllCitiesByProvince(item) == null)
            {
                comboBox_ciudad.IsEnabled = false;
            }
            else
            {
                ListaCiudades = _MetodosAlumno.retrieveAllCitiesByProvince(item);
            }
            comboBox_ciudad.ItemsSource = ListaCiudades;
            comboBox_ciudad.SelectedIndex = 0;
        }
        private void Fecha_nacimiento_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            textBox_edad.Text = calcularEdad(fecha_nacimiento).ToString();
        }
        private void TextBox_cedula_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TextBox_cedula_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            else { e.Handled = false; }
        }
        
        private void TextBox_nombre_KeyDown(object sender, KeyEventArgs e)
        {
            if(char.IsLetter((char)e.Key) || e.Key == Key.Back || (e.Key >= Key.D0 && e.Key <= Key.D9))
            {
                if (!(e.Key == Key.Z || e.Key == Key.X || e.Key == Key.V || e.Key == Key.Y || e.Key == Key.W))
                {
                    e.Handled = true;
                }
            }
            else { e.Handled = false; }
            
        }

        private void TextBox_nacional_KeyDown(object sender, KeyEventArgs e)
        {
            if (char.IsLetter((char)e.Key) || e.Key == Key.Back || (e.Key >= Key.D0 && e.Key <= Key.D9))
            {
                if (!(e.Key == Key.Z || e.Key == Key.X || e.Key == Key.V || e.Key == Key.Y || e.Key == Key.W))
                {
                    e.Handled = true;
                }
            }
            else { e.Handled = false; }
        }

        private void TextBox_edad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TextBox_uniforme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void TextBox_uniforme_TextChanged(object sender, TextChangedEventArgs e)
        {
            validateTextFilled();
        }
    }
}
