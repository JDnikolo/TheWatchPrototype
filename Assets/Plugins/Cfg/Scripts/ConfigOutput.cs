namespace Cfg
{
	public struct ConfigOutput
	{
		public string Section;
		public string Key;
		public string Value;
		public string Comment;

		public bool IsEmpty => Section == null && Comment == null;
		
		public bool IsContent => Section != null;
		
		public bool IsComment => Comment != null;
		
		public ConfigOutput(string section, string key, string value)
		{
			Section = section;
			Key = key;
			Value = value;
			Comment = null;
		}

		public ConfigOutput(string comment)
		{
			Section = null;
			Key = null;
			Value = null;
			Comment = comment;
		}
	}
}