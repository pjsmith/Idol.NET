using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IDOLOnDemand.Exceptions;
using IDOLOnDemand.Helpers;
using IDOLOnDemand.Response;



namespace IDOLOnDemand.Model
{
    public class ListUserStores
    {
        private readonly IdolConnect _idolConnection;

        public string SyncEndpoint = "/sync/liststores/v1";
        public string AsyncEndpoint = "/async/liststores/v1";

        public ListUserStores(IdolConnect idolConnection)
        {
            _idolConnection = idolConnection;
        }

        public ListUserStoresResponse.Value Execute()
        {
            var apiResults = _idolConnection.Connect(this, SyncEndpoint);
            var deseriaizedResponse = JsonConvert.DeserializeObject<ListUserStoresResponse.Value>(apiResults);

            if (deseriaizedResponse.message == null & deseriaizedResponse.detail == null)
            {
                return deseriaizedResponse;
            }
            else
            {
                if (deseriaizedResponse.message != null)
                {
                    throw new APIFailedException(deseriaizedResponse.message);
                }
                else
                {
                    throw new InvalidJobArgumentsException(deseriaizedResponse.detail[0]);
                }
            }

        }
    }


}
