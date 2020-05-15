﻿using System;
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
            /*comboBox1_inicio.Items.Clear();
            comboBox1_inicio.SelectedIndex = 0;
            comboBox1_inicio.ItemsSource = _MetodosAlumno.GetListProvincias();*/
            // datagridAlumno.ItemsSource = _MetodosAlumno.getAll().Tables[0].DefaultView;
            //datagridAlumno.DataContext = _MetodosAlumno.getAll().Tables[0].DefaultView; // <-- error
            
        }
        private void TextBoxes_Changes(object sender, EventArgs e)
        {
            try
            {
                string SexoVal = comboBox1_sexo.SelectedIndex != -1  ? comboBox1_sexo.SelectedValue.ToString() : "";
                DateTime FechaVal = fecha_nacimiento.SelectedDate != null ? fecha_nacimiento.SelectedDate.Value.Date: DateTime.Today.Date;
                datagridAlumno.ItemsSource = _MetodosAlumno.GetAllumnosByFields(textBox_cedula.Text, textBox_nombre.Text, SexoVal, FechaVal, textbox_ciudad.Text.ToUpper(), Convert.ToBoolean(checkbox_estado.IsChecked)).Tables[0].DefaultView;
            }
            catch (Exception er)
            {
                Console.Write(er.Message);
            }
            
        }


        /* functions */
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
            //handler.ClearFields(grilla);
        }
    }
}
