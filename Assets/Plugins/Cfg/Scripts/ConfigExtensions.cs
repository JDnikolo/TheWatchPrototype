using System;
using System.IO;

namespace Cfg
{
	public static class ConfigExtensions
	{
		public static void Load(this Config config, FileInfo file)
		{
			if (!file.Exists) return;
			using (var reader = file.OpenText()) config.Load(reader);
		}

		public static void Save(this Config config, FileInfo file)
		{
			if (!file.Exists) return;
			using (var writer = file.CreateText()) config.Save(writer);
		}

		public static void Load(this Config config, string file) => config.Load(new FileInfo(file));

		public static void Save(this Config config, string file) => config.Save(new FileInfo(file));

		public static void Write<T>(this ConfigWriter writer, string key, T value)
		{
			if (value == null) throw new ArgumentNullException(nameof(value));
			writer.WritePair(key, value.ToString());
		}
		
		public static bool TryRead(this ConfigReader reader, out ConfigOutput output)
		{
			output = reader.Read();
			return !output.IsEmpty;
		}
	}
}