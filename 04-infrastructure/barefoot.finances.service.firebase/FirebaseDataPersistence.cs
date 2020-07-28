using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using barefoot.finances.service.core.interfaces;
using barefoot.finances.service.models;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Newtonsoft.Json;

namespace barefoot.finances.service.firebase
{
    public class FirebaseDataPersistence : IDataPersistance
    {
        private readonly IFirebaseClient _client;

        public FirebaseDataPersistence()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                BasePath = ""
            };
            _client = new FirebaseClient(config);
        }

        public async Task<HouseholdInfo> GetHouseholdInfoAsync(string userId)
        {
            var response = await _client.GetAsync("householdinfo");
            var responseDict = JsonConvert.DeserializeObject<Dictionary<string, HouseholdInfo>>(response.Body);
            
            HouseholdInfo result = responseDict.SingleOrDefault(x => x.Key == userId).Value;

            return result;
        }

        public async Task<bool> SaveHouseholdInfoAsync(HouseholdInfo info)
        {
            var response = await _client.PushAsync("householdinfo", info);
            if(response.StatusCode == HttpStatusCode.OK)
                return true;

            return false;
        }
    }
}
