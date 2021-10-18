using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace ApiTests
{
    [TestFixture]
    public static class ApiTests
    {
        [Test]
        public static void test()
        {
            var restClient = new RestClient();
            var resRequest = new RestRequest("https://api.coinpaprika.com/v1/coins");

            IRestResponse<List<Coooin>> response = restClient.Get<List<Coooin>>(resRequest);
            var actives = response.Data.Where(a => a.is_active == true);



        }


        [Test]
        public static void test2()
        {
            var restClient = new RestClient();
            var resRequest = new RestRequest("https://api.coinpaprika.com/v1/coins/btc-bitcoin/exchanges");

            IRestResponse<List<Root>> response = restClient.Get<List<Root>>(resRequest);

            var a = response.Data[0].fiats[1].name;
        }


    //B3_1
    //B3_2
    }
}