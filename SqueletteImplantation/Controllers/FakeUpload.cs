
using Microsoft.AspNetCore.Http;

namespace SqueletteImplantation.Controllers
{
    public class FakeUpload : UploadService
    {
        public bool upload(IFormFile formFile, string chemin)
        {
            return true;
        }
    }
}
