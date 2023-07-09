using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BAL.FileManager
{
    public class FileManagerService
    {

        public static async Task<string> UploadFile(Stream file, string filePath, bool deleteExistingFile = false)
        {
            try
            {
                string imgUrl = filePath.Replace("\\", "/");
                filePath = Constants.WebRoot + filePath;
                string directory = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                if (deleteExistingFile && File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                await using FileStream fs = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fs);
                return imgUrl;
            }
            catch (Exception exc)
            {
                string errorMessage = exc.Message;
            }
            await Task.Delay(10);
            return "";
        }

        public static async Task<string> MoveFile(string sourceFile, string destinationFile)
        {
            try
            {
                string sourceImgUrl = sourceFile.Replace("\\", "/");
                string destImgUrl = destinationFile.Replace("\\", "/");
                sourceFile = Constants.WebRoot + sourceFile;
                destinationFile = Constants.WebRoot + destinationFile;

                if (File.Exists(sourceFile))
                {
                    string destinationDir = Path.GetDirectoryName(destinationFile);

                    Directory.CreateDirectory(destinationDir);

                    File.Move(sourceFile, destinationFile, true);
                    return destImgUrl;
                }
                return sourceImgUrl;
            }
            catch (Exception exc)
            {
                string errorMessage = exc.Message;
            }
            await Task.Delay(10);
            return "";
        }

        public static void DeletFile(string existingFile)
        {
            existingFile = Constants.WebRoot + existingFile.Replace("/", "\\");
            if (File.Exists(existingFile) == true)
            {
                File.Delete(existingFile);
            }
        }
    }
}
