using System.Collections;
using System.Collections.Generic;

namespace Cfg
{
	public class Section : IEnumerable<KeyValuePair<string, string>>
	{
		private Dictionary<string, string> m_values;

		public string Name;

		public string this[string key]
		{
			get => m_values[key];
			set => m_values[key] = value;
		}
		
		public Section(string name)
		{
			m_values = new Dictionary<string, string>();
			Name = name;
		}
		
		public void Add(string key, string value) => m_values.Add(key, value);
		
		public bool Contains(string key) => m_values.ContainsKey(key);
		
		public bool TryGet(string key, out string value) => m_values.TryGetValue(key, out value);
		
		public bool Remove(string key) => m_values.Remove(key);
		
		public void Clear() => m_values.Clear();

		public Dictionary<string, string>.Enumerator GetEnumerator() => m_values.GetEnumerator();

		IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator() =>
			GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}