using CommandLine;
using Newtonsoft.Json;

namespace C314.IdeaLog;

[Verb("add", HelpText = "Add a new idea")]
internal class Add : IVerb
{
    [Value(0, Required = true, HelpText = "The idea")]
    public string Idea { get; set; }

    public void HandleInput()
    {
        var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var dir = Path.Combine(appdata, "C314.IdeaLog");
        var file = Path.Combine(dir, "ideas.json");
        var obj = JsonConvert.DeserializeObject<Idea>(File.ReadAllText(file));
        obj.Ideas.Add(Idea);
        File.WriteAllText(file, JsonConvert.SerializeObject(obj));
    }
}
