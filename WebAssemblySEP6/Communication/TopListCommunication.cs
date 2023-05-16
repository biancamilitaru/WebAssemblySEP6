using Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Model.HttpResponse;

namespace WebAssemblySEP6.Communication
{

    public class TopListCommunication : ITopListCommunication
    {
        private HttpClient httpClient;

        public TopListCommunication()
        {
            httpClient = new HttpClient();
            //do the connection to the backend
        }

        public IList<TopList> GetTopListsAsync()
        {
            //database call
            //we need to get the userId from the logged in user
            //--------------This is just for testing----------------------
            IList<TopList> topLists = new List<TopList>();
            topLists.Add(new TopList{UserName = "hahi", Title="Romance", Id=1});
            topLists.Add(new TopList{UserName = "hahi", Title="Thriller", Id=2});

            return topLists;

        }
    }
}