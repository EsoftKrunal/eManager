using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using ShipSoft.CrewManager.DataAccessLayer;
using System.Data.SqlClient;  
namespace ShipSoft.CrewManager.Operational
{
    public class UtilityManager 
    {
        public static void LogException(string httpErrorUrl, Exception ex)
        {
            using (StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath("~/ExceptionLog/LogFile.txt"), true))
            {
                sw.WriteLine("[ Date: " + DateTime.Now.ToLongDateString() + " ] [ Time : " + DateTime.Now.ToLongTimeString() + " ] " + Environment.NewLine + httpErrorUrl +Environment.NewLine + ex.Message.ToString() + Environment.NewLine + ex.Source.ToString() + Environment.NewLine + ex.StackTrace.ToString() + Environment.NewLine + ex.Data.ToString() + Environment.NewLine + Environment.NewLine);
            }
        }
        public static string getLocation()
        {
            DataAccessBase b = new DataAccessBase();  
            DataBaseHelper dbhelper = new DataBaseHelper("ExecQuery");
            SqlParameter[] pms={new SqlParameter("Query","Select BranchName from Branch Where Self=1")}; 
            object id = dbhelper.RunScalar(b.ConnectionString,pms);
            return id.ToString().Substring(0,1); 
        }
        public string UploadFileToServer(HttpPostedFile fileToUpload, string existingFileName, string fileType)
        {
            // fileType C=Crew Image, T=Travel, P=Professional, A=Academic, M=Medical, H-CRM & HRD, R-Training Requirements, L-Appraisal
            string uploadFileName = "";
            string path;
            string fullPath;

            if (fileType == "Y") // Upload Crew Image
            {
                // Validate file size is not greater than 50 kb
                // Return ERR if filesize is greater than 50 kb else return file name

                if (fileToUpload.ContentLength > (1024 * 300))
                {
                    uploadFileName = "?File Size is Too big! Maximum Allowed is 100kb...";
                }
                else
                {
                    // set the path and file
                    uploadFileName = getLocation() + fileType + "_" + System.IO.Path.GetRandomFileName() + ".jpg";
                    path = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/HRD/Logo/");
                    fullPath = Path.Combine(path, uploadFileName);

                    fileToUpload.SaveAs(fullPath);

                    // delete old file if exists
                    if (existingFileName.Trim().Length > 0)
                    {
                        fullPath = path + existingFileName;
                        FileInfo fi = new FileInfo(fullPath);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }
                    }
                }
            }


