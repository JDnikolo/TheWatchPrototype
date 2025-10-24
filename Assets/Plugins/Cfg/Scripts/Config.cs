using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Cfg
{
	public class Config : IEnumerable<KeyValuePair<string, Section>>
	{
		private Dictionary<string, Section> m_sections = new();

		public Section this[string key] => m_sections[key];

		public void Add(string key, Section value) => m_sections.Add(key, value);
		
		public bool Contains(string key) => m_sections.ContainsKey(key);
		
		public bool TryGet(string key, out Section value) => m_sections.TryGetValue(key, out value);
		
		public bool Remove(string key) => m_sections.Remove(key);

		public void Clear(bool deep)
		{
			if (deep)
				foreach (var section in m_sections.Values) 
					section.Clear();
			m_sections.Clear();
		}

		public void Load(StreamReader stream)
		{
			using (var reader = new ConfigReader(stream))
				while (true)
				{
					var output = reader.Read();
					if (output.IsEmpty) break;
					if (output.Comment != null) continue;
					var sectionName = output.Section;
					if (!m_sections.TryGetValue(sectionName, out var section))
					{
						section = new Section(sectionName);
						m_sections.Add(sectionName, section);
					}
					
					section.Add(output.Key, output.Value);
				}
		}

		public void Save(StreamWriter stream)
		{
			using (var writer = new ConfigWriter(stream))
				foreach (var sectionPair in m_sections)
				{
					writer.WriteSection(sectionPair.Key);
					foreach (var pair in sectionPair.Value) 
						writer.WritePair(pair.Key, pair.Value);
				}
		}
		
		public Dictionary<string, Section>.Enumerator GetEnumerator() => m_sections.GetEnumerator();

		IEnumerator<KeyValuePair<string, Section>> IEnumerable<KeyValuePair<string, Section>>.GetEnumerator() =>
			GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}