#r "nuget: Newtonsoft.Json, 11.0.2"

using System.Text.RegularExpressions;
using Newtonsoft.Json;

public void GenerateVSCodeSnippets(string distFolder)
{
    Console.WriteLine("Generating VSCode Snippets...");

    var snippets = new Dictionary<string, Snippet>();

    foreach (var filePath in Directory.GetFiles(distFolder, "*.puml", SearchOption.AllDirectories))
    {
        var entityName = Path.GetFileNameWithoutExtension(filePath);
        if (entityName == "all")
        {
            continue;
        }

        Console.WriteLine($"Adding snippet {entityName} from {filePath}");

        snippets.Add($"{entityName}", new Snippet{
            prefix = $"{entityName}",
            description = $"Add {entityName} to diagram",
            body = new List<string>{
                $"{entityName}(${{1:alias}}, \"${{2:label}}\", \"${{3:technology}}\")",
                "$0"
            }
        });

        Console.WriteLine($"Adding snippet {entityName}_Descr");

        snippets.Add($"{entityName}_Descr", new Snippet{
            prefix = $"{entityName} with Description",
            description = $"Add {entityName} with Description to diagram",
            body = new List<string>{
                $"{entityName}(${{1:alias}}, \"${{2:label}}\", \"${{3:technology}}\", \"${{4:description}}\")",
                "$0"
            }
        });
    }

    var snippetsDirectory = Path.Combine(distFolder, ".vscode", "snippets");
    Directory.CreateDirectory(snippetsDirectory);

    using (StreamWriter file = File.CreateText(Path.Combine(snippetsDirectory, "diagram.json")))
    {
        var serializer = new JsonSerializer
        {
            Formatting = Formatting.Indented,
        };
        serializer.Serialize(file, snippets);
    }
}

private static string SplitCamelCase(string camelCaseString) => Regex.Replace(camelCaseString, "(\\B[A-Z])", " $1");

private class Snippet {
    public string prefix { get; set; }

    public string description { get; set; }
    public string scope { get; set; } = "plantuml";

    public List<string> body { get; set; }
}