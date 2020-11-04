using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using LogicaNegocio;
using Entidades;
using Microsoft.Win32;
using System.Security;
using System.Drawing;
// metro desing
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using System.Diagnostics;

namespace Escuela_app
{
    /// <summary>
    /// Lógica de interacción para InsertAlumno.xaml
    /// </summary>
    public partial class InsertAlumno : MetroWindow
    {
        public InsertAlumno()
        {
            InitializeComponent();
            AsignacionComboBox();
        }
        public InsertAlumno(string cedula, string representante) :this()
        {
            // constructor for update data of Alumno
            FillFromMainWindowByCedula(cedula, representante);
        }
        // instancias //
        private Alumno alumno = new Alumno();
        private handler_events handler = new handler_events();
        public EAlumno EAlumno { get; set; }
        private string PathFileName { get; set; }
        List<string> PathFileNames = new List<string>();
        //MessageBoxResult result1;
        private bool InsertOrUpdate = false;
        InsertRepresentante WRepresentante;
        // Image imagenBoton;
        // methods
        /* general methods */
        private void GuardarAlumno()
        {
            try
            {
                if (EAlumno == null) EAlumno = new EAlumno();
                EAlumno.Id_alumno = textBox_cedula.Text;
                EAlumno.nomb_alumno = textBox_nombre.Text.ToUpper();
                EAlumno.sexo = GetSexValueFromRadioButton();
                EAlumno.fecha_nacimiento = fecha_nacimiento.SelectedDate.Value;
                EAlumno.edad = Convert.ToInt32(textBox_edad.Text);
                EAlumno.ciudad = comboBox_ciudad.SelectedItem.ToString().ToUpper();
                EAlumno.provincia = comboBox_provincia.SelectedItem.ToString().ToUpper();
                EAlumno.nacionalidad = textBox_nacional.Text;
                EAlumno.direccion_dom = textBox_direccion.Text;
                EAlumno.tipo_sangre = comboBox_sangre.SelectedItem.ToString();
                EAlumno.num_uniforme = Convert.ToInt32(textBox_uniforme.Text);
                EAlumno.id_representante = InsertOrUpdate == false ?  WRepresentante.ERepresentante.Id_representante : EAlumno.id_representante;
                EAlumno.fecha_registro = DateTime.Today;
                EAlumno.estado = true;
                SaveFilesMediaToFolderAlumno(); // called here to get the path of photo of Alumno to the object Alumno
                EAlumno.FotoPath = String.IsNullOrEmpty(handler.FotoPath) ? "": handler.FotoPath;
                EAlumno.FichaPath = String.IsNullOrEmpty(handler.FichaPath) ? "" : handler.FichaPath;
                //EAlumno.FichaPath = PathFileNames;
                alumno.SaveAlumno(EAlumno);
                /*if (alumno.stringBuilder.Length != 0)
                {
                    MessageBox.Show(alumno.stringBuilder.ToString(), "Para continuar:");
                }
                else
                {
                    MessageBox.Show("Representante registrado/actualizado con éxito");
                    this.DialogResult = true;
                    // TraerTodos();
                }*/
                // Path of image || Representante || Alumno
                //SaveFilesMediaToFolderAlumno();
                DialogResult = true;

            }
            catch (Exception er)
            {
                MessageBox.Show(string.Format("Error: {0}", er.Message), "Error inesperado");

            }
        }
        public void AsignacionComboBox()
        {
            
            comboBox_provincia.Items.Clear();
            comboBox_provincia.SelectedIndex = 0;
            comboBox_sangre.SelectedIndex = 0;
            comboBox_provincia.ItemsSource = alumno.GetListProvincias();
            comboBox_sangre.ItemsSource = alumno.getBloodType();
            textBox_cedula.Focus();
        }
        void FillFromMainWindowByCedula(string cedula, string representante)
        {
            try
            {
                EAlumno = alumno.GetAlumnoById(cedula);
                textBox_cedula.IsEnabled = false;
                InsertOrUpdate = true;
                if (EAlumno != null)
                {
                    textBox_cedula.Text = EAlumno.Id_alumno;
                    textBox_nombre.Text = EAlumno.nomb_alumno;
                    SetSexByIdFromMainWindowToUpdateAlumno(EAlumno.sexo);
                    fecha_nacimiento.SelectedDate = EAlumno.fecha_nacimiento.Date;
                    // edad => edad.text = Ealumno.edad;
                    
                    comboBox_provincia.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(EAlumno.provincia.ToString().ToLower());
                    comboBox_ciudad.Text = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(EAlumno.ciudad.ToLower());
                    textBox_nacional.Text = EAlumno.nacionalidad;
                    textBox_direccion.Text = EAlumno.direccion_dom;
                    comboBox_sangre.Text = EAlumno.tipo_sangre;
                    textBox_uniforme.Text = EAlumno.num_uniforme.ToString();
                    textBox_representante.Text = representante;
                    imagenAlumno.Source = handler.DrawImage(String.IsNullOrEmpty(EAlumno.FotoPath) ? "df_alumno.png": null, !String.IsNullOrEmpty(EAlumno.FotoPath) ? EAlumno.FotoPath: null);
                    if (!string.IsNullOrEmpty(EAlumno.FichaPath)) { 
                        btn_VerFicha.Visibility = Visibility.Visible;
                    }
                    // set image of Alumno
                    //imagenAlumno.Source = handler.DrawImage(null, PathFileName);
                    // Dialog box with two buttons: yes and no.
                    //
                    /*result1 = MessageBox.Show("Desea actualizar los datos del Alumno?",
                        "Usuario Registrado",
                        MessageBoxButton.YesNo);
                    if (result1 != MessageBoxResult.Yes)
                    {
                        // cerrar
                        DialogResult = false;
                    }*/
                }
            } catch (Exception er)
            {
                MessageBox.Show(string.Format("Error: {0}", er.Message), "Error inesperado");
            }
        }
        void SetSexByIdFromMainWindowToUpdateAlumno(string sexo)
        {
            radioButton_masculino.IsChecked = sexo == "Masculino" ?  true : false;
            radioButton_femenino.IsChecked = sexo == "Femenino" ? true : false;
        }
        string GetSexValueFromRadioButton()
        {
            string SexVal = "";
            if (radioButton_masculino.IsChecked == false && radioButton_femenino.IsChecked == false)
            {
                return "Masculino";
            }
            SexVal = radioButton_masculino.Content.ToString() == "Masculino" ? "Masculino" : "Femenino";
            return SexVal;
        }
        
