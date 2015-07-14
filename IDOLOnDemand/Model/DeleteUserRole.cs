using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IDOLOnDemand.Response;
using IDOLOnDemand.Exceptions;
using IDOLOnDemand.Helpers;


namespace IDOLOnDemand.Model
{
    public class DeleteUserRole
    {
        private readonly IdolConnect _idolConnection;
        public string SyncEndpoint = "/sync/deleterole/v1";
        public string AsyncEndpoint = "/async/deleterole/v1";

        public string Store { get; set; }
        public string Role { get; set; }

        public DeleteUserRole(IdolConnect idolConnection)
        {
            _idolConnection = idolConnection;
        }

        public DeleteUserRoleResponse.Value Execute()
        {
            var apiResults = _idolConnection.Connect(this, SyncEndpoint);
            var deseriaizedResponse = JsonConvert.DeserializeObject<DeleteUserRoleResponse.Value>(apiResults);

            if (deseriaizedResponse.status != "failed" & deseriaizedResponse.status != null)
            {
                return deseriaizedResponse;
            }
            else
            {
                if (deseriaizedResponse.error.Equals(4005))
                {
                    throw new APIFailedException(deseriaizedResponse.detail[0]);
                }
                else
                {
                    throw new InvalidJobArgumentsException(deseriaizedResponse.actions[0].errors[0].reason);
                }
            }

        }
    }


}
