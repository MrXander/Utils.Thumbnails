using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Utils.Thumbnails.Models
{
    public class ImageProcessor
    {
        public static MemoryStream Resize(string path, int maxWidth, int maxHeight)
        {
            if (File.Exists(path))
            {
                using (var image = new Bitmap(path))
                {
                    var ratioX = (double)maxWidth / image.Width;
                    var ratioY = (double)maxHeight / image.Height;
                    var ratio = Math.Min(ratioX, ratioY);

                    var newWidth = (int)(image.Width * ratio);
                    var newHeight = (int)(image.Height * ratio);

                    var newImage = new Bitmap(newWidth, newHeight);
                    Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
                    var mStream = new MemoryStream();
                    newImage.Save(mStream, ImageFormat.Jpeg);
                    mStream.Position = 0;
                    return mStream;
                }                                
            }

            return new MemoryStream();
        }
    }
}