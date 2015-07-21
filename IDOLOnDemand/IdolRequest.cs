using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDOLOnDemand
{
    public abstract class IdolRequest : IIdolRequest
    {
        public virtual Dictionary<string, string> ToParameterDictionary()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            foreach (var item in this.GetType().GetProperties())
            {
                if (item.GetValue(this, null) != null)
                {
                    parameters.Add(item.Name, item.GetValue(this, null).ToString());
                }
            }

            return parameters;
        }
    }
}
