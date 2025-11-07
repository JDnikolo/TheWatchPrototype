using System.ComponentModel;
using UnityEngine;

namespace Logic.Boolean
{
    [CreateAssetMenu(fileName = "CompareNumber", menuName = "Logic/Boolean/Number IF-Gate")]
    public sealed class LogicBooleanIfNumber : LogicBooleanIf<int>
    {
        private enum NumberCompareMode : byte
        {
            Equal,
            NotEqual,
            Lower,
            LowerOrEqual,
            GreaterOrEqual,
            Greater
        }

        [SerializeField] private NumberCompareMode mode;

        protected override bool Equals(int x, int y) => mode switch
        {
            NumberCompareMode.Equal => x == y,
            NumberCompareMode.NotEqual => x != y,
            NumberCompareMode.Lower => x < y,
            NumberCompareMode.LowerOrEqual => x <= y,
            NumberCompareMode.GreaterOrEqual => x >= y,
            NumberCompareMode.Greater => x > y,
            _ => throw new InvalidEnumArgumentException()
        };
    }
}