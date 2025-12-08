using Interactables.Actions.Physics;
using Interactables.Triggers;
using LookupTables;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	public static class Maker
	{
		[MenuItem("GameObject/Make Item Shout-Pushable", true, 0)]
		private static bool MakeItemShoutPushable()
		{
			var gameObject = Selection.activeGameObject;
			return gameObject && gameObject.GetComponent<MeshFilter>();
		}

		[MenuItem("GameObject/Make Item Shout-Pushable", false, 0)]
		private static void MakeItemShoutPushable(MenuCommand menuCommand)
		{
			var gameObject = Selection.activeGameObject;
			var collider = gameObject.GetComponent<Collider>();
			if (!collider)
			{
				var meshCollider = gameObject.AddComponent<MeshCollider>();
				gameObject.AddComponent<ColliderDestructor>();
				meshCollider.convex = true;
				meshCollider.sharedMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
			}
			
			var rigidBody = gameObject.GetComponent<Rigidbody>();
			if (!rigidBody)
			{
				gameObject.AddComponent<Rigidbody>();
				gameObject.AddComponent<RigidBodyDestructor>();
			}
			
			var shoutReceiver = gameObject.AddComponent<InteractableShoutTrigger>();
			shoutReceiver.SetCollider(collider);
			var onShoutPush = gameObject.AddComponent<InteractablePushSelfFromPlayer>();
			shoutReceiver.SetInteractable(onShoutPush);
			gameObject.layer = LayerMask.NameToLayer("Interactable");
		}
	}
}