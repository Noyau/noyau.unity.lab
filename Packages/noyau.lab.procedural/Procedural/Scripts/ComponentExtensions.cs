namespace Noyau.Lab.Procedural
{
    using UnityEngine;
    using Component = UnityEngine.Component;

    public static class ComponentExtensions
    {
        public static T Require<T>(this Component root) where T : Component
        {
            return root.gameObject.Require<T>();
        }

        public static T Require<T>(this GameObject gameObject) where T : Component
        {
            T _comp = gameObject.GetComponent<T>();
            if (_comp == null)
                return gameObject.AddComponent<T>();
            return _comp;
        }
    } // class: ComponentExtensions
} // namespace