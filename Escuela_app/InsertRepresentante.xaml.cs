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
using Entidades;
namespace Escuela_app
{
    /// <summary>
    /// Lógica de interacción para InsertRepresentante.xaml
    /// </summary>
    public partial class InsertRepresentante : Window, IDisposable
    {
        public InsertRepresentante()
        {
            InitializeComponent();
        }
        // instancias //
        private Representante representante = new Representante();
        private handler_events handler = new handler_events();
        /* local variables */
        public ERepresentante ERepresentante { get; set; }
        MessageBoxResult result1;
        /* general methods */
        private void Guardar()
        {
            try
            {
                if (ERepresentante == null) ERepresentante = new ERepresentante();
                ERepresentante.Id_representante = textBox_cedula.Text;
                ERepresentante.nomb_representante = textBox_nombre.Text;
                ERepresentante.parentesco = textBox_parentesco.Text;
                ERepresentante.celular = textBox_celular.Text;
                ERepresentante.observacion = textBox_observacion.Text;

                representante.SaveRepresentante(ERepresentante);

                if (representante.stringBuilder.Length != 0)
                {
                    MessageBox.Show(representante.stringBuilder.ToString(), "Para continuar:");
                }
                else
                {
                    MessageBox.Show("Representante registrado/actualizado con éxito");
                    this.DialogResult = true;
                    // TraerTodos();
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(string.Format("Error: {0}", er.Message), "Error inesperado");

            }
        }
        private void GetRepresentanteById(string cedula)
        {
            try
            {
                ERepresentante = representante.GetRepresentanteById(cedula);

                if (ERepresentante != null)
                {
                    // textBox_cedula.Text = ERepresentante.Id_representante;
                    textBox_nombre.Text = ERepresentante.nomb_representante;
                    textBox_parentesco.Text = ERepresentante.parentesco;
                    textBox_celular.Text = ERepresentante.celular;
                    textBox_observacion.Text = ERepresentante.observacion;
                    // Dialog box with two buttons: yes and no.
                    //
                    result1 = MessageBox.Show("Desea actualizar los datos de Representante?",
                        "Usuario Registrado",
                        MessageBoxButton.YesNo);
                    if(result1 != MessageBoxResult.Yes)
                    {
                        // cerrar
                        this.DialogResult = false;
                    } 
                }
                /*else
                    MessageBox.Show("El Representante no esta registrado");*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error inesperado");
            }
        }
        /// <summary>
        /// Actions Events of this form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name=""></param>
        /* Events */
        private void Button_cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        

        private void Button_guardar_Click(object sender, RoutedEventArgs e)
        {
            // save representante in bd
            Guardar();
            // this.DialogResult = true;
        }
        
        private void TextBox_cedula_TextChanged(object sender, TextChangedEventArgs e)
        {
            // GetRepresentanteById(textBox_cedula.Text);
            if(textBox_cedula.Text.Length >= 10 )
            {
                if (handler.VerificaIdentificacion(textBox_cedula.Text))
                {
                    Console.WriteLine("cedula valida");
                    GetRepresentanteById(textBox_cedula.Text);
                }
                else
                {
                    Console.WriteLine("cedula invalida");
                }
            }
            
        }
        
        /**/
        public void Dispose()
        {
            // throw new NotImplementedException();
            // this.Dispose();
        }
    }
}
