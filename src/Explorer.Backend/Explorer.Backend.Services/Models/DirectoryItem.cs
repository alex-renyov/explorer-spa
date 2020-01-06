using System;

namespace Explorer.Backend.Services.Models
{
    public sealed class DirectoryItem
    {
        public DirectoryItem(string name, string parentPath, DateTime created, DateTime modified)
        {
            Name = name;
            ParentPath = parentPath;
            Created = created;
            Modified = modified;
        }

        public string Name { get; }
        
        public string ParentPath { get; }
        
        public DateTime Created { get; }
        
        public DateTime Modified { get; }
    }
}