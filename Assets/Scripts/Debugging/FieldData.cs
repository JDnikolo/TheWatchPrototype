#if UNITY_EDITOR
namespace Debugging
{
	public struct FieldData
	{
		public bool Obsolete;
		public bool AllowNull;
		public int MinCount;
		public string CustomDebug;

		public FieldData(bool obsolete, bool allowNull, int minCount, string customDebug)
		{
			Obsolete = obsolete;
			AllowNull = allowNull;
			MinCount = minCount;
			CustomDebug = customDebug;
		}

		public override string ToString() => $"O: {Obsolete} A: {AllowNull} M: {MinCount} C: {CustomDebug}";
	}
}
#endif