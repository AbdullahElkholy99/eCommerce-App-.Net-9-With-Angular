using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;


namespace Ecom.Infrastructure.Repositories.Service;

public class ImageManagementService : IImageManagementService
{
    private readonly IFileProvider _fileProvider;
    public ImageManagementService(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }
    //---------------- Upload Image ----------------
    public async Task<List<string>> UploadImageAsync(IFormFileCollection files, string src)
    {
        var saveImageSrc = new List<string>();

        var imageDirectory = Path.Combine("wwwroot", "Images",src);

        if (!Directory.Exists(imageDirectory))
            Directory.CreateDirectory(imageDirectory);

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                var imageName = file.FileName;

                var imageSrc = $"Images/{src}/{imageName}";

                var root =  Path.Combine(imageDirectory, imageName);

                using (var stream = new FileStream(root, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                saveImageSrc.Add(imageSrc);
            }

        }

        return  saveImageSrc ;
    }

    //---------------- Delete Image ----------------
    public void DeleteImageAsync(string src)
    {
        var info = _fileProvider.GetFileInfo(src);

        if (info.Exists)
            File.Delete(info.PhysicalPath);

    }
}
