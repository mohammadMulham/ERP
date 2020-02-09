using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Extentions
{
    public static class ModelStateDictionaryExtend
    {
        /// <summary>
        /// AddModelError with Errors key and errors messages 
        /// </summary>
        /// <param name="modelStateDictionary"></param>
        /// <returns>ModelStateDictionary</returns>
        public static ModelStateDictionary GetWithErrorsKey(this ModelStateDictionary modelStateDictionary)
        {
            var errors = new List<string>();
            foreach (var item in modelStateDictionary)
            {
                foreach (var error in item.Value.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }
            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError("errors", error);
            }
            return modelStateDictionary;
        }
    }
}
