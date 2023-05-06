using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.CoreSkills;
using Microsoft.SemanticKernel.Skills.Document.FileSystem;
using Microsoft.SemanticKernel.Skills.Document.OpenXml;
using Microsoft.SemanticKernel.Skills.Document;
using Microsoft.SemanticKernel.Orchestration;
using Op.SemanticKernel.Demo.Examples.Skills;
using Microsoft.Extensions.Logging;

namespace Op.SemanticKernel.Demo.Examples
{
    public sealed class ProjectHistoryExample
    {
        private const string MemoryCollectionName = "ProjectHistory";
        private readonly IKernel _kernel;
        private readonly TextMemorySkill _memorySkill;
        private readonly string _path = $"./Documents/sample-document.docx";
        private readonly ILogger _logger;

        public ProjectHistoryExample(IKernel kernel, ILogger logger) =>
            (_kernel, _memorySkill, _logger) = (kernel, new(), logger);

        public async Task RunAsync()
        {
            var context = _kernel.CreateNewContext();
            await BuildMemory(context);

            _kernel.ImportSkill(_memorySkill);
            var pc = _kernel.AddConsultantProfile();
            //context = _kernel.CreateNewContext();

            context[TextMemorySkill.CollectionParam] = MemoryCollectionName;
            context[TextMemorySkill.LimitParam] = "3";

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter question:");
                Console.ResetColor();

                var query = Console.ReadLine();
                context["query"] = query;
                var response = await pc.InvokeAsync(context, log: _logger);

                WriteResponse(response);
            }
        }

        private async Task BuildMemory(SKContext context)
        {
            DocumentSkill documentSkill = new(new WordDocumentConnector(), new LocalFileSystemConnector());

                Console.WriteLine($"Memorizing {new FileInfo(_path).Name}");
                string text = await documentSkill.ReadTextAsync(_path, context);

                var sections = text
                    .Split($"{Environment.NewLine}{Environment.NewLine}", StringSplitOptions.RemoveEmptyEntries)
                    .Where(s => s.Split(" ").Length > 2)
                    .Select(e => e.Trim())
                    .ToArray();
                
                for (var i = 0; i < sections.Length; i++)
                {
                    var index = i+1;
                    var section = sections[i];
                    var wordCount = section.Split(' ').Length;

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Storing: {index}/{sections.Length}");
                    Console.ResetColor();
                    Console.WriteLine(section);
                    await _kernel.Memory.SaveInformationAsync("ProjectHistory", section, $"ph-{index+1:00}");
                }
                
                Console.WriteLine($"Completed {_path}!");
        }

        private static void WriteResponse(object response)
        {
            Console.WriteLine();
            Console.WriteLine("GPT:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(response);
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
