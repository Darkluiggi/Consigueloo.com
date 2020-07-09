using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DAO
{
    public class ImageHelper
    {

        public byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                return ms.ToArray();
            }
        }

        public Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                var returnImage = Image.FromStream(ms);

                return returnImage;
            }
        }
        public string GetImageFromByteArray(byte[] byteData)
        {
            
            //Convert byte arry to base64string   
            string imreBase64Data = Convert.ToBase64String(byteData);
            string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
            return imgDataURL;
        }
    }
}
