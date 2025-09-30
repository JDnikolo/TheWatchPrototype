using System;
using Localization;

namespace UI
{
	[Serializable]
	public struct TextWriterInput
	{
		public TextAsset textToDisplay;
		public TextWriterFinished onTextWriterFinished;
	}
}