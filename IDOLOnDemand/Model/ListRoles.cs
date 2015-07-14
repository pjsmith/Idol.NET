using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDOLOnDemand.Helpers;
using IDOLOnDemand.Exceptions;
using IDOLOnDemand.Response;
using Newtonsoft.Json;


namespace IDOLOnDemand.Model
{
    public class ListRoles
    {
        private readonly IdolConnect _idolConnection;

        public string SyncEndpoint = "/sync/listroles/v1";
        public string AsyncEndpoint = "/async/listroles/v1";

        public string Store { get; set; }

        public ListRoles(IdolConnect idolConnection)
        {
            _idolConnection = idolConnection;
        }

        public ListRolesResponse.Value Execute()
        {
            var apiResults = _idolConnection.Connect(this, SyncEndpoint);
            var deseriaizedResponse = JsonConvert.DeserializeObject<ListRolesResponse.Value>(apiResults);

            if (deseriaizedResponse.message == null & deseriaizedResponse.detail == null)
            {
                return deseriaizedResponse;
            }
            else
            {
                throw new InvalidJobArgumentsException(deseriaizedResponse.detail[0]);
            }
        }
    }

}
