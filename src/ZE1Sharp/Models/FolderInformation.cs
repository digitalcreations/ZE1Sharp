namespace ZE1Sharp.Models
{
    using System.Collections.Generic;

    public class FolderInformation : Response
    {
        public ICollection<string> Files { get; set; }
    }
}