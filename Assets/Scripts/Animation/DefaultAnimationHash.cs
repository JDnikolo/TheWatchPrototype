using UnityEngine;

namespace Animation
{
    public class DefaultAnimationHash
    {
        public static readonly int Salute = Animator.StringToHash("Salute");
        public static readonly int Scared = Animator.StringToHash("Scared");
        public static readonly int Talking = Animator.StringToHash("Talking");
    }

    public enum DefaultAnimations
    {
        None, Salute, Scared, Talking,
    }
}