using Microsoft.AspNetCore.WebUtilities;

namespace pfs.Interfaces
{
    public interface IStreamFileUploadService
    {
        Task<bool> UploadFile(MultipartReader reader, MultipartSection section);
    }
}
