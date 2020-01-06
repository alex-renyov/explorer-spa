using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Explorer.Backend.Services.Models;

namespace Explorer.Backend.Services
{
    public sealed class ExplorerService : IExplorerService
    {
        private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        
        public async Task<PagedResult<DirectoryItem>> GetDirectories(string path, int fromIndex, int count)
        {
            if (path == "/" && IsWindows)
            {
                var drives = DriveInfo.GetDrives().Where(d => d.IsReady && d.DriveType == DriveType.Fixed);
                var result = drives.Select(d => "/" + d.Name.Substring(0, 1).ToLowerInvariant() + "/").ToArray();
                return new PagedResult<DirectoryItem>(
                    result.Skip(fromIndex).Take(count).Select(s => new DirectoryItem(s, "/", DateTime.UnixEpoch, DateTime.UnixEpoch)).ToArray(),
                    result.Length
                    );
            }

            var translatedPath = TranslatePath(path);
            var directories = Directory.GetDirectories(translatedPath);
            var total = directories.Length;
            
            return new PagedResult<DirectoryItem>(
                directories.Skip(fromIndex).Take(count).Select(s => GetDirectoryInfo(path, s)).ToArray(),
                total
                );
        }

        public async Task<PagedResult<FileItem>> GetFiles(string path, int fromIndex, int count)
        {
            if (path == "/" && IsWindows)
            {
                return new PagedResult<FileItem>(); 
            }

            var translatedPath = TranslatePath(path);

            var files = Directory.GetFiles(translatedPath);
            var total = files.Length;
            
            return new PagedResult<FileItem>(files.Skip(fromIndex).Take(count).Select(f => GetFileInfo(path, f)).ToArray(), total); 
        }

        /// <summary>
        /// Leaves path as is for unix, converts windows path to unix-like, e.g. /c/windows/system32
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string TranslatePath(string path)
        {
            if (!IsWindows)
            {
                return path;
            }

            var drive = path.Substring(1, 1);
            var driveString = "/" + drive + "/";

            if (path.Substring(0, 3) != driveString)
            {
                throw new InvalidOperationException("Invalid windows path");
            }

            return drive + ":/" + path.Substring(3);
        }

        private static FileItem GetFileInfo(string path, string name)
        {
            var fileInfo = new FileInfo(name);
            var localName = Path.GetFileName(name);
            return new FileItem(
                localName,
                path,
                fileInfo.Length,
                fileInfo.CreationTime,
                fileInfo.LastWriteTime
                );
        }

        private static DirectoryItem GetDirectoryInfo(string path, string name)
        {
            var directoryInfo = new DirectoryInfo(name);
            var localName = Path.GetFileName(name);
            
            return new DirectoryItem(
                localName,
                path,
                directoryInfo.CreationTime,
                directoryInfo.LastWriteTime
                );
        }
    }
}