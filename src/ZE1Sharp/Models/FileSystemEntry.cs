namespace ZE1Sharp.Models
{
    public class FileSystemEntry
    {
        public FileSystemEntry(string path)
        {
            this.Path = path;
        }

        internal FileSystemEntry(string path, string name)
        {
            if (!path.EndsWith("/"))
            {
                path += "/";
            }
            this.Path = path + name;
        }

        public string Path { get; private set; }

        public string FileName => System.IO.Path.GetFileName(this.Path);
    }
}