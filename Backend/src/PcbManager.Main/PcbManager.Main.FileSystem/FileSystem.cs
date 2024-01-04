using PcbManager.Main.App.Abstractions;

namespace PcbManager.Main.FileSystemNS
{
    public class FileSystem : IFileSystem
    {
        private readonly string _fileSystemPath = "C:/Users/Danil/Desktop/FileStorage";

        public void CreateFolder(string path, string folderName) =>
            Directory.CreateDirectory($"{_fileSystemPath}/{path}/{folderName}");

        public void DeleteFile(string path, string fileName) =>
            File.Delete($"{_fileSystemPath}/{path}/{fileName}");

        public void DeleteFolder(string path, string folderName) =>
            Directory.Delete($"{_fileSystemPath}/{path}/{folderName}", true);

        public FileStream GetFileSteram(string path, string fileName) =>
            File.Open($"{_fileSystemPath}/{path}/{fileName}", FileMode.Open);

        public async Task SaveFileAsync(string path, string fileName, Stream fileStream)
        {
            await using var stream = File.Create($"{_fileSystemPath}/{path}/{fileName}");
            await fileStream.CopyToAsync(stream);
        }
    }
}
