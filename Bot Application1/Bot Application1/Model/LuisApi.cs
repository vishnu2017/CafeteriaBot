﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Bot_Application1.Security;

namespace Bot_Application1.Model
{
    [Serializable]
    public static class LuisApi
    {
        public static async Task<LuisResult> GetLuisResult(string query)
        {
            LuisResult luisResponse;

            string modelId = Credentials.LUIS_MODEL_ID;
            string subscriptionKey = Credentials.LUIS_SUBSCRIPTION_KEY;

            string luisUrl = $"https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/{modelId}?subscription-key={subscriptionKey}&verbose=true&q={HttpUtility.HtmlEncode(query)}";
            
            // Create a request for the URL.   
            WebRequest request = WebRequest.Create(luisUrl);

            // Get the response.  
            WebResponse response = await request.GetResponseAsync();

            // Get the stream containing content returned by the server.  
            Stream dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);

            // Read the content.  
            var responseFromServer = reader.ReadToEnd();
            luisResponse = JsonConvert.DeserializeObject<LuisResult>(responseFromServer);

            // Clean up the streams and the response.  
            reader.Close();
            response.Close();

            // Display the content.  
            return luisResponse;
        }
    }
}
