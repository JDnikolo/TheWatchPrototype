#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using Object = UnityEngine.Object;

namespace Debugging
{
	public struct OperationData
	{
		public Object Instance;
		public Dictionary<Type, MethodInfo> ArrayMethods;
		public Dictionary<Type, MethodInfo> ListMethods;
		public Dictionary<DictionaryType, MethodInfo> DictionaryMethods;

		public OperationData(Object instance, bool save)
		{
			Instance = instance;
			if (save)
			{
				ArrayMethods = new Dictionary<Type, MethodInfo>();
				ListMethods = new Dictionary<Type, MethodInfo>();
				DictionaryMethods = new Dictionary<DictionaryType, MethodInfo>();
			}
			else
			{
				ArrayMethods = null;
				ListMethods = null;
				DictionaryMethods = null;
			}
		}
	}
}
#endif