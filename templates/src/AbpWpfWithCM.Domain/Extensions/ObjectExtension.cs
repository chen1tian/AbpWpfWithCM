using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbpWpfWithCM.Domain.Extensions
{
    /// <summary>
    /// 返回对象的Description标注数据
    /// </summary>
    public static class ObjectExtension
    {
        public static string GetDescription(this object obj)
        {
            if (obj == null)
                return null;

            var member = obj.GetType().GetMember(obj.ToString());
            if (member != null && member.Length > 0)
            {
                var desc = member[0].GetCustomAttributes().OfType<DescriptionAttribute>().FirstOrDefault();
                if (desc != null)
                {
                    return desc.Description;
                }
            }

            return obj.ToString();
        }
    }
}
