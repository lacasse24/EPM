using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace SqueletteImplantation.Controllers
{
    public class RealUpload : UploadService
    {
        public static string Chemin = "/Upload/";

        public bool upload(IFormFile formFile, string chemin)
        {
            string CheminApp = "/home/ubuntu/EPM/implantation-a17-epm/SqueletteImplantation/wwwroot" ;
            try
            {              
                using (FileStream upload = new FileStream(CheminApp + chemin, FileMode.CreateNew))
                {
                    formFile.CopyTo(upload);
                }
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
