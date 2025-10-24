using System.ComponentModel;
using UnityEngine;

namespace Logic
{
    [CreateAssetMenu(fileName = "CompareNumber", menuName = "Logic/Number/Compare-Gate")]
    public sealed class LogicGateCompareNumber : LogicGateIf<int>
    {
        public enum NumberCompareMode { None, Lower, LowerOrEqual, Equal, GreaterOrEqual, Greater }
        [SerializeField] private NumberCompareMode mode;
        protected override bool Equals(int x, int y)
        {
            return mode switch
            {
                NumberCompareMode.Lower => x < y,
                NumberCompareMode.LowerOrEqual => x <= y,
                NumberCompareMode.Equal => x == y,
                NumberCompareMode.GreaterOrEqual => x >= y,
                NumberCompareMode.Greater => x > y,
                _ => throw new InvalidEnumArgumentException()
            };
        }
    }
}