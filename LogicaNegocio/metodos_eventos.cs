using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
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

        
    }
}
