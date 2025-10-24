using System;
using System.IO;

namespace Cfg
{
	public class ConfigWriter : IDisposable
	{
		private StreamWriter m_writer;
		private string m_section;
		
		public ConfigWriter(StreamWriter writer) => m_writer = writer;

		public void WriteSection(string section)
		{
			if (string.IsNullOrEmpty(section)) throw new ArgumentNullException(nameof(section));
			if (m_section != null) m_writer.WriteLine();
			m_section = section;
			m_writer.WriteLine($"[{section}]");
		}

		public void WritePair(string key, string value)
		{
			if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));
			if (string.IsNullOrEmpty(m_section)) throw new InvalidOperationException("No section specified.");
			m_writer.WriteLine($"{key}={value}");
		}
		
		public void WriteComment(string comment) => m_writer.WriteLine($"#{comment}");

		public void Dispose()
		{
			if (m_writer != null)
			{
				m_writer.Dispose();
				m_writer = null;
			}
		
			m_section = null;
		}
	}
}