using Runtime;
using UnityEngine;

namespace Managers
{
	[AddComponentMenu("Managers/Local Manager")]
	public sealed class LocalManager : Singleton<LocalManager>
	{
		protected override bool Override => true;
	}
}