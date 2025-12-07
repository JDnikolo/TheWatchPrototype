using Logic.String;
using UnityEngine;

namespace Character
{
	[CreateAssetMenu(fileName = "Profile", menuName = "Character/Character Profile Data")]
	public sealed class CharacterProfileData : BaseObject
	{
		[SerializeField] private LogicString characterName;
		
		public string CharacterName => characterName.Evaluate();
	}
}