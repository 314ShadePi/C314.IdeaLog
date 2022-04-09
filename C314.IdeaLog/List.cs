using CommandLine;
using Newtonsoft.Json;

namespace C314.IdeaLog;

[Verb("list", HelpText = "List all ideas")]
internal class List : IVerb
{
    public void HandleInput()
    {
        var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var dir = Path.Combine(appdata, "C314.IdeaLog");
        var file = Path.Combine(dir, "ideas.json");
        var obj = JsonConvert.DeserializeObject<Idea>(File.ReadAllText(file));
        PrettyPrint(obj);
    }
    void PrettyPrint(Idea obj, string indent = "", bool isLast = true)
    {
        var marker = isLast ? "└──" : "├──";
        Console.Write(indent);
        Console.Write(marker);
        Console.WriteLine("Ideas");
        indent += isLast ? "   " : "|  ";
        var lastChild = obj.Ideas.Count - 1;
        for (int i = 0; i < obj.Ideas.Count; i++)
        {
            if (i == lastChild)
            {
                Console.WriteLine(indent + "└──" + obj.Ideas[i]);
                return;
            }
            Console.WriteLine(indent + "├──" + obj.Ideas[i]);
            return;
        }
    }
}
