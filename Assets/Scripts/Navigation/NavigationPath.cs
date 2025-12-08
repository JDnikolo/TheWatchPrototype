using Attributes;
using Debugging;
using UnityEngine;
using Utilities;

namespace Navigation
{
	[AddComponentMenu("Navigation/Navigation Path")]
	public sealed class NavigationPath : BaseBehaviour
	{
		// ReSharper disable once MissingLinebreak
#if UNITY_EDITOR
		[CustomDebug(nameof(DebugWaypoints))] 
#endif
		[SerializeField] private Transform[] waypoints;
		[SerializeField] private bool circular;
		[SerializeField] private bool patrol;

		private bool m_reverse;
		
		public Transform this[int index] => waypoints[index];

		public Transform MoveNext(ref int i)
		{
			if (m_reverse)
			{
				if (i > 0) i -= 1;
				else if (circular) i = waypoints.Length - 1;
				else
				{
					if (patrol) m_reverse = false;
					i = 0;
				}
			}
			else
			{
				if (i < waypoints.Length - 1) i += 1;
				else if (circular) i = 0;
				else
				{
					if (patrol) m_reverse = true;
					i = waypoints.Length - 1;
				}
			}

			return waypoints[i];
		}
#if UNITY_EDITOR
		private bool DebugWaypoints(OperationData operationData, string path, FieldData fieldData)
		{
			fieldData.MinCount = circular ? 2 : 1;
			return operationData.TestArrayGeneric(path, fieldData, waypoints);
		}

		private void OnDrawGizmosSelected()
		{
			if (waypoints == null) return;
			Color lineColor;
			if (circular) lineColor = Color.green;
			else if (patrol) lineColor = Color.blue;
			else lineColor = Color.yellow;
			var length = waypoints.Length;
			if (length < 2) return;
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(waypoints[0].position, 0.2f);
			for (var i = 1; i < length; i++)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawWireSphere(waypoints[i].position, 0.2f);
				Gizmos.color = lineColor;
				Gizmos.DrawLine(waypoints[i].position, waypoints[i - 1].position);
			}

			if (circular)
			{
				Gizmos.color = lineColor;
				Gizmos.DrawLine(waypoints[length - 1].position, waypoints[0].position);
			}
		}
#endif
	}
}