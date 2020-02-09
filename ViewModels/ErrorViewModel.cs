using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.ViewModels
{
    public class ErrorViewModel
    {
        public ErrorViewModel(ErrorType type, string key, string message)
        {
            Type = type;
            Key = key;
            Message = message;
        }

        public ErrorViewModel(ErrorType type, string message)
        {
            Type = type;
            Key = "";
            Message = message;
        }

        public ErrorType Type { get; set; }
        public string Key { get; set; }
        public string Message { get; set; }
    }

    public enum ErrorType
    {
        NotFound,
        BadRequest
    }
}
