using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Utilities
{
	public static partial class Utils
	{
		public static int SafeCount<T>(this ICollection<T> collection) => collection?.Count ?? -1;
		
		public static bool ContainsKey<T>(this ICollection<T> collection, int key) =>
			key >= 0 && key < collection.Count;
		
		public static bool TryGetValue<T>(this IList<T> collection, int key, out T value)
		{
			if (key < 0 || key >= collection.Count)
			{
				value = default;
				return false;
			}

			value = collection[key];
			return true;
		}
		
		public static bool ContainsReadOnlyKey<T>(this IReadOnlyCollection<T> collection, int key) =>
			key >= 0 && key < collection.Count;
		
		public static bool TryGetReadOnlyValue<T>(this IReadOnlyList<T> collection, int key, out T value)
		{
			if (key < 0 || key >= collection.Count)
			{
				value = default;
				return false;
			}

			value = collection[key];
			return true;
		}

		public static void RemoveAtFast<T>(this IList<T> collection, int index)
		{
			var last = collection.Count - 1;
			collection[index] = collection[last];
			collection.RemoveAt(last);
		}
		
		public static T GetRandom<T>(this IList<T> list)
		{
			if (list == null) throw new ArgumentNullException(nameof(list));
			var length = list.Count;
			if (length == 0) throw new InvalidOperationException("List is empty");
			if (length == 1) return list[0];
			return list[Random.Range(0, length)];
		}
	}
}