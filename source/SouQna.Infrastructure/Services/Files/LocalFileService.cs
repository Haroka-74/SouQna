using Microsoft.AspNetCore.Hosting;
using SouQna.Application.Interfaces;

namespace SouQna.Infrastructure.Services.Files
{
    public class LocalFileService(IWebHostEnvironment environment) : IFileService
    {
        public async Task<string> SaveFileAsync(
            Stream fileStream,
            string fileName,
            string folderName
        )
        {
            var rootPath = environment.WebRootPath;
            var relativeFolder = Path.Combine("Images", folderName);
            var absoluteFolder = Path.Combine(rootPath, relativeFolder);

            if(!Directory.Exists(absoluteFolder))
                Directory.CreateDirectory(absoluteFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
            var absoluteFilePath = Path.Combine(absoluteFolder, uniqueFileName);

            using var fs = new FileStream(absoluteFilePath, FileMode.Create);
            await fileStream.CopyToAsync(fs);

            return $"/{relativeFolder.Replace("\\", "/")}/{uniqueFileName}";
        }
    }
}