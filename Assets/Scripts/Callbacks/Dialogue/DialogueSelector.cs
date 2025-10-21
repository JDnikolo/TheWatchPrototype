using Localization.Dialogue;
using UnityEngine;

namespace Callbacks.Dialogue
{
	public abstract class DialogueSelector : MonoBehaviour
	{
		public abstract void Evaluate(DialogueOption selectedOption);
	}
}