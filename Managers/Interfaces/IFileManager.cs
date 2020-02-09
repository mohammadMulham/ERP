using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Managers
{
    public interface IFileManager
    {
        string ReadFile(string relativePath);
        void WriteFile(string relativePath, string content);
    }
}
