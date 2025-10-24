using System;
using System.IO;

namespace Cfg
{
	public class ConfigReader : IDisposable
	{
		private enum LineType : byte
		{
			Empty,
			Comment,
			Content,
		}
		
		private StreamReader m_reader;
		private string m_section;
		
		public ConfigReader(StreamReader reader) => m_reader = reader;

		public ConfigOutput Read()
		{
			while (!m_reader.EndOfStream)
			{
				var line = m_reader.ReadLine();
				switch (GetLineType(line))
				{
					case LineType.Empty:
						m_section = null;
						continue;
					case LineType.Comment:
						return new ConfigOutput(line);
					case LineType.Content:
						var index = line.IndexOf('[');
						if (index >= 0)
						{
							var index2 = line.IndexOf(']', index);
							if (index2 < 0) continue;
							m_section = line.Substring(index + 1, index2 - index - 1);
							continue;
						}

						if (m_section == null) continue;
						index = line.IndexOf('=');
						if (index < 0) continue;
						return new ConfigOutput(m_section, line.Substring(0, index), line.Substring(index + 1));
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			return new ConfigOutput();
		}
		
		private static LineType GetLineType(string line)
		{
			for (var i = 0; i < line.Length; i++)
			{
				var character = line[i];
				if (char.IsWhiteSpace(character)) continue;
				if (character == '#') return LineType.Comment;
				return LineType.Content;
			}

			return LineType.Empty;
		}
		
		public void Dispose()
		{
			if (m_reader != null)
			{
				m_reader.Dispose();
				m_reader = null;
			}
			
			m_section = null;
		}
	}
}