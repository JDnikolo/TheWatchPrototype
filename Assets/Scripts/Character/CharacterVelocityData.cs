using UnityEngine;

namespace Character
{
	[CreateAssetMenu(fileName = "Velocity", menuName = "Character/Character Velocity Data")]
	public sealed class CharacterVelocityData : ScriptableObject
	{
		[SerializeField] private float acceleration;
		[SerializeField] private float maxVelocity;
		
		public float Acceleration => acceleration;
		public float MaxVelocity => maxVelocity;
	}
}