using Microsoft.AspNetCore.Http;

namespace Ecom.Core.Services;

public interface IImageManagementService
{
    //---------------- Upload Image ----------------
    Task<List<string>> UploadImageAsync(IFormFileCollection files , string src);

    //---------------- Delete Image ----------------
    void DeleteImageAsync(string src);

}
