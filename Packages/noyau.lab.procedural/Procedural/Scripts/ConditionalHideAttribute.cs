using System;
using UnityEngine;

namespace Noyau.Lab.Procedural
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class ConditionalHideAttribute : PropertyAttribute
    {
        public string propertyName { get; }
        public int intValue { get; }
        public bool boolValue { get; }

        public ConditionalHideAttribute(string propertyName, int value)
        {
            this.propertyName = propertyName;
            this.intValue = value;
        }
        public ConditionalHideAttribute(string propertyName, bool value)
        {
            this.propertyName = propertyName;
            this.boolValue = value;
        }
    } // class: ConditionalHideAttribute
} // namespace