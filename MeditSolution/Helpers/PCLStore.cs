using System;
using System.Threading.Tasks;
using PCLStorage;

namespace MeditSolution.Helpers
{
    public class PCLStore
    {
        public static async Task<string> WriteFileAsync(byte[] content, string fileExtension, string documentName)
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            var specificFileType = "myFile";

            var fileName = string.Concat(specificFileType, "_", documentName, fileExtension);

            IFolder subFolder = null;
            var subFolderExists = await rootFolder.CheckExistsAsync("MyAppResources");

            if (subFolderExists == ExistenceCheckResult.NotFound)
            {
                subFolder = await rootFolder.CreateFolderAsync("MyAppResources", CreationCollisionOption.ReplaceExisting);
            }
            else
            {
                subFolder = await rootFolder.GetFolderAsync("MyAppResources");
            }

            var fileExists = await subFolder.CheckExistsAsync(fileName);

            if (fileExists == ExistenceCheckResult.NotFound)
            {
                var file = await subFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                var stream = await file.OpenAsync(FileAccess.ReadAndWrite);

                stream.Write(content, 0, content.Length);

                return file.Path;
            }

            var filePath = subFolder.Path + "/" + fileName;

            return filePath;
        }

        public static async Task DeleteEverything()
        {
            var rootFolder = FileSystem.Current.LocalStorage;          

            IFolder subFolder = null;

            var subFolderExists = await rootFolder.CheckExistsAsync("MyAppResources");

            if (subFolderExists == ExistenceCheckResult.NotFound)
            {
                return;
            }
            else
            {
                subFolder = await rootFolder.GetFolderAsync("MyAppResources");
                await subFolder.DeleteAsync();
                return;
            }
        }

        public static async Task<string> CheckFileExists(string fileExtension, string documentName)
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            var specificFileType = "myFile";

            var fileName = string.Concat(specificFileType, "_", documentName, fileExtension);

            IFolder subFolder = null;
           
            var subFolderExists = await rootFolder.CheckExistsAsync("MyAppResources");

            if (subFolderExists == ExistenceCheckResult.NotFound)
            {
                return null;
            }
            else
            {
                subFolder = await rootFolder.GetFolderAsync("MyAppResources");
            }

            var fileExists = await subFolder.CheckExistsAsync(fileName);

            if (fileExists == ExistenceCheckResult.NotFound)
            {
                return null;
            }
            else
            {
                var file = await subFolder.GetFileAsync(fileName);

                if (file != null && !string.IsNullOrEmpty(file.Path))
                    return file.Path;
                else
                    return null;
            }
        }
    }
}
