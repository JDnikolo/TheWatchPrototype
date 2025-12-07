using Attributes;
using Runtime;
using UnityEngine;

namespace Managers
{
	[AddComponentMenu("Managers/Local Manager")]
	public sealed class LocalManager : Singleton<LocalManager>
	{
		[CanBeNullInPrefab, SerializeField] private Camera localCamera;

		public Camera LocalCamera => localCamera;

		protected override bool Override => true;
	}
}