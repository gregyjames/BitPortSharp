using System.Text.Json.Serialization;

namespace BitPortLibrary.Objects.Cloud;

public class ByPathObject
{
public class RootObject
{
    public string status { get; set; }
    public Data[] data { get; set; }
    public object errors { get; set; }
}

public class Data
{
    public string name { get; set; }
    public object code { get; set; }
    public int size { get; set; }
    public object files_count { get; set; }
    public object created_at { get; set; }
    public object parent_folder_code { get; set; }
    public object[] path { get; set; }
    public Folders[] folders { get; set; }
    public Files[] files { get; set; }
    public object parent_folder { get; set; }
}

public class Folders
{
    public string name { get; set; }
    public string code { get; set; }
    public int size { get; set; }
    public int files_count { get; set; }
    public Created_at created_at { get; set; }
    public object parent_folder_code { get; set; }
    public string[] path { get; set; }
}

public class Created_at
{
    public string date { get; set; }
    public int timezone_type { get; set; }
    public string timezone { get; set; }
}

public class Files
{
    public string name { get; set; }
    public Created_at1 created_at { get; set; }
    public string code { get; set; }
    public object parent_folder_code { get; set; }
    public int size { get; set; }
    public bool video { get; set; }
    public object[] screenshots { get; set; }
    public string extension { get; set; }
    public string type { get; set; }
    public object paused_at { get; set; }
    public int virus { get; set; }
    public object duration { get; set; }
    public Subtitles subtitles { get; set; }
    public string download_url { get; set; }
    public object[] path { get; set; }
}

public class Created_at1
{
    public string date { get; set; }
    public int timezone_type { get; set; }
    public string timezone { get; set; }
}

public class Subtitles
{
    public string selected_code { get; set; }
    public object shift { get; set; }
}


}