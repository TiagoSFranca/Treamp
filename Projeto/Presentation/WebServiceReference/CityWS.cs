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
    public class CityWS
    {
        private const string url = "http://ws-cities.apphb.com/api/city/";
        public static async Task<List<CityViewItem>> GetCities(int idState)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url+"?state="+idState);
            HttpResponseMessage response = await httpClient.SendAsync(request);
            string dados = await response.Content.ReadAsStringAsync();
            List<CityViewItem> obj = JsonConvert.DeserializeObject<List<CityViewItem>>(dados);
            return obj;
        }
        public static async Task<CityViewModel> GetCity(int idCIty)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url + idCIty);
            HttpResponseMessage response = await httpClient.SendAsync(request);
            string dados = await response.Content.ReadAsStringAsync();
            CityViewModel obj = JsonConvert.DeserializeObject<CityViewModel>(dados);
            return obj;
        }
    }
}