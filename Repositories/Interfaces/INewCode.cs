using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Repositories
{
    public interface INewCode
    {
        /// <summary>
        /// Generate new code for this entity
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="BoxesNumber"></param>
        /// <param name="FirstCode"></param>
        /// <returns></returns>
        string GetNewCode(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0');
        /// <summary>
        /// Generate new code for this entity
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="BoxesNumber"></param>
        /// <param name="FirstCode"></param>
        /// <returns></returns>
        Task<string> GetNewCodeAsync(Guid? ParentId = null, int BoxesNumber = 2, char FirstCode = '0');
    }
}
