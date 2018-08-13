using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JournalResearcher.Logic.Service
{
    public interface IUploadService
    {
        Task<string> FileUploader(IFormFile file, string uploadFolderPath);
    }

    public class UploadService : IUploadService
    {
        public async Task<string> FileUploader(IFormFile file, string uploadFolderPath)
        {
            int maxContent = 5 * 1024 * 1024; //5 MB

            if (file == null || file.Length == 0)
                throw new ArgumentNullException(string.Format("{0}", "File Cannot Be Empty"));
            //Create Folder If Not Exist

            if (!Directory.Exists(uploadFolderPath)) Directory.CreateDirectory(uploadFolderPath);

            //Check File size maybe is not too large
            if (file.Length > maxContent)
                throw new ApplicationException(string.Format("{0}", "Maximum upload file allowed is " + (maxContent / 1024).ToString()));
            //Check if extension is valid:: which are pdf and docx or doc;


            string[] allowedExtensions = { "pdf", "docx", "doc" };
            var extension = Path.GetExtension(file.FileName).ToLower().Substring(1);
            if (!allowedExtensions.Contains(extension))
                throw new ApplicationException("Invalid File Format");
            var filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, filename);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }


            return filename;
        }
    }
}