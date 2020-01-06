using System;
using System.IO;

namespace Explorer.Backend.Services.Models
{
    public sealed class FileItem
    {
        public FileItem(string name, string parentPath, long size, DateTime created, DateTime modified)
        {
            Name = name;
            ParentPath = parentPath;
            Size = size;
            Created = created;
            Modified = modified;

            NameWithoutExtension = Path.GetFileNameWithoutExtension(name);
            Extension = Path.GetExtension(name);
        }

        public string Name { get; }
        
        public string ParentPath { get; }
        
        public long Size { get; }
        
        public DateTime Created { get; }
        
        public DateTime Modified { get; }

        public string NameWithoutExtension { get; }
        
        public string Extension { get; }
    }
}