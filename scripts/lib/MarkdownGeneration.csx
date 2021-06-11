using System.Text.RegularExpressions;

public static void GenerateMarkdownTable(string distFolder)
{
    Console.WriteLine("Generating Markdown table...");

    var sbTable = new StringBuilder();
    sbTable.AppendLine("Macro (Name) | <pre>Color</pre> | Url");
    sbTable.AppendLine(" ---  | :---: | ---");

    foreach (var filePath in Directory.GetFiles(distFolder, "*.puml", SearchOption.AllDirectories))
    {
        var entityName = Path.GetFileNameWithoutExtension(filePath);

        sbTable.Append($"{entityName} </br> ({entityName}) | ");

        if (File.Exists(Path.Combine(distFolder, $"{entityName}.svg")))
        {
            sbTable.Append($"![{entityName}](dist/{entityName}.svg?raw=true) | ");
        }
        else
        {
            sbTable.Append($" |");
        }

        sbTable.AppendLine($"{entityName}.puml");
    }
    File.WriteAllText(Path.Combine(distFolder, "table.md"), sbTable.ToString());
}