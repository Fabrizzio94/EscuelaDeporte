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
// metro desing
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using Entidades;
using LogicaNegocio;
using System.Data;

namespace Escuela_app
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            AsignacionComboBox();
        }
        /* instancias */
        private Alumno _MetodosAlumno = new Alumno();
        private handler_events handler = new handler_events();
        InsertAlumno alumno;
        DataTable datos = new DataTable();
        /* Variables  locales */

        /* methods */
        public  void AsignacionComboBox ()
        {
            //comboBox1_sexo.IsEnabled = String.IsNullOrEmpty(textBox_nombre.Text) ? false : true;
            /*comboBox1_inicio.Items.Clear();
            comboBox1_inicio.SelectedIndex = 0;
            comboBox1_inicio.ItemsSource = _MetodosAlumno.GetListProvincias();*/
            // datagridAlumno.ItemsSource = _MetodosAlumno.getAll().Tables[0].DefaultView;
            //datagridAlumno.DataContext = _MetodosAlumno.getAll().Tables[0].DefaultView; // <-- error

        }
        void LimpiarVentana()
        {
            handler.ClearFieldsDockPanel(stack_1);
            handler.ClearFieldsDockPanel(stack_2);
        }
        private void TextBoxes_Changes(object sender, EventArgs e)
        {
            try
            {
                string SexoVal = comboBox1_sexo.SelectedIndex != -1  ? comboBox1_sexo.SelectedValue.ToString() : "";
                DateTime FechaVal = fecha_nacimiento.SelectedDate != null ? fecha_nacimiento.SelectedDate.Value.Date: DateTime.Today.Date;
                string NombreVal = (String.IsNullOrWhiteSpace(textBox_nombre.Text) || String.IsNullOrEmpty(textBox_nombre.Text)) ? "5" : textBox_nombre.Text.ToUpper();
                string CiudadVal = textbox_ciudad.Text.ToUpper();
                bool EstadoVal = Convert.ToBoolean(checkbox_estado.IsChecked);
                datagridAlumno.ItemsSource = _MetodosAlumno.GetAlumnosById_Nombre(textBox_cedula.Text, NombreVal, EstadoVal).Tables[0].DefaultView;
                if (comboBox1_sexo.SelectedIndex != -1 && fecha_nacimiento.SelectedDate == null)
                {
                    // lista por sexo
                    // alumno.sexo = 'Masculino' and alumno.estado_alumno = 't'
                    datagridAlumno.ItemsSource = _MetodosAlumno.GetAlumnosBySex(SexoVal, EstadoVal).Tables[0].DefaultView;
                }
                if (comboBox1_sexo.SelectedIndex != -1 && fecha_nacimiento.SelectedDate != null)
                {
                    // lista por sexo y fecha de nacimiento 
                    // (alumno.sexo = 'Masculino' and alumno.fecha_nacimiento = '12-01-2000') and alumno.estado_alumno = 't'
                    datagridAlumno.ItemsSource = _MetodosAlumno.GetAlumnosBySexo_FechaNacimiento(SexoVal, FechaVal, EstadoVal).Tables[0].DefaultView;
                }
                if (!String.IsNullOrEmpty(textbox_ciudad.Text) && comboBox1_sexo.SelectedIndex == -1 && fecha_nacimiento.SelectedDate == null)
                {
                    // lista por ciudad
                    // alumno.ciudad = '' and alumno.estado_alumno = 't'
                    datagridAlumno.ItemsSource = _MetodosAlumno.GetAlumnosByCiudad(CiudadVal, EstadoVal).Tables[0].DefaultView;
                }
                if (!String.IsNullOrEmpty(textbox_ciudad.Text) && fecha_nacimiento.SelectedDate != null && comboBox1_sexo.SelectedIndex == -1)
                {
                    // lista por ciudad y fecha de nacimiento
                    // alumno.ciudad = '' and alumno.fecha_nacimiento = '' ) and alumno.estado_alumno = 't'
                    datagridAlumno.ItemsSource = _MetodosAlumno.GetAlumnosByCiudad_FechaNacimiento(CiudadVal, FechaVal, EstadoVal).Tables[0].DefaultView;
                }
                if(!String.IsNullOrEmpty(textbox_ciudad.Text) && fecha_nacimiento.SelectedDate != null && comboBox1_sexo.SelectedIndex != -1)
                {
                    // lista por ciudad, fecha nacimiento y sexo
                    // alumno.ciudad = '' and alumno.fecha_nacimiento = ''  and alumno.sexo) and alumno.estado_alumno = 't'
                    datagridAlumno.ItemsSource = _MetodosAlumno.GetAlumnosBySexo_FechaNacimiento_Ciudad(SexoVal, FechaVal, CiudadVal, EstadoVal).Tables[0].DefaultView;
                }
            }
            catch (Exception er)
            {
                Console.Write(er.Message);
            }
            
        }


        /* functions */
        List<string> RowToList(DataRowView rowView)
        {
            List<string> items = new List<string>();
            for (int i = 0; i < 13; i++)
            {
                items.Add(rowView.Row[i].ToString());

            }
            return items;
        }
        /* TextBox events - Windows1*/
        private void TextBox1_inicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 )
            {
                e.Handled = false;
            }else{
                e.Handled = true;
            }      
        }

        private void TextBox1_inicio_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            } else { e.Handled = false; }
        }

        private void DatePicker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char character = Convert.ToChar(e.Text);
            if (char.IsNumber(character) || character.Equals('/') || character.Equals('-'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void TextBox_nombre_KeyDown(object sender, KeyEventArgs e)
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
        private void Button_Nuevo_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            alumno = new InsertAlumno();
            alumno.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Limpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarVentana();
        }
        
        private void DatagridAlumno_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(datagridAlumno.SelectedIndex != -1) { 
                /*TextBlock x = datagridAlumno.Columns[0].GetCellContent(datagridAlumno.Items[datagridAlumno.SelectedIndex]) as TextBlock;
                if (x != null)
                    MessageBox.Show(x.Text);*/
            }

        }

        private void DatagridAlumno_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = datagridAlumno.SelectedItem;
            string ID = (datagridAlumno.SelectedCells[0].Column.GetCellContent(item)).ToString();
            MessageBox.Show(ID);
        }

        public void OnChecked(object sender, RoutedEventArgs e)
        {
            if (datagridAlumno.SelectedIndex != -1)
            {
                List<string> list = new List<string>();
                DataRowView dataRowView = (DataRowView)datagridAlumno.SelectedItem;

                // call edit state window
                ManageAlumno state = new ManageAlumno(RowToList(dataRowView));
                state.ShowDialog();
                //LimpiarVentana();
                TextBoxes_Changes(sender, e);
            }
        }
        
    }
}
