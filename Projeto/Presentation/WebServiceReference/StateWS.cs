using Newtonsoft.Json;
using Presentation.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Presentation.WebServiceReference
{
    public class StateWS
    {
        public static async Task<List<StateViewItem>> GetStates()
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://ws-cities.apphb.com/api/state/");
            HttpResponseMessage response = await httpClient.SendAsync(request);
            string dados = await response.Content.ReadAsStringAsync();
            List<StateViewItem> obj = JsonConvert.DeserializeObject<List<StateViewItem>>(dados);
            return obj;
        }
    }
}