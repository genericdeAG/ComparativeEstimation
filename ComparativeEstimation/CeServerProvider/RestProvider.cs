using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit.Sdk;

namespace CeServerProvider
{
    public class RestProvider: ICes
    {
        private HttpClient _httpClient;

        public RestProvider(string serverAdresse)
        {
            var uri = new Uri(serverAdresse);

            _httpClient = new HttpClient
            {
                BaseAddress = uri
            };

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Âmelde(string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"anmeldungen/{id}");
            var response = _httpClient.SendAsync(request).Result;

            if(response.StatusCode != HttpStatusCode.OK) throw new Exception();
        }

        public void Gewichtung_regischtriere(IEnumerable<GewichtetesVergleichspaarDto> voting, Action ok, Action fehler)
        {
            var jsonVoting = JsonConvert.SerializeObject(voting);
            var content = new StringContent(jsonVoting, Encoding.Unicode, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, "voting") {Content = content};

            var result = _httpClient.SendAsync(request).Result;

            if (result.IsSuccessStatusCode)
            {
                ok?.Invoke();
            }
            else
            {
                fehler?.Invoke();
            }

        }

        public GesamtgewichtungDto Gesamtgewichtung
        {
            get
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "gesamtgewichtung");
                var response = _httpClient.SendAsync(request).Result;
                var content =  response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode != HttpStatusCode.OK) throw new Exception();

                return JsonConvert.DeserializeObject<GesamtgewichtungDto>(content);

            }
        }

        public IEnumerable<VergleichspaarDto> Vergleichspaare
        {
            get
            {
                //TODO: in methode auslagern
                var request = new HttpRequestMessage(HttpMethod.Get, "vergleichspaare");
                var response = _httpClient.SendAsync(request).Result;
                var content = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode != HttpStatusCode.OK) throw new Exception();

                return JsonConvert.DeserializeObject<IEnumerable<VergleichspaarDto>>(content);
            }
        }
        public void Sprint_âlege(IEnumerable<string> stories)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "sprint");
            var jsonContent = JsonConvert.SerializeObject(stories);
            request.Content = new StringContent(jsonContent, Encoding.Unicode, "application/json");
            var response = _httpClient.SendAsync(request).Result;

            if (response.StatusCode != HttpStatusCode.OK) throw new Exception();
        }

        public void Sprint_lösche()
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "sprint");
            var response = _httpClient.SendAsync(request).Result;

            if (response.StatusCode != HttpStatusCode.OK) throw new Exception();
        }
    }
}
