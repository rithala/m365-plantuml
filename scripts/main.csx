#! "netcoreapp3.1"

#r "nuget: System.Drawing.Common, 5.0.2"
#r "nuget: Pluralize.NET.Core, 1.0.0"

#load "lib/Config.csx"
#load "lib/HSLColor.csx"
#load "lib/MarkdownGeneration.csx"
#load "lib/VSCodeSnippets.csx"

using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using Pluralize.NET.Core;

var sourceFolder = @"../source";

var iconsDirectory = Path.Combine(sourceFolder, "icons");

var targetFolder = @"../dist";

var targetImageHeight = 70;
var inkScapePath = @"C:\Program Files\Inkscape\bin\inkscape.exe";

static string rsvgConvertPath = @"C:\ProgramData\chocolatey\bin\rsvg-convert.exe";
static string imageBaseUrl = "https://raw.githubusercontent.com/rithala/m365-plantuml/master/dist";

static string[] commonDefinitions = new[] { "Raw.puml", "Common.puml", "C4Integration.puml", "Simplified.puml" };

static Pluralizer pluralizer = new Pluralizer();

Main();

public void Main()
{

    // Cleanup
    if (Directory.Exists(targetFolder))
    {
        Directory.Delete(targetFolder, true);
    }
    Directory.CreateDirectory(targetFolder);

    foreach (var item in commonDefinitions)
    {
        File.Copy(Path.Combine(sourceFolder, item), Path.Combine(targetFolder, item));
    }

    foreach (var filePath in Directory.GetFiles(iconsDirectory,
                                            "*.svg",
                                            SearchOption.AllDirectories).GroupBy(x => Path.GetFileNameWithoutExtension(x),
                                                                                 x => x).Select(x => x.First()))
    {
        var serviceName = CreateTargetName(Path.GetFileNameWithoutExtension(filePath));
        Console.WriteLine($"Processing {serviceName}");

        RsvgConvert(filePath, Path.Combine(targetFolder, serviceName + ".svg"), targetImageHeight);
        ConvertToPuml($"{imageBaseUrl}/{serviceName}.svg", targetFolder, serviceName + ".puml");
    }

    var catAllFilePath = Path.Combine(targetFolder, "all.puml");
    CombineMultipleFilesIntoSingleFile(targetFolder, "*.puml", catAllFilePath, commonDefinitions);

    GenerateMarkdownTable("../", targetFolder, imageBaseUrl);
    GenerateVSCodeSnippets(targetFolder);

    Console.WriteLine("Finished");
}


public string CreateTargetName(string serviceName)
    => pluralizer.Singularize(serviceName.Replace(" ", "").Replace("(classic)", "Classic").Replace("(", "").Replace(")", ""));

public bool FitCanvasToDrawing(string inputPath)
{
    var processInfo = new ProcessStartInfo
    {
        FileName = inkScapePath,
        Arguments = $"--verb=FitCanvasToDrawing --verb=FileSave --verb=FileClose --verb=FileQuit \"{inputPath}\"",
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };

    using (var process = Process.Start(processInfo))
    {
        if (!process.WaitForExit(10000))
        {
            Console.WriteLine($"Killing InkScape for FitCanvasToDrawing {inputPath}");
            process.Kill();
            return false;
        }
    }

    return true;
}

public static bool RsvgConvert(string inputPath, string outputPath, int targetImageHeight, bool exportAsPng = false, bool withWhiteBackground = false)
{
    var processInfo = new ProcessStartInfo
    {
        FileName = rsvgConvertPath,
        Arguments = $"--height {targetImageHeight} --width {targetImageHeight} --keep-aspect-ratio --output \"{outputPath}\" --format {(exportAsPng ? "png" : "svg")} --background-color {(withWhiteBackground ? "white" : "none")} \"{inputPath}\"",
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };

    using (var process = Process.Start(processInfo))
    {
        if (!process.WaitForExit(10000))
        {
            Console.WriteLine($"Killing rsvg-convert {inputPath}");
            process.Kill();
            return false;
        }
    }

    return true;
}

private static void CombineMultipleFilesIntoSingleFile(string inputDirectoryPath, string inputFileNamePattern, string outputFilePath, string[] omitFiles)
{
    var inputFilePaths = Directory.GetFiles(inputDirectoryPath, inputFileNamePattern, SearchOption.AllDirectories).Where(x => !omitFiles.Contains(Path.GetFileName(x)));
    using (var outputStream = File.Create(outputFilePath))
    {
        foreach (var inputFilePath in inputFilePaths)
        {
            using (var inputStream = File.OpenRead(inputFilePath))
            {
                inputStream.CopyTo(outputStream);

                byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                outputStream.Write(newline, 0, newline.Length);
            }
        }
    }
}

public string ConvertToPuml(string imgPath, string directory, string pumlFileName)
{
    var entityName = Path.GetFileNameWithoutExtension(pumlFileName);
    var pumlPath = Path.Combine(directory, pumlFileName);

    var pumlContent = new StringBuilder();

    pumlContent.AppendLine($"AzureEntityColoring({entityName})");
    pumlContent.AppendLine($"!define {entityName}(e_alias, e_label, e_techn) AzureEntity(e_alias, e_label, e_techn, AZURE_SYMBOL_COLOR, \"{imgPath}\", {entityName})");
    pumlContent.AppendLine($"!define {entityName}(e_alias, e_label, e_techn, e_descr) AzureEntity(e_alias, e_label, e_techn, e_descr, AZURE_SYMBOL_COLOR, \"{imgPath}\", {entityName})");

    File.WriteAllText(pumlPath, pumlContent.ToString());
    return pumlPath;
}