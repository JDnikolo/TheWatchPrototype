using UnityEngine;

namespace Navigation
{
	[AddComponentMenu(menuName: "Navigation/Navigation Path")]
	public sealed class NavigationPath : MonoBehaviour
	{
		[SerializeField] private Transform[] waypoints;
		[SerializeField] private bool circular;

		public Transform this[int index] => waypoints[index];

		public Transform MoveNext(ref int i)
		{
			if (i < waypoints.Length - 1) i += 1;
			else if (circular) i = 0;
			else i = waypoints.Length - 1;
			return waypoints[i];
		}

		private void OnDrawGizmosSelected()
		{
			if (waypoints == null) return;
			var length = waypoints.Length;
			if (length < 2) return;
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(waypoints[0].position, 0.2f);
			for (var i = 1; i < length; i++)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawWireSphere(waypoints[i].position, 0.2f);
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(waypoints[i].position, waypoints[i - 1].position);
			}

			if (circular)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(waypoints[length - 1].position, waypoints[0].position);
			}
		}
	}
}