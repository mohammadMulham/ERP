using ERPAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace ERPAPI.Extentions
{
    public static class EnumExtentions
    {
        public static string ToLocalizedString(this Enum en)
        {
            Type type = en.GetType();
            TypeInfo typeInfo = type.GetTypeInfo();

            EnumResouceAttribute attribute = typeInfo.GetCustomAttribute<EnumResouceAttribute>();

            if (attribute != null)
            {
                ResourceManager manager = new ResourceManager(attribute.ResouceType);
                return manager.GetString(en.ToString());
            }

            return en.ToString();
        }
    }
}
