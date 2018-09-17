﻿using UnityEngine;

namespace Noyau.Lab.Procedural
{
    public sealed class Threshold
    {
        public float min { get; private set; } = float.MaxValue;
        public float max { get; private set; } = float.MinValue;

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
    } // class: Threshold
} // namespace