        /*private Image GetImageButtom(Button boton, string name)
        {
            Image imagenBoton;
            imagenBoton = boton.FindName(name) as Image;
            return imagenBoton;
        }*/
        int CalcularEdad(DatePicker fecha)
        {
            DateTime fechaActual = DateTime.Today;
            int edad = fechaActual.Year - fecha.SelectedDate.Value.Year;
            if (fechaActual < fecha.SelectedDate.Value.AddYears(edad)) edad--;
            return edad;
        }
        public void OpenFileDialogToPath(bool AllowMultiSelect)
        {
            try
            {
                //if (!(String.IsNullOrEmpty(textBox_representante.Text)))
                //{
                    OpenFileDialog openFileDialog = new OpenFileDialog()
                    {
                        Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg",
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        Multiselect = AllowMultiSelect
                    };

                    if (openFileDialog.ShowDialog() == true)
                    {
                        
                        if (AllowMultiSelect)
                        {
                            PathFileNames = openFileDialog.FileNames.ToList();
                        } else
                        {
                            PathFileName = openFileDialog.FileName;
                        }
                    }
                //}
            }
            catch (SecurityException ex)
            {
                // The user lacks appropriate permissions to read files, discover paths, etc.
                MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
                    "Error message: " + ex.Message + "\n\n" +
                    "Details (send to Support):\n\n" + ex.StackTrace
                );
            }
            catch (Exception ex)
            {
                // Could not load the image - probably related to Windows file system permissions.
                MessageBox.Show("Cannot display the image: " + PathFileName.Substring(PathFileName.LastIndexOf('\\'))
                    + ". You may not have permission to read the file, or " +
                    "it may be corrupt.\n\nReported error: " + ex.Message);
            }
            
        }
        public void SaveFilesMediaToFolderAlumno()
        {
            if(PathFileName != null)
                handler.AlreadyExist(PathFileName, textBox_representante.Text, textBox_nombre.Text);
            if (PathFileNames.Any())
                handler.MultiImagesToPDF(PathFileNames, textBox_representante.Text, textBox_nombre.Text);
            
            //handler.AlreadyExist(PathFileName, textBox_representante.Text, textBox_nombre.Text);
            //handler.MultiImagesToPDF(PathFileNames, textBox_representante.Text, textBox_nombre.Text);
        }
        // events 
        private void Button_representante_Click(object sender, RoutedEventArgs e)
        {
            // Call Representatne form modal
            using(WRepresentante = new InsertRepresentante()) {
               WRepresentante.ShowDialog();
               if (WRepresentante.DialogResult == false)
                {
                    if(WRepresentante.ERepresentante != null)
                    {
                        textBox_representante.Text = WRepresentante.ERepresentante.nomb_representante;
                    }
                } else
                {
                    textBox_representante.Text = WRepresentante.ERepresentante.nomb_representante;
                }
            }
        }

        

