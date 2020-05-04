using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;
using System.IO;

namespace LogicaNegocio
{
    public class handler_events
    {
        /* variables */
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\Resources\"));
        string ImagePath = "";
        /* instances*/
        public ImageSource DrawBitmapGreyscale(string filename)
        {
            // build the path of the image
            ImagePath = path + filename;
            // Load the bitmap into a bitmap image object
            var bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(ImagePath);
            bitmap.EndInit();

            // Convert the bitmap to greyscale, and draw it.
            FormatConvertedBitmap myFormatedConvertedBitmap = new FormatConvertedBitmap();
            myFormatedConvertedBitmap.BeginInit();
            myFormatedConvertedBitmap.Source = bitmap;
            myFormatedConvertedBitmap.DestinationFormat = PixelFormats.Gray8;
            myFormatedConvertedBitmap.EndInit();
            return myFormatedConvertedBitmap;
        }
        public ImageSource DrawImage(string filename)
        {
            // build the path of the image
            ImagePath = path + filename;
            // Load the bitmap into a bitmap image object
            var bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(ImagePath);
            bitmap.EndInit();
            return bitmap;
        }

        public bool ValidateTextFilled(Grid grilla, Button button_representante)
        {
            bool validData = true;
            // Control control in grilla.Children.OfType<TextBox>()
            foreach (Control control in grilla.Children.OfType<TextBox>())
            {
                if (control is TextBox && control.Name != "textBox_representante")
                {
                    // Console.WriteLine(control.Name);
                    TextBox textbox = control as TextBox;
                    validData &= !string.IsNullOrWhiteSpace(textbox.Text);
                    
                }
            }
            return validData;
        }
        public void ClearFields(Grid grilla)
        {
            foreach (Control ctrl in grilla.Children)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                if (ctrl is ComboBox)
                    ((ComboBox)ctrl).SelectedIndex = 0;
                if (ctrl is RadioButton)
                    ((RadioButton)ctrl).IsChecked = false;
                if (ctrl is DatePicker)
                    ((DatePicker)ctrl).ClearValue(DatePicker.SelectedDateProperty);
            }
        }
        public int CalcularEdad(DatePicker fecha)
        {
            DateTime fechaActual = DateTime.Today;
            int edad = fechaActual.Year - fecha.SelectedDate.Value.Year;
            if (fechaActual < fecha.SelectedDate.Value.AddYears(edad)) edad--;
            return edad;
        }

        public bool VerificaIdentificacion(string identificacion)
        {
            bool estado = false;
            char[] valced = new char[13];
            int provincia;
            if (identificacion.Length >= 10)
            {
                valced = identificacion.Trim().ToCharArray();
                provincia = int.Parse((valced[0].ToString() + valced[1].ToString()));
                if (provincia > 0 && provincia < 25)
                {
                    if (int.Parse(valced[2].ToString()) < 6)
                    {
                        estado = VerificaCedula(valced);
                    }
                    else if (int.Parse(valced[2].ToString()) == 6)
                    {
                        estado = VerificaSectorPublico(valced);
                    }
                    else if (int.Parse(valced[2].ToString()) == 9)
                    {

                        estado = VerificaPersonaJuridica(valced);
                    }
                }
            }
            return estado;
        }

        public static bool VerificaCedula(char[] validarCedula)
        {
            int aux = 0, par = 0, impar = 0, verifi;
            for (int i = 0; i < 9; i += 2)
            {
                aux = 2 * int.Parse(validarCedula[i].ToString());
                if (aux > 9)
                    aux -= 9;
                par += aux;
            }
            for (int i = 1; i < 9; i += 2)
            {
                impar += int.Parse(validarCedula[i].ToString());
            }

            aux = par + impar;
            if (aux % 10 != 0)
            {
                verifi = 10 - (aux % 10);
            }
            else
                verifi = 0;
            if (verifi == int.Parse(validarCedula[9].ToString()))
                return true;
            else
                return false;
        }
        public static bool VerificaPersonaJuridica(char[] validarCedula)
        {
            int aux = 0, prod, veri;
            veri = int.Parse(validarCedula[10].ToString()) + int.Parse(validarCedula[11].ToString()) + int.Parse(validarCedula[12].ToString());
            if (veri > 0)
            {
                int[] coeficiente = new int[9] { 4, 3, 2, 7, 6, 5, 4, 3, 2 };
                for (int i = 0; i < 9; i++)
                {
                    prod = int.Parse(validarCedula[i].ToString()) * coeficiente[i];
                    aux += prod;
                }
                if (aux % 11 == 0)
                {
                    veri = 0;
                }
                else if (aux % 11 == 1)
                {
                    return false;
                }
                else
                {
                    aux = aux % 11;
                    veri = 11 - aux;
                }

                if (veri == int.Parse(validarCedula[9].ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool VerificaSectorPublico(char[] validarCedula)
        {
            int aux = 0, prod, veri;
            veri = int.Parse(validarCedula[9].ToString()) + int.Parse(validarCedula[10].ToString()) + int.Parse(validarCedula[11].ToString()) + int.Parse(validarCedula[12].ToString());
            if (veri > 0)
            {
                int[] coeficiente = new int[8] { 3, 2, 7, 6, 5, 4, 3, 2 };

                for (int i = 0; i < 8; i++)
                {
                    prod = int.Parse(validarCedula[i].ToString()) * coeficiente[i];
                    aux += prod;
                }

                if (aux % 11 == 0)
                {
                    veri = 0;
                }
                else if (aux % 11 == 1)
                {
                    return false;
                }
                else
                {
                    aux = aux % 11;
                    veri = 11 - aux;
                }

                if (veri == int.Parse(validarCedula[8].ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public void CopyFileToFolder(string source)
        {
            //string source = openFileDialog1.FileName;
            //string name = "Fabricio";
            string path = Directory.GetCurrentDirectory();
            // dest = Directory.GetCurrentDirectory();
            //Console.WriteLine($@"..\..\Resources\Escuela\{name}");
            string dest = Path.GetFullPath(Path.Combine(path, @"..\..\Resources\Escuela\")) + Path.GetFileName(source);
            Console.WriteLine(dest);
            //File.Copy(source, dest);
        }
        public void AlreadyExist(string DirRepresentatne, string SubDirAlumno)
        {
            string path = Directory.GetCurrentDirectory(); ;
            string dest = Path.GetFullPath(Path.Combine(path, $@"..\..\Resources\Escuela\{DirRepresentatne} "));
            // If directory does not exist, create it. 
            if (!Directory.Exists(dest))
            {
                Directory.CreateDirectory(dest);
                Directory.CreateDirectory(dest + $@"\{SubDirAlumno}");
            } else
            {
                // dest += $@"\{SubDirAlumno}";
                if (!Directory.Exists(dest + $@"\{SubDirAlumno}"))
                {
                    Directory.CreateDirectory(dest + $@"\{SubDirAlumno}");
                    
                }
            }
            //return false;
        }
        
    }
}
