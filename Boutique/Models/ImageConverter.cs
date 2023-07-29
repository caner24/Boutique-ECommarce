using System.Drawing;

namespace Boutique.Models
{
    public static class ImageConverter
    {
        public static byte[] ImageToByteArray(Image image)
        {
            if (image != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
            else
            {
                return null;
            }
        }
        public static string ByteArrayToImageAsync(byte[] image)
        {
            string base64String = Convert.ToBase64String(image, 0, image.Length);

            return "data:image/png;base64," + base64String;
        }
    }
}
