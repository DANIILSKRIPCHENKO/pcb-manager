namespace PcbManager.Main.App.Abstractions
{
    public interface IFileSystem
    {
        public void CreateFolder(string path, string folderName);

        public void DeleteFolder(string path, string folderName);

        public Task SaveFileAsync(string path, string fileName, Stream fileStream);

        public void DeleteFile(string path, string fileName);

        public FileStream GetFileSteram(string path, string fileName);
    }
}
