using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;
using System.Drawing;
using Image = System.Windows.Controls.Image;
using System.IO;
// iText 7
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using PageSize = iText.Kernel.Geom.PageSize;
using Imagen = iText.Layout.Element.Image;
using System.Windows;

namespace LogicaNegocio
{
    public class handler_events
    {
        /* variables */
        string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\Resources\"));
        string ImagePath = "";
        /* instances*/
        public ImageSource DrawImage(string filename, string Filename)
        {
            // build the path of the image
            if (String.IsNullOrEmpty(Filename))
            {
                ImagePath = path + filename;
            } else
            {
                ImagePath = Filename;
            }
            
            
            // Load the bitmap into a bitmap image object
            var bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(ImagePath);
            //bitmap.DecodePixelHeight = 100;
            //bitmap.DecodePixelWidth = 100;
            bitmap.EndInit();
            return bitmap;
        }
        
        public bool ValidateTextFilled(Grid grilla)
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
        public void ClearFields(Grid grid)
        {
            
            /// GetAll UIElement
            UIElementCollection element = grid.Children;

            /// casting the UIElementCollection into List
            List<FrameworkElement> lstElement = element.Cast<FrameworkElement>().ToList();

            /// Geting all Control from list
            var lstControl = lstElement.OfType<Control>();

            foreach (Control ctrl in lstControl)
            {
                ///Hide all Controls
                
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
        public void ClearFieldsDockPanel(StackPanel panel)
        {
            UIElementCollection element = panel.Children;
            List<FrameworkElement> lstElement = element.Cast<FrameworkElement>().ToList();
            var lstControl = lstElement.OfType<Control>();
            foreach(Control ctrl in lstControl)
            {
                if(ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                if(ctrl is ComboBox)
                    ((ComboBox)ctrl).SelectedIndex = -1;
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
        public void CopyFileToFolder(string source, string DirRepresentante, string SubDirAlumno)
        {
            try
            {
                string path = Directory.GetCurrentDirectory();
                string dest = Path.GetFullPath(Path.Combine(path, $@"..\..\Resources\Escuela\{DirRepresentante}\{SubDirAlumno}\")) + $"{SubDirAlumno}" + Path.GetExtension(source);
                File.Copy(source, dest, true);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void AlreadyExist(string source, string DirRepresentatne, string SubDirAlumno)
        {
            string path = Directory.GetCurrentDirectory();
            string dest = Path.GetFullPath(Path.Combine(path, $@"..\..\Resources\Escuela\{DirRepresentatne}"));
            // If directory does not exist, create it. 
            if (!Directory.Exists(dest))
            {
                Directory.CreateDirectory(dest);
                Directory.CreateDirectory(dest + $@"\{SubDirAlumno}");
                CopyFileToFolder(source, DirRepresentatne, SubDirAlumno);
            }
            if (!Directory.Exists(dest + $@"\{SubDirAlumno}"))
            {
                Directory.CreateDirectory(dest + $@"\{SubDirAlumno}");
                CopyFileToFolder(source, DirRepresentatne, SubDirAlumno);
                return;
            }
            if (Directory.Exists(dest) && Directory.Exists(dest + $@"\{SubDirAlumno}"))
            {
                CopyFileToFolder(source, DirRepresentatne, SubDirAlumno);
            }
        }
        
        public void MultiImagesToPDF(List<string> list, string DirRepresentante ,string SubDirAlumno)
        {
            try
            {
                //
                //
                string path = Directory.GetCurrentDirectory();
                string dest = Path.GetFullPath(Path.Combine(path, $@"..\..\Resources\Escuela\{DirRepresentante}\{SubDirAlumno}\")) + $"ficha_medica_{SubDirAlumno}" + ".pdf";
                Imagen img = new Imagen(ImageDataFactory.Create(list[0]));

                PdfDocument pdf = new PdfDocument(new PdfWriter(dest));// + $"\ficha_medica_{SubDirAlumno}" + ".pdf"));
                Document document = new Document(pdf, new PageSize(img.GetImageWidth(), img.GetImageHeight()));
                int i = 0;
                foreach (string file in list)
                {
                    
                    img = new Imagen(ImageDataFactory.Create(file));
                    pdf.AddNewPage(new PageSize(img.GetImageWidth(), img.GetImageHeight()));
                    img.SetFixedPosition(i + 1, 0, 0);
                    document.Add(img);
                    i++;
                }
                document.Close();
            }
            catch(IOException e)
            {
                Console.WriteLine("erro es " + e.Message);
            }
        }
    }
}
