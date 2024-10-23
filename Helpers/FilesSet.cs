using Microsoft.SqlServer.Server;
using System.IO;
using System;

namespace Re.PL.Helpers
{
    public class FilesSet
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
            {
                return null; // Return null if the file is empty or not provided
            }

            try
            {
                // Ensure folder path exists
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath); // Create the directory if it doesn't exist
                }

                // Sanitize file name and add a GUID for uniqueness
                string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";

                // Ensure the full path for the file
                string filePath = Path.Combine(folderPath, fileName);

                // Use "using" to properly dispose of the file stream
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                return fileName; // Return the file name for saving to the database
            }
            catch (Exception ex)
            {
                // Log the exception (or handle it in an appropriate way)
                // Return null to indicate failure
                return null;
            }
        }

        public static void DeleteFile(string fileName, string folderName) {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);

            }
        }
    }
}

