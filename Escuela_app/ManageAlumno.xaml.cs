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
// metro desing
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
namespace Escuela_app
{
    /// <summary>
    /// Lógica de interacción para ManageAlumno.xaml
    /// </summary>
    public partial class ManageAlumno : MetroWindow
    {
        public ManageAlumno(List<string> Mainlista)
        {
            InitializeComponent();
            ListText = Mainlista;
            AsignacionToControls();
        }
        // instances and variables
        private List<string> ListText = new List<string>();
        private Alumno alumno = new Alumno();
        private EAlumno EAlumno { get; set; }
        /* methods  */
        void AsignacionToControls()
        {
            txt_cedula.Text = ListText[0];
            txt_nombre.Text = ListText[1];
            activo.IsChecked = ListText[12].Equals("True") ? true: false; 
            inactivo.IsChecked = ListText[12].Equals("False") ? true : false;
            observacion.Text = alumno.GetAlumnoById(txt_cedula.Text).observacion;
            observacion.Focus();
        }
        private void UpdateStateAlumno()
        {
            try
            {
                if (EAlumno == null) EAlumno = new EAlumno();
                EAlumno.Id_alumno = txt_cedula.Text;
                EAlumno.estado = GetStateFromRatioControls();
                EAlumno.observacion = observacion.Text;
                alumno.UpdateStatusAlumno(EAlumno);
                DialogResult = true;
            } catch(Exception er)
            {
                Console.WriteLine(er.Message);
            }
        }
        public bool GetStateFromRatioControls()
        {
            if (activo.IsChecked == true)
                return true;
            if (inactivo.IsChecked == true)
                return false;
            return bool.Parse(ListText[12]);
        }

        /* events */
        private void Btn_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Btn_Guardar_Click(object sender, RoutedEventArgs e)
        {
            UpdateStateAlumno();
        }
    }
}
