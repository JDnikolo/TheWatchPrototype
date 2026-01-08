#if UNITY_EDITOR
using System;
using Object = UnityEngine.Object;

namespace Debugging
{
	public struct DebugObject : IEquatable<DebugObject>
	{
		public Object Instance;
		public Object TestInstance;
		public string DebugString;

		public DebugObject(Object instance, Object testInstance, string debugString)
		{
			Instance = instance;
			DebugString = debugString;
			TestInstance = testInstance;
		}

		public bool Equals(DebugObject other) => DebugString == other.DebugString;

		public override bool Equals(object obj) => obj is DebugObject other && Equals(other);

		public override int GetHashCode() => (DebugString != null ? DebugString.GetHashCode() : 0);

		public override string ToString() => $"{Instance} {TestInstance} {DebugString}";
	}
}
#endif