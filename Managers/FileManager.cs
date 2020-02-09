using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Managers
{
    public class FileManager : IFileManager
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public FileManager(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string ReadFile(string relativePath)
        {
            var filePath = _hostingEnvironment.ContentRootPath + "\\" + relativePath;
            var stream = File.OpenText(filePath);
            string content = stream.ReadToEnd();
            return content;
        }

        public void WriteFile(string relativePath, string content)
        {
            var filePath = _hostingEnvironment.ContentRootPath + "\\" + relativePath;
            File.WriteAllText(filePath, content);
        }
    }
}
