using System;
using System.Collections;
using AYellowpaper.SerializedCollections.KeysGenerators;
using Localization.Dialogue;
using UnityEngine;

namespace Editor
{
	[KeyListGenerator("Dialogue Options", typeof(DialogueOption))]
	public sealed class DialogueListGenerator : KeyListGenerator
	{
		[SerializeField] private DialogueObject dialogue;
		
		public override IEnumerable GetKeys(Type type)
		{
			if (dialogue) return dialogue.Options;
			return new DialogueOption[0];
		}
	}
}