        private void ComboBox_provincia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ListaCiudades = new List<string>();
            comboBox_ciudad.IsEnabled = true;
            string item = comboBox_provincia.SelectedItem.ToString();
            if (alumno.retrieveAllCitiesByProvince(item) == null)
            {
                comboBox_ciudad.IsEnabled = false;
            }
            else
            {
                ListaCiudades = alumno.retrieveAllCitiesByProvince(item);
            }
            comboBox_ciudad.ItemsSource = ListaCiudades;
            comboBox_ciudad.SelectedIndex = 0;
        }
        private void Fecha_nacimiento_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(fecha_nacimiento.SelectedDate != null)
            textBox_edad.Text = handler.CalcularEdad(fecha_nacimiento).ToString();

        }
        private void TextBox_cedula_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Tab)
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
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Tab)
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
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Tab)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        // draw the representante image button
        private void TextBoxes_TextChanged(object sender, TextChangedEventArgs e)
        {
            button_representante.IsEnabled = handler.ValidateTextFilled(grilla);
        }

        private void Button_guardar_Click(object sender, RoutedEventArgs e)
        {
            GuardarAlumno();
            Button_limpiar_Click(sender, e);
        }


        private void Button_cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Button_limpiar_Click(object sender, RoutedEventArgs e)
        {
            handler.ClearFields(grilla);
            imagenAlumno.Source = handler.DrawImage("df_alumno.png", null);
            textBox_cedula.IsEnabled = true;
        }
        private void OnChanges_TextRepresentante(object sender, TextChangedEventArgs e)
        {
            button_ficha.IsEnabled = String.IsNullOrEmpty(textBox_representante.Text) ? false : true;
            button_fotoAlumno.IsEnabled = String.IsNullOrEmpty(textBox_representante.Text) ? false : true;
            button_guardar.IsEnabled = String.IsNullOrEmpty(textBox_representante.Text) ? false : true;
        }
        private void Button_ficha_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialogToPath(true);
        }

        private void Button_fotoAlumno_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialogToPath(false);
            if (PathFileName != null)
            {
                imagenAlumno.Source = handler.DrawImage("", PathFileName);
                //imagenAlumno.Source = handler.Convert(handler.ImageWpfToGDI(handler.DrawImage("", PathFileName)));
            }
        }
        private void AbrirPdf(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(EAlumno.FichaPath);
            //Process.Start("D:\\Descargas\\CertiVotacion.pdf");
            if(!string.IsNullOrEmpty(EAlumno.FichaPath))
            {
                Process.Start(EAlumno.FichaPath);
            }
        }
    }
}
