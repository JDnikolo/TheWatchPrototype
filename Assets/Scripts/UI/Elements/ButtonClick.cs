/*
using System;
using System.Collections.Generic;
using System.Text;
using Interactables;
using UnityEngine;
using Utilities;

namespace UI.Elements
{
	public sealed partial class Button
	{
		[Serializable]
		private struct ButtonClick
		{
			[SerializeField] private Interactable interactable;
			[SerializeField] private string clicks;

			public void Organize(StringBuilder sb, List<Interactable> anyClick, 
				Dictionary<int, List<Interactable>> specificClicks)
			{
				if (string.IsNullOrEmpty(clicks))
					throw new InvalidOperationException("Clicks cannot be null or empty.");
				if (clicks == "any")
				{
					anyClick.Add(interactable);
					return;
				}

				sb.Clear();
				var firstNumber = -1;
				var lastNumber = -1;
				for (var i = 0; i < clicks.Length; i++)
				{
					var character = clicks[i];
					switch (character)
					{
						case ';':
							ref var number = ref firstNumber;
							if (firstNumber != -1) number = ref lastNumber;
							number = int.Parse(sb.Flush());
							Organize(firstNumber, lastNumber, interactable, specificClicks);
							firstNumber = -1;
							lastNumber = -1;
							break;
						case '-':
							firstNumber = int.Parse(sb.Flush());
							break;
						default:
							if (!char.IsNumber(character)) continue;
							sb.Append(character);
							break;
					}
				}
				
				Organize(firstNumber, lastNumber, interactable, specificClicks);
			}

			private void Organize(int firstNumber, int lastNumber, Interactable interactable,
				Dictionary<int, List<Interactable>> specificClicks)
			{
				if (firstNumber == -1) return;
				if (lastNumber == -1 || lastNumber == firstNumber)
				{
					Organize(firstNumber, interactable, specificClicks);
					return;
				}

				if (lastNumber < firstNumber) return;
				for (var i = firstNumber; i <= lastNumber; i++) Organize(i, interactable, specificClicks);
			}

			private void Organize(int number, Interactable interactable,
				Dictionary<int, List<Interactable>> specificClicks)
			{
				if (!specificClicks.TryGetValue(number, out var list)) list = new List<Interactable>();
				list.Add(interactable);
			}
		}
	}
}
*/