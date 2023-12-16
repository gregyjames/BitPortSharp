using Microsoft.Extensions.Logging;

namespace BitPortLibrary;

public static class Transfers
{
    public static async Task<bool> AddMagnet(this BitPortClient client, string magnet)
    {
        var request_url = URLs.method_call_base + $"/v2/transfers";
        client._logger.LogInformation(request_url);
        var formData = new Dictionary<string, string>
        {
            { "torrent", magnet}
        };
        var content = new FormUrlEncodedContent(formData);
        var responseMessage = await client._client.PostAsync(request_url, content);
        var response = await responseMessage.Content.ReadAsStringAsync();
        client._logger.LogTrace(response);
        if (responseMessage.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    public static async Task<string> GetTransfers(this BitPortClient client)
    {
        var request_url = URLs.method_call_base + $"/v2/transfers";
        client._logger.LogInformation(request_url);
        var response = await client._client.GetAsync(request_url);
        var str = await response.Content.ReadAsStringAsync();
        client._logger.LogTrace(str);
        return str;
    }
}