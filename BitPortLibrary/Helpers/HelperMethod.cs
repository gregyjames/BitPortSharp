using System.Text.Json;

namespace BitPortLibrary;

public static class HelperMethod
{
    public static void SerializeObjectToFile<T>(T obj, string filePath)
    {
        // Serialize the object to JSON
        string json = JsonSerializer.Serialize(obj);

        // Save the JSON to the file
        File.WriteAllText(filePath, json);
    }

    public static T DeserializeObjectFromFile<T>(string filePath)
    {
        // Read the JSON from the file
        string json = File.ReadAllText(filePath);

        // Deserialize the JSON to an object of type T
        return JsonSerializer.Deserialize<T>(json);
    }
}