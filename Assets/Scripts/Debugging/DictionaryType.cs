#if UNITY_EDITOR
using System;

namespace Debugging
{
	public struct DictionaryType : IEquatable<DictionaryType>
	{
		private Type m_keyType;
		private Type m_valueType;

		public DictionaryType(Type keyType, Type valueType)
		{
			m_keyType = keyType;
			m_valueType = valueType;
		}

		public bool Equals(DictionaryType other) =>
			m_keyType == other.m_keyType && m_valueType == other.m_valueType;

		public override bool Equals(object obj) => obj is DictionaryType other && Equals(other);

		public override int GetHashCode() => HashCode.Combine(m_keyType, m_valueType);
	}
}
#endif