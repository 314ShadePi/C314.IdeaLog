using CommandLine;
using Newtonsoft.Json;

namespace C314.IdeaLog;

internal class Program
{
    public static void Main(string[] args)
    {
        var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var dir = Path.Combine(appdata, "C314.IdeaLog");
        var file = Path.Combine(dir, "ideas.json");
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        if (!File.Exists(file))
        {
            var obj = new Idea
            {
                Ideas = new List<string>
                {
                    "test"
                }
            };
            File.WriteAllText(file, JsonConvert.SerializeObject(obj));
            obj = JsonConvert.DeserializeObject<Idea>(File.ReadAllText(file));
            obj.Ideas.RemoveAt(0);
            File.WriteAllText(file, JsonConvert.SerializeObject(obj));
        }
        Type[] types = { typeof(Add), typeof(List) };
        Parser.Default.ParseArguments(args, types)
              .WithParsed(obj => ((IVerb)obj).HandleInput())
              .WithNotParsed(HandleErrors);
    }

    private static void HandleErrors(IEnumerable<Error> errors)
    {
        if (errors.IsVersion())
        {
            Console.WriteLine("Version Request");
            return;
        }

        if (errors.IsHelp())
        {
            Console.WriteLine("Help Request");
            return;
        }

        Console.WriteLine("Parser Fail");

        foreach (var error in errors)
        {
            Console.WriteLine(error.ToString());
        }
    }
}
