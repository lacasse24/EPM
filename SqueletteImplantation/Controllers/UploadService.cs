using Microsoft.AspNetCore.Http;


namespace SqueletteImplantation.Controllers
{
    public interface UploadService
    {
        bool upload(IFormFile formFile, string chemin);
    }
}
