using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ExamplesGenerator
{
	internal static class AsciiDocParser
	{
		private static readonly Regex TitleRegex =
			new Regex(@"=== (?<name>.*?)\.a(?:scii)?doc: line (?<lineNumber>\d+): (?<hash>.*?)(?:\.a(?:scii)?doc)?$");

		public static List<ReferencePage> ParsePages(string path)
		{
			var file = GetLinesFromPath(path);
			var pages = new Dictionary<string, ReferencePage>();

			for (var index = 0; index < file.Length; index++)
			{
				var line = file[index];
				if (line.StartsWith("=== "))
				{
					var match = TitleRegex.Match(line);
					if (!match.Success)
					{
						Console.WriteLine($"Could not find title match, line: {index}, input: {line}");
						continue;
					}

					var name = match.Groups["name"].Value;
					var lineNumber = int.Parse(match.Groups["lineNumber"].Value);
					var hash = match.Groups["hash"].Value;

					// skip to start of body
					index += 3;
					line = file[index];
					var builder = new StringBuilder();

					while (line != "----")
					{
						builder.AppendLine(line);
						index++;
						line = file[index];
					}

					var content = builder.ToString();

					index += 2;
					line = file[index];

					var languages = line.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
					index += 2;
					line = file[index];
					var implemented = line.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

					var exampleLanguages = languages
						.Zip(implemented, (l, i) => new Language { Name = l.Trim(), Implemented = i.Trim() == "&check;" })
						.ToList();

					index++;

					if (!pages.TryGetValue(name, out var page))
					{
						page = new ReferencePage(name);
						pages.Add(name, page);
					}

					var example = new ReferenceExample(hash, lineNumber, content);
					example.Languages.AddRange(exampleLanguages);

					if (!page.Examples.Contains(example))
						page.Examples.Add(example);
				}
			}

			return pages.Values.ToList();
		}

		private static string[] GetLinesFromPath(string path)
		{
			var uri = new Uri(path);

			if (uri.IsFile)
				return File.ReadAllLines(path);

			using (var client = new WebClient())
			{
				try
				{
					var lines = new List<string>();
					using (var stream = client.OpenRead(path))
					using (var streamReader = new StreamReader(stream, Encoding.UTF8))
					{
						string str;
						while ((str = streamReader.ReadLine()) != null)
							lines.Add(str);
					}
					return lines.ToArray();
				}
				catch (Exception e)
				{
					throw new Exception($"Could not download master reference file from {path}", e);
				}
			}
		}
	}
}
