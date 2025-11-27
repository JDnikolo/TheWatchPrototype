using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boxing;
using Managers;
using Managers.Persistent;
using Runtime;
using UI.ComboBox;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace Utilities
{
	public static partial class Utils
	{
		/// <summary>
		/// Tests if a game object is the one assigned to <see cref="PlayerManager.PlayerObject"/>.
		/// </summary>
		public static bool IsPlayerObject(this GameObject gameObject)
		{
			var playerManager = PlayerManager.Instance;
			if (playerManager != null)
			{
				var playerObject = playerManager.PlayerObject;
				if (playerObject) return gameObject == playerObject;
			}

			return false;
		}

		/// <summary>
		/// Ensures only the Player input is usable.
		/// </summary>
		/// <remarks>Disables cursor.</remarks>
		public static void ForcePlayerInput(this InputManager inputManager)
		{
			inputManager.PlayerMap.Enabled = true;
			inputManager.UIMap.Enabled = false;
			inputManager.ToggleCursor(false);
		}

		/// <summary>
		/// Ensures only the UI input is usable.
		/// </summary>
		/// <remarks>Enables cursor.</remarks>
		public static void ForceUIInput(this InputManager inputManager)
		{
			inputManager.UIMap.Enabled = true;
			inputManager.PlayerMap.Enabled = false;
			inputManager.ToggleCursor(true);
		}

		public static void SetEnabled(this InputActionMap map, bool enabled)
		{
			if (enabled) map.Enable();
			else map.Disable();
		}

		public static bool GetParent(this Component component, out Transform parent)
		{
			parent = component.transform.parent;
			return parent;
		}
		
		public static T InstantiateManaged<T>(this T prefab) where T : Component
		{
			var result = Object.Instantiate(prefab);
			InitializeManaged(result);
			return result;
		}
		
		public static T InstantiateManaged<T>(this T prefab, Transform parent, 
			bool instantiateInWorldSpace = false) where T : Component
		{
			var result = Object.Instantiate(prefab, parent, instantiateInWorldSpace);
			InitializeManaged(result);
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void InitializeManaged<T>(this T result) where T : Component
		{
			var components = result.gameObject.GetComponents<Component>();
			for (var i = 0; i < components.Length; i++) InitializeManagedInternal(components[i]);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void InitializeManagedInternal(Component component)
		{
			if (component is IPrewarm prewarm) prewarm.OnPrewarm();
		}
		
		public static void CollectChildren<T>(this Component instance, ICollection<T> collection) where T : Component
		{
			if (collection == null) throw new ArgumentNullException(nameof(collection));
			var parent = instance.transform;
			var childCount = parent.childCount;
			for (var i = 0; i < childCount; i++)
				if (parent.GetChild(i).TryGetComponent(out T component))
					collection.Add(component);
		}
		
		public static void CollectChildren<TComponent, TCast>(this Component instance, 
			ICollection<TCast> collection) where TComponent : Component
		{
			if (collection == null) throw new ArgumentNullException(nameof(collection));
			var parent = instance.transform;
			var childCount = parent.childCount;
			for (var i = 0; i < childCount; i++)
				if (parent.GetChild(i).TryGetComponent(out TComponent component) && component is TCast cast)
					collection.Add(cast);
		}

		public static TCast GetDeferredComponent<TCast>(this Component instance)
		{
			var components = instance.GetComponents(typeof(Component));
			for (var i = 0; i < components.Length; i++)
				if (components[i] is TCast cast)
					return cast;
			return default;
		}
	}
}