using UnityEngine;

namespace Noyau.Lab.Procedural
{
    [System.Serializable]
    public abstract class ShaderParam<T> : ISerializationCallbackReceiver
    {
        [SerializeField] protected string m_propertyName;
        protected int m_propertyId;

        [SerializeField] protected T m_value;

        public ShaderParam(string propertyName, T defaultValue = default(T))
        {
            m_propertyName = propertyName;
            m_propertyId = -1;
            m_value = defaultValue;
        }

        // TODO
        public abstract void Set(Material target);

        public void OnBeforeSerialize()
        {
            m_propertyId = Shader.PropertyToID(m_propertyName);
        }
        public virtual void OnAfterDeserialize() { }

        public static implicit operator T(ShaderParam<T> param) => param.m_value;
    } // class: ShaderParam<T>

    [System.Serializable]
    public sealed class ColorParam : ShaderParam<Color>
    {
        public ColorParam(string propertyName, Color defaultValue) : base(propertyName, defaultValue)
        { }
        public ColorParam(string propertyName) : this(propertyName, Color.white)
        { }

        public override void Set(Material target)
        {
            target.SetColor(m_propertyId, this);
        }
    } // class: ColorParam

    [System.Serializable]
    public sealed class VectorParam : ShaderParam<Vector4>
    {
        public VectorParam(string propertyName, Vector4 defaultValue = default(Vector4)) : base(propertyName, defaultValue)
        { }

        public override void Set(Material target)
        {
            target.SetVector(m_propertyId, this);
        }
    } // class: VectorParam

    [System.Serializable]
    public sealed class TextureParam : ShaderParam<Texture2D>
    {
        public TextureParam(string propertyName, Texture2D defaultValue = null) : base(propertyName, defaultValue)
        { }

        public override void Set(Material target)
        {
            target.SetTexture(m_propertyId, this);
        }
    } // class: TextureParam
} // namespace