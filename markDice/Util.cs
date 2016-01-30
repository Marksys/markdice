using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.IO;

namespace markDice
{
    public static class Util
    {
        public static ImageSource getImgFromByte(byte[] arrayImg)
        {

            BitmapImage bmpImage = new BitmapImage();
            bmpImage.SetSource(new MemoryStream(arrayImg));
            return bmpImage;
        }

        public static byte[] toByte(Image img)
        {
            BitmapImage bmpToArray = img.Source as BitmapImage;
            WriteableBitmap wb = new WriteableBitmap(bmpToArray);
            
            

            MemoryStream msWrite = new MemoryStream();
            wb.SaveJpeg(msWrite, 100, 100, 0, 90);
            msWrite.Seek(0, SeekOrigin.Begin);

            if (msWrite != null)
            {
                msWrite.Position = 0;
                MemoryStream ms = new MemoryStream();
                byte[] buffer = new byte[msWrite.Length];
                int read;
                while ((read = msWrite.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return buffer;
            }
            return null;
        }

        public static byte[] toByte(BitmapImage img)
        {
            BitmapImage bmpToArray = img;
            WriteableBitmap wb = new WriteableBitmap(bmpToArray);

            MemoryStream msWrite = new MemoryStream();
            wb.SaveJpeg(msWrite, 100, 100, 0, 90);
            msWrite.Seek(0, SeekOrigin.Begin);

            if (msWrite != null)
            {
                msWrite.Position = 0;
                MemoryStream ms = new MemoryStream();
                byte[] buffer = new byte[msWrite.Length];
                int read;
                while ((read = msWrite.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return buffer;
            }
            return null;
        }
    }
}
