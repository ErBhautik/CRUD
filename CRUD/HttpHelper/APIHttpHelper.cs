using System;
using System.Net.Http;
using System.Text.Json;

namespace CRUD.HttpHelper
{
    public class APIHttpHelper : IAPIHttpHelper
    {
        public APIResponse<TResult> GetRequest<TResult>(string uri)
        {
			APIResponse<TResult> apiResponse = new();
			try
			{
				using HttpClient client = new();
				var response = client.GetAsync(uri).Result;
				var readTask = response.Content.ReadAsStringAsync();
				readTask.Wait();
				if (response.IsSuccessStatusCode)
				{
					apiResponse.StatusCode = (int)response.StatusCode;
					try
					{
						var options = new JsonSerializerOptions();
						options.PropertyNameCaseInsensitive = true;
						apiResponse.RawResult = readTask.Result;
						apiResponse.Result = JsonSerializer.Deserialize<TResult>(readTask.Result, options);
					}
					catch (Exception)
					{
						apiResponse.Result = default;
					}
					return apiResponse;
				}
				else
				{
					apiResponse.StatusCode = (int)response.StatusCode;
					apiResponse.Result = default;
					apiResponse.RawResult = readTask.Result;
					apiResponse.ErrorMessage += $"{response.StatusCode}:{readTask.Result}";
					return apiResponse;
				}
			}
			catch (Exception ex)
			{
				apiResponse.Result = default;
				apiResponse.ErrorMessage += ex.Message;
				return apiResponse;
			}
		}
    }
}
