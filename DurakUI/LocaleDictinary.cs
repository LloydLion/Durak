using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Globalization;
using System.Threading;
using Newtonsoft.Json;
using System.IO;

namespace DurakUI
{
	class LocaleDictinary
	{
		private Dictionary<string, string> dictinary;

		public const string DefaultCulture = "en-US";


		public static LocaleDictinary Default { get; }

		public string FolderPath { get; }

		public IReadOnlyDictionary<string, string> BaseDictinary => dictinary;


		public string this[string key] => GetValue(key);


		static LocaleDictinary()
		{
			Default = new LocaleDictinary("Languages");
			
		}

		public LocaleDictinary(string folderPath)
		{
			FolderPath = folderPath;

			var file = folderPath + Path.DirectorySeparatorChar +
				Thread.CurrentThread.CurrentUICulture.Name + ".json";
			if (File.Exists(file))
				dictinary = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(file));
			else dictinary = new Dictionary<string, string>();

			var dictinary2 = JsonConvert.DeserializeObject<Dictionary<string, string>>
				(File.ReadAllText(folderPath + Path.DirectorySeparatorChar +
				DefaultCulture + ".json"));

			for (int i = 0; i < dictinary2.Count; i++)
			{
				var el = dictinary2.ElementAt(i);
				if (!dictinary.ContainsKey(el.Key))
					dictinary.Add(el.Key, el.Value);
			}
		}


		public string GetValue(string key)
		{
			return dictinary[key];
		}
	}
}
