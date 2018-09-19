using System;
using UnityEngine;

namespace Noyau.Lab.Procedural
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class IntervalAttribute : PropertyAttribute
    {
        public float min { get; }
        public float max { get; }

        public int intMin => (int)min;
        public int intMax => (int)max;

        public bool isInteger { get; }
        public bool isSlider { get; }

        private IntervalAttribute(float min, float max, bool isSlider, bool isInteger)
        {
            // NOTE: I don't wanna handle exceptions so just make sure "min" is lesser than "max" etc.
            // without logging anything because "Hey dude! 'min' means 'minimum' and 'max' means
            // 'maximum'! What's wrong with ya!"
            this.min = min < max ? min : max;
            this.max = max > min ? max : min;
            this.isSlider = isSlider;
            this.isInteger = isInteger;
        }

        public IntervalAttribute(float min, float max, bool isSlider = false) : this(min, max, isSlider, false)
        { }
        public IntervalAttribute(int min, int max, bool isSlider = false) : this(min, max, isSlider, true)
        { }
    } // class: IntervalAttribute

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class GreaterThanAttribute : IntervalAttribute
    {
        public GreaterThanAttribute(float min) : base(min, float.MaxValue, false) { }
        public GreaterThanAttribute(int min) : base(min, int.MaxValue, false) { }
    } // class: GreaterThanAttribute

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class LesserThanAttribute : IntervalAttribute
    {
        public LesserThanAttribute(float max) : base(float.MinValue, max, false) { }
        public LesserThanAttribute(int max) : base(int.MinValue, max, false) { }
    } // class: LesserThanAttribute
} // namespace