            if (fileType == "C") // Upload Crew Image
            {
                // Validate file size is not greater than 50 kb
                // Return ERR if filesize is greater than 50 kb else return file name

                if (fileToUpload.ContentLength > (1024 * 300))
                {
                    uploadFileName = "?File Size is Too big! Maximum Allowed is 300kb...";
                }
                else
                {
                    // set the path and file
                    uploadFileName = getLocation() + fileType + "_" + System.IO.Path.GetRandomFileName() + ".jpg";
                    path = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/HRD/CrewPhotos/");
                    fullPath = Path.Combine(path, uploadFileName);

                    fileToUpload.SaveAs(fullPath);

                    // delete old file if exists
                    if (existingFileName.Trim().Length > 0)
                    {
                        fullPath = path + existingFileName;
                        FileInfo fi = new FileInfo(fullPath);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }
                    }
                }
            }

            if (fileType == "T") // Upload Travel Documents
            {
                // Validate file size is not greater than 200 kb
                // Return ERR if filesize is greater than 200 kb else return file name

                if (fileToUpload.ContentLength > (1024 * 1024 * 10))
                {
                    uploadFileName = "?File Size is Too big! Maximum Allowed is 10 MB...";
                }
                else
                {
                    // set the path and file
                    uploadFileName = getLocation() + fileType + "_" + System.IO.Path.GetRandomFileName() + ".pdf";

                    path = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/HRD/Documents/Travel/");
                    fullPath = Path.Combine(path, uploadFileName);

                    fileToUpload.SaveAs(fullPath);

                    // delete old file if exists
                    if (existingFileName.Trim().Length > 0)
                    {
                        fullPath = path + existingFileName;
                        FileInfo fi = new FileInfo(fullPath);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }
                    }
                }
            }

            if (fileType == "P") // Upload Professional Documents
            {
                // Validate file size is not greater than 200 kb
                // Return ERR if filesize is greater than 200 kb else return file name

                if (fileToUpload.ContentLength > (1024 * 1024 * 10))
                {
                    uploadFileName = "?File Size is Too big! Maximum Allowed is 10 MB...";
                }
                else
                {

                    // set the path and file
                    uploadFileName = getLocation() + fileType + "_" + System.IO.Path.GetRandomFileName() + ".pdf";
                    //path = HttpContext.Current.Server.MapPath("UserUploadedDocuments/Documents/Professional/");
                    path = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/HRD/Documents/Professional/");
                    fullPath = Path.Combine(path, uploadFileName);

                    fileToUpload.SaveAs(fullPath);

                    // delete old file if exists
                    if (existingFileName.Trim().Length > 0)
                    {
                        fullPath = path + existingFileName;
                        FileInfo fi = new FileInfo(fullPath);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }
                    }
                }
            }

            if (fileType == "M") // Upload medical Documents
            {
                // Validate file size is not greater than 200 kb
                // Return ERR if filesize is greater than 200 kb else return file name

                if (fileToUpload.ContentLength > (1024 * 1024 * 10))
                {
                    uploadFileName = "?File Size is Too big! Maximum Allowed is 10 MB...";
                }
                else
                {

                    // set the path and file
                    uploadFileName = getLocation() + fileType + "_" + System.IO.Path.GetRandomFileName() + ".pdf";
                    path = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/HRD/Documents/Medical/");
                    fullPath = Path.Combine(path, uploadFileName);

                    fileToUpload.SaveAs(fullPath);

                    // delete old file if exists
                    if (existingFileName.Trim().Length > 0)
                    {
                        fullPath = path + existingFileName;
                        FileInfo fi = new FileInfo(fullPath);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }
                    }
                }
            }

            if (fileType == "A") // Upload Academic Documents
            {
                // Validate file size is not greater than 200 kb
                // Return ERR if filesize is greater than 200 kb else return file name

                if (fileToUpload.ContentLength > (1024 * 1024 * 3))
                {
                    uploadFileName = "?File Size is Too big! Maximum Allowed is 3 MB...";
                }
                else
                {

                    // set the path and file
                    uploadFileName = getLocation() + fileType + "_" + System.IO.Path.GetRandomFileName() + ".pdf";
                    path = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/HRD/Documents/Academic/");
                    fullPath = Path.Combine(path, uploadFileName);

                    fileToUpload.SaveAs(fullPath);

                    // delete old file if exists
                    if (existingFileName.Trim().Length > 0)
                    {
                        fullPath = path + existingFileName;
                        FileInfo fi = new FileInfo(fullPath);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }
                    }
                }
            }

            if (fileType == "H") // Upload CRM & HRD Documents
            {
                // Validate file size is not greater than 200 kb
                // Return ERR if filesize is greater than 200 kb else return file name

                if (fileToUpload.ContentLength > (1024 * 1024 * 3))
                {
                    uploadFileName = "?File Size is Too big! Maximum Allowed is 3 MB...";
                }
                else
                {

                    // set the path and file
                    uploadFileName = getLocation() + fileType + "_" + System.IO.Path.GetRandomFileName() + ".pdf";
                    path = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/HRD/Documents/CRM/");
                    fullPath = Path.Combine(path, uploadFileName);

                    fileToUpload.SaveAs(fullPath);

                    // delete old file if exists
                    if (existingFileName.Trim().Length > 0)
                    {
                        fullPath = path + existingFileName;
                        FileInfo fi = new FileInfo(fullPath);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }
                    }
                }
            }

            if (fileType == "R") // Upload Training Requirements
            {
                // Validate file size is not greater than 200 kb
                // Return ERR if filesize is greater than 200 kb else return file name

                if (fileToUpload.ContentLength > (1024 * 1024 * 3))
                {
                    uploadFileName = "?File Size is Too big! Maximum Allowed is 3 MB...";
                }
                else
                {

                    // set the path and file
                    uploadFileName = getLocation() + fileType + "_" + System.IO.Path.GetRandomFileName() + ".pdf";
                    path = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/HRD/Documents/Training/");
                    fullPath = Path.Combine(path, uploadFileName);

                    fileToUpload.SaveAs(fullPath);

                    // delete old file if exists
                    if (existingFileName.Trim().Length > 0)
                    {
                        fullPath = path + existingFileName;
                        FileInfo fi = new FileInfo(fullPath);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }
                    }
                }
            }

            if (fileType == "L") // Upload Appraisal Documents
            {
                // Validate file size is not greater than 200 kb
                // Return ERR if filesize is greater than 200 kb else return file name

                if (fileToUpload.ContentLength > (1024 * 1024 * 10))
                {
                    uploadFileName = "?File Size is Too big! Maximum Allowed is 10 MB...";
                }
                else
                {

                    // set the path and file
                    uploadFileName = getLocation() + fileType + "_" + System.IO.Path.GetRandomFileName() + ".pdf";
                    path = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/HRD/Documents/Appraisal/");
                    fullPath = Path.Combine(path, uploadFileName);

                    fileToUpload.SaveAs(fullPath);

                    // delete old file if exists
                    if (existingFileName.Trim().Length > 0)
                    {
                        fullPath = path + existingFileName;
                        FileInfo fi = new FileInfo(fullPath);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }
                    }
                }
            }

            if (fileType == "I") // Upload Invoice Documents
            {
                // Validate file size is not greater than 200 kb
                // Return ERR if filesize is greater than 200 kb else return file name

                if (fileToUpload.ContentLength > (1024 * 1024 * 3))
                {
                    uploadFileName = "?File Size is Too big! Maximum Allowed is 3 MB...";
                }
                else
                {

                    // set the path and file
                    uploadFileName = getLocation() + fileType + "_" + System.IO.Path.GetRandomFileName() + ".pdf";
                    path = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/Invoice/");
                    fullPath = Path.Combine(path, uploadFileName);

                    fileToUpload.SaveAs(fullPath);

                    // delete old file if exists
                    if (existingFileName.Trim().Length > 0)
                    {
                        fullPath = path + existingFileName;
                        FileInfo fi = new FileInfo(fullPath);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }
                    }
                }
            }

            if (fileType == "W") // Upload Invoice Documents
            {
                // Validate file size is not greater than 200 kb
                // Return ERR if filesize is greater than 200 kb else return file name

                if (fileToUpload.ContentLength > (1024 * 1024 * 3))
                {
                    uploadFileName = "?File Size is Too big! Maximum Allowed is 3 MB...";
                }
                else
                {

                    // set the path and file
                    uploadFileName = getLocation() + fileType + "_" + System.IO.Path.GetRandomFileName() + ".pdf";
                    path = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/HRD/WagesSheet/");
                    fullPath = Path.Combine(path, uploadFileName);

                    fileToUpload.SaveAs(fullPath);

                    // delete old file if exists
                    if (existingFileName.Trim().Length > 0)
                    {
                        fullPath = path + existingFileName;
                        FileInfo fi = new FileInfo(fullPath);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }
                    }
                }
            }
            return uploadFileName;
        }
        public string UploadFileToServer(HttpPostedFile fileToUpload, string uploadedFileName, string existingFileName, string fileType)
        {
            string text = "";
            if (fileType == "D")
            {
                if (fileToUpload.ContentLength > 10485760)
                {
                    text = "?File Size is Too big! Maximum Allowed is 10 MB...";
                }
                else
                {
                    text = "1984_" + fileType + "_" + Path.GetRandomFileName() + Path.GetExtension(uploadedFileName);
                    string text2 = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/Inspection/Inspection/");
                    string filename = Path.Combine(text2, text);
                    fileToUpload.SaveAs(filename);
                    if (existingFileName.Trim().Length > 0)
                    {
                        filename = text2 + existingFileName;
                        FileInfo fileInfo = new FileInfo(filename);
                        if (fileInfo.Exists)
                        {
                            fileInfo.Delete();
                        }
                    }
                }
            }

            if (fileType == "TR")
            {
                if (fileToUpload.ContentLength > 10485760)
                {
                    text = "?File Size is Too big! Maximum Allowed is 10 MB...";
                }
                else
                {
                    text = "1984_" + fileType + "_" + Path.GetRandomFileName() + Path.GetExtension(uploadedFileName);
                    string text2 = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/Inspection/Transaction_Reports/");
                    string filename = Path.Combine(text2, text);
                    fileToUpload.SaveAs(filename);
                    if (existingFileName.Trim().Length > 0)
                    {
                        filename = text2 + existingFileName;
                        FileInfo fileInfo2 = new FileInfo(filename);
                        if (fileInfo2.Exists)
                        {
                            fileInfo2.Delete();
                        }
                    }
                }
            }

            if (fileType == "XF")
            {
                text = "1984_" + fileType + "_" + Path.GetRandomFileName() + Path.GetExtension(uploadedFileName);
                string text2 = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/XML_Forms/");
                string filename = Path.Combine(text2, text);
                fileToUpload.SaveAs(filename);
                if (existingFileName.Trim().Length > 0)
                {
                    filename = text2 + existingFileName;
                    FileInfo fileInfo3 = new FileInfo(filename);
                    if (fileInfo3.Exists)
                    {
                        fileInfo3.Delete();
                    }
                }
            }

            if (fileType == "AT")
            {
                if (fileToUpload.ContentLength > 10485760)
                {
                    text = "?File Size is Too big! Maximum Allowed is 10 MB...";
                }
                else
                {
                    text = "1984_" + fileType + "_" + Path.GetRandomFileName() + Path.GetExtension(uploadedFileName);
                    string text2 = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/Accident_Tracker/");
                    string filename = Path.Combine(text2, text);
                    fileToUpload.SaveAs(filename);
                    if (existingFileName.Trim().Length > 0)
                    {
                        filename = text2 + existingFileName;
                        FileInfo fileInfo4 = new FileInfo(filename);
                        if (fileInfo4.Exists)
                        {
                            fileInfo4.Delete();
                        }
                    }
                }
            }

            if (fileType == "NCR")
            {
                if (fileToUpload.ContentLength > 10485760)
                {
                    text = "?File Size is Too big! Maximum Allowed is 10 MB...";
                }
                else
                {
                    text = "1984_" + fileType + "_" + Path.GetRandomFileName() + Path.GetExtension(uploadedFileName);
                    string text2 = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/NCR_Tracker/");
                    string filename = Path.Combine(text2, text);
                    fileToUpload.SaveAs(filename);
                    if (existingFileName.Trim().Length > 0)
                    {
                        filename = text2 + existingFileName;
                        FileInfo fileInfo5 = new FileInfo(filename);
                        if (fileInfo5.Exists)
                        {
                            fileInfo5.Delete();
                        }
                    }
                }
            }

            if (fileType == "COC")
            {
                if (fileToUpload.ContentLength > 10485760)
                {
                    text = "?File Size is Too big! Maximum Allowed is 10 MB...";
                }
                else
                {
                    text = "1984_" + fileType + "_" + Path.GetRandomFileName() + Path.GetExtension(uploadedFileName);
                    string text2 = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/COC_Tracker/");
                    string filename = Path.Combine(text2, text);
                    fileToUpload.SaveAs(filename);
                    if (existingFileName.Trim().Length > 0)
                    {
                        filename = text2 + existingFileName;
                        FileInfo fileInfo6 = new FileInfo(filename);
                        if (fileInfo6.Exists)
                        {
                            fileInfo6.Delete();
                        }
                    }
                }
            }

            if (fileType == "COCClosure")
            {
                if (fileToUpload.ContentLength > 10485760)
                {
                    text = "?File Size is Too big! Maximum Allowed is 10 MB...";
                }
                else
                {
                    text = "1984_" + fileType + "_" + Path.GetRandomFileName() + Path.GetExtension(uploadedFileName);
                    string text2 = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/COC_Closure/");
                    string filename = Path.Combine(text2, text);
                    fileToUpload.SaveAs(filename);
                    if (existingFileName.Trim().Length > 0)
                    {
                        filename = text2 + existingFileName;
                        FileInfo fileInfo7 = new FileInfo(filename);
                        if (fileInfo7.Exists)
                        {
                            fileInfo7.Delete();
                        }
                    }
                }
            }

            if (fileType == "FollowUpClosure")
            {
                if (fileToUpload.ContentLength > 10485760)
                {
                    text = "?File Size is Too big! Maximum Allowed is 10 MB...";
                }
                else
                {
                    text = "1984_" + fileType + "_" + Path.GetRandomFileName() + Path.GetExtension(uploadedFileName);
                    string text2 = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/Inspection/FollowUpClosure/");
                    string filename = Path.Combine(text2, text);
                    fileToUpload.SaveAs(filename);
                    if (existingFileName.Trim().Length > 0)
                    {
                        filename = text2 + existingFileName;
                        FileInfo fileInfo8 = new FileInfo(filename);
                        if (fileInfo8.Exists)
                        {
                            fileInfo8.Delete();
                        }
                    }
                }
            }

            if (fileType == "VC")
            {
                if (fileToUpload.ContentLength > 10485760)
                {
                    text = "?File Size is Too big! Maximum Allowed is 10 MB...";
                }
                else
                {
                    text = "1984_" + fileType + "_" + Path.GetRandomFileName() + Path.GetExtension(uploadedFileName);
                    string text2 = HttpContext.Current.Server.MapPath("~/EMANAGERBLOB/LPSQE/Vessel_Certificates/");
                    string filename = Path.Combine(text2, text);
                    fileToUpload.SaveAs(filename);
                    if (existingFileName.Trim().Length > 0)
                    {
                        filename = text2 + existingFileName;
                        FileInfo fileInfo9 = new FileInfo(filename);
                        if (fileInfo9.Exists)
                        {
                            fileInfo9.Delete();
                        }
                    }
                }
            }

            return text;
        }

        public string DownloadFileFromServer(string fileToDownload, string fileType)
        {
            // fileType C=Crew Image, T=Travel, P=Professional, A=Academic, M=Medical, H-CRM & HRD, R-Training Requirements, L-Appraisal
            string fullPath="";


            if (fileType == "Y") // Download Crew Image
            {
                // set the path and file
                fullPath = Path.Combine("~/EMANAGERBLOB/HRD/Logo/", fileToDownload);
            }
            
            if (fileType == "C") // Download Crew Image
            {
                // set the path and file
                fullPath = Path.Combine("~/EMANAGERBLOB/HRD/CrewPhotos/", fileToDownload);
            }

            if (fileType == "T") // Download Travel Documents
            {
                // set the path and file
                fullPath = Path.Combine("~/EMANAGERBLOB/HRD/Documents/Travel/", fileToDownload);
            }

            if (fileType == "P") // Download Professional Documents
            {
                // set the path and file
                fullPath = Path.Combine("~/EMANAGERBLOB/HRD/Documents/Professional/", fileToDownload);
            }

            if (fileType == "M") // Download Medical Documents
            {
                // set the path and file
                fullPath = Path.Combine("~/EMANAGERBLOB/HRD/Documents/Medical/", fileToDownload);
            }
            
            if (fileType == "A") // Download Academic Documents
            {
                // set the path and file
                fullPath = Path.Combine("~/EMANAGERBLOB/HRD/Documents/Academic/", fileToDownload);
            }

            if (fileType == "H") // Download CRM & HRD Documents
            {
                // set the path and file
                fullPath = Path.Combine("~/EMANAGERBLOB/HRD/Documents/CRM/", fileToDownload);
            }

            if (fileType == "R") // Download Training Requirements
            {
                // set the path and file
                fullPath = Path.Combine("~/EMANAGERBLOB/HRD/Documents/Training/", fileToDownload);
            }

            if (fileType == "L") // Download Appraisal Documents
            {
                // set the path and file
                fullPath = Path.Combine("~/EMANAGERBLOB/HRD/Documents/Appraisal/", fileToDownload);
            }

            if (fileType == "I") // Download Appraisal Documents
            {
                // set the path and file
                fullPath = Path.Combine("~/EMANAGERBLOB/Invoice/", fileToDownload);
            }

            if (fileType == "W") // Download Appraisal Documents
            {
                // set the path and file
                fullPath = Path.Combine("~/EMANAGERBLOB/HRD/WagesSheet/", fileToDownload);
            }

            //if (!(File.Exists(HttpContext.Current.Server.MapPath("../"+fullPath.Substring(2)))))
            //{
            //   fullPath = Path.Combine("~/UserUploadedDocuments/CrewPhotos/", "noimage.jpg");
            //}
            
            return fullPath;
        }
    }
}
