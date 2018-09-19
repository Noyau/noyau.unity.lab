using UnityEngine;

namespace Noyau.Lab.Procedural
{
    public sealed class Threshold
    {
        public float min { get; private set; } = float.MaxValue;
        public float max { get; private set; } = float.MinValue;

        public void Reset()
        {
            min = float.MaxValue;
            max = float.MinValue;
        }

        public void AddValue(float value)
        {
            if (value < min) min = value;
            if (value > max) max = value;
        }

        public float InverseLerp(float value)
        {
            return Mathf.InverseLerp(min, max, value); // Note: maybe implement custom implementation? for efficiency?
        }

        public static implicit operator Vector2(Threshold threshold) => new Vector2(threshold.min, threshold.max);
        public static implicit operator Vector4(Threshold threshold) => new Vector4(threshold.min, threshold.max);
        public static implicit operator Threshold(Vector2 vector) => new Threshold { min = vector.x, max = vector.y };
        public static implicit operator Threshold(Vector4 vector) => new Threshold { min = vector.x, max = vector.y };
    } // class: Threshold
} // namespace