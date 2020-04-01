using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;

namespace LogicaNegocio
{
    public class metodos_eventos
    {

        public ImageSource DrawBitmapGreyscale(string filename)
        {
            // Load the bitmap into a bitmap image object
            var bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(filename);
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
            // Load the bitmap into a bitmap image object
            var bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(filename);
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
    }
}
