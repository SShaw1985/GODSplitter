// See https://aka.ms/new-console-template for more information
// Define the input file path and the output directory
using System.Text.RegularExpressions;

string inputFilePath = "C:\\Users\\Stephen.Shaw\\Documents\\GitHub\\Poundland.HHT\\hhtApp\\Data\\SyncData.cs"; // Replace with your file path
string outputDirectory = "C:\\Users\\Stephen.Shaw\\Documents\\GitHub\\Poundland.HHT\\Poundland.Core\\POCO"; // Replace with your desired output directory


// Read the content of the input file
string content = File.ReadAllText(inputFilePath);

// Define a regex pattern to match class and enum definitions, ensuring we capture the entire content
string pattern = @"(public\s+(?:class|enum)\s+\w+\s*{(?:[^{}]*|(?<open>{)|(?<-open>}))*(?(open)(?!))})";
var matches = Regex.Matches(content, pattern, RegexOptions.Singleline);

// Ensure the output directory exists
Directory.CreateDirectory(outputDirectory);

// Iterate over each match and write it to a separate file
foreach (Match match in matches)
{
    // Extract the class or enum name for the filename
    var classNameMatch = Regex.Match(match.Value, @"public\s+(?:class|enum)\s+(\w+)");
    if (classNameMatch.Success)
    {
        string className = classNameMatch.Groups[1].Value;
        string fileName = $"{className}.cs";

        // Write the class/enum to a new file in the output directory
        string filePath = Path.Combine(outputDirectory, fileName);
        string fileContent = $"using System;\nusing System.Collections.Generic;\n\nnamespace NAMESPACEHERE\n{{\n{match.Value}\n}}\n";
        File.WriteAllText(filePath, fileContent);

        Console.WriteLine($"Created file: {filePath}");
    }
}

Console.WriteLine("Splitting complete!");