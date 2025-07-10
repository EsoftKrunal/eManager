<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public class Upload : IHttpHandler {
    
           private ImageCodecInfo GetEncoder(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
        }
        return null;
    }
    private void SaveToReduced(Image i,string ToPath)
    {
        // Get a bitmap. The using statement ensures objects  
        // are automatically disposed from memory after use.  
        using (Bitmap bmp1 = new Bitmap(i))
        {
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

            // Create an Encoder object based on the GUID  
            // for the Quality parameter category.  
            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.  
            // An EncoderParameters object has an array of EncoderParameter  
            // objects. In this case, there is only one  
            // EncoderParameter object in the array.  
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(ToPath, jpgEncoder, myEncoderParameters);

            //myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            //myEncoderParameters.Param[0] = myEncoderParameter;
            //bmp1.Save(@"C:\TestPhotoQualityHundred.jpg", jpgEncoder, myEncoderParameters);

            //// Save the bitmap as a JPG file with zero quality level compression.  
            //myEncoderParameter = new EncoderParameter(myEncoder, 0L);
            //myEncoderParameters.Param[0] = myEncoderParameter;
            //bmp1.Save(@"C:\TestPhotoQualityZero.jpg", jpgEncoder, myEncoderParameters);
        }
    }
    public Image FixedSize(Image imgPhoto, int Width, int Height)
    {
        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width -
                          (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height -
                          (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap bmPhoto = new Bitmap(Width, Height,
                          PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        grPhoto.Clear(Color.White);
        grPhoto.InterpolationMode =
                InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new Rectangle(destX, destY, destWidth, destHeight),
            new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Expires = -1;
        try
        {
            HttpPostedFile postedFile = context.Request.Files["Filedata"];
            
            string savepath = "";
            string tempPath = "";
            tempPath = "Temp"; 
            savepath = context.Server.MapPath(tempPath);
            string filename = postedFile.FileName;
            if (!Directory.Exists(savepath))
                Directory.CreateDirectory(savepath);
            string key = DateTime.Now.ToString("ddmmyyhhmmss_ffff_");

            Image img=Bitmap.FromStream(postedFile.InputStream);
            Image img1=FixedSize(img,800,600);
            SaveToReduced(img1,savepath + @"\" + key + filename);

            //postedFile.SaveAs(savepath + @"\" + key + filename);
            context.Response.Write(tempPath + "/" + filename);
            context.Response.StatusCode = 200;
        }
        catch (Exception ex)
        {
            context.Response.Write("Error: " + ex.Message);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
}