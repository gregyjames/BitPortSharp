using System.Text.Json;
using BitPortLibrary.Objects;
using Microsoft.Extensions.Logging;

namespace BitPortLibrary;

public static class CloudExtensions
{
    public static async Task<Objects.Cloud.ByPathObject.RootObject> ByPath(this BitPortClient client, string path = "", bool recursive = false)
    {
        var request_url = URLs.method_call_base + "/v2/cloud/byPath";
        client._logger.LogInformation(request_url);
        if (!string.IsNullOrEmpty(path))
        {
            var encoded_path = Helpers.EncodeToBase64(path);
            request_url = request_url + $"?folderPath={encoded_path}";
        }

        var message = await client._client.GetAsync(request_url);
        var raw_json = await message.Content.ReadAsStringAsync();
        client._logger.LogTrace(raw_json);
        if (message.IsSuccessStatusCode)
        {
            var json_object = JsonSerializer.Deserialize<Objects.Cloud.ByPathObject.RootObject>(raw_json);
            return json_object;
        }

        return null;
    }

    public static async Task GetFolderZipURL(this BitPortClient client, string folderCode)
    {
        var request_url = URLs.method_call_base + $"/v2/cloud/{folderCode}/download-as-zip";
        client._logger.LogInformation(request_url);
        var message = await client._client.GetAsync(request_url);
        await using (var fileStream = File.Create(folderCode + ".zip"))
        {
            await message.Content.CopyToAsync(fileStream);
        }
    }
}