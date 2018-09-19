using UnityEngine;

namespace Noyau.Lab.Procedural
{
    [System.Serializable]
    public class ShaderParam : ISerializationCallbackReceiver
    {
        [SerializeField] protected string m_propertyName;
        protected int m_propertyId;

        public string propertyName => m_propertyName;
        public int propertyId => m_propertyId;

        public virtual object objectReferenceValue { get; } = null;

        public ShaderParam(string propertyName)
        {
            m_propertyName = propertyName;
            m_propertyId = -1;
        }

        // TODO implement *all* setters for material properties
        public void SetTargetValue(Material target, Color color)
        {
            target.SetColor(propertyId, color);
        }
        public void SetTargetValue(Material target, Vector2 vector)
        {
            target.SetVector(propertyId, vector);
        }
        public void SetTargetValue(Material target, Vector4 vector)
        {
            target.SetVector(propertyId, vector);
        }
        public void SetTargetValue(Material target, Texture texture)
        {
            target.SetTexture(propertyId, texture);
        }

        public virtual void OnBeforeSerialize() { }
        public virtual void OnAfterDeserialize()
        {
            m_propertyId = Shader.PropertyToID(m_propertyName);
        }
    } // class: ShaderParam

    public abstract class ShaderParam<T> : ShaderParam
    {
        [SerializeField] protected T m_value;

        public T value
        {
            get { return m_value; }
            set { OnValueChanged(m_value = value); }
        }

        public sealed override object objectReferenceValue => value;

        public ShaderParam(string propertyName, T defaultValue = default(T)) : base(propertyName)
        {
            m_value = defaultValue;
        }

        protected virtual void OnValueChanged(T value) { }

        /// <summary>
        /// Set material property defined by the "propertyName"
        /// using the current "value".
        /// </summary>
        /// <param name="target">the Material to update</param>
        public abstract void SetTargetValue(Material target);

        public static implicit operator T(ShaderParam<T> param) => param.m_value;
    } // class: ShaderParam<T>

    [System.Serializable]
    public sealed class ColorParam : ShaderParam<Color>
    {
        public ColorParam(string propertyName, Color defaultValue) : base(propertyName, defaultValue)
        { }
        public ColorParam(string propertyName) : this(propertyName, Color.white)
        { }

        public override void SetTargetValue(Material target)
        {
            SetTargetValue(target, this);
        }
    } // class: ColorParam

    [System.Serializable]
    public sealed class GradientParam : ShaderParam<Gradient>
    {
        public const int TextureResolution = 64; // expected texture size: 64x2
        public const int ColorBufferSize = TextureResolution << 1;

        [SerializeField, HideInInspector] private Texture2D m_texture = null; // TODO release texture sometimes (?)
        [SerializeField, HideInInspector] private Color[] m_colors = null;

        // TODO implement custom "preview" for texture

        public GradientParam(string propertyName, Gradient defaultValue) : base(propertyName, defaultValue)
        { }
        public GradientParam(string propertyName) : this(propertyName, new Gradient())
        {
            value.SetKeys(
                new[] { new GradientColorKey(Color.black, 0F), new GradientColorKey(Color.white, 1F), },
                new[] { new GradientAlphaKey(1F, 0F), new GradientAlphaKey(1F, 1F), });
        }

        private void OnValidateGradient()
        {
            if (m_texture == null)
            {
                m_texture = new Texture2D(TextureResolution, 2);
                m_texture.name = $"{propertyName} ({typeof(GradientParam).Name})";
            }

            if (m_colors == null || m_colors.Length != ColorBufferSize)
                m_colors = new Color[ColorBufferSize];

            for (int i = 0; i < TextureResolution; ++i)
                m_colors[i] = m_colors[i + TextureResolution] = value.Evaluate(i / (TextureResolution - 1F));

            m_texture.SetPixels(m_colors);
            m_texture.Apply();
        }

        public override void SetTargetValue(Material target)
        {
            OnValidateGradient();
            SetTargetValue(target, this);
        }

        public static implicit operator Texture2D(GradientParam param) => param.m_texture;
    } // class: GradientParam

    [System.Serializable]
    public sealed class Vector4Param : ShaderParam<Vector4>
    {
        public Vector4Param(string propertyName, Vector4 defaultValue = default(Vector4)) : base(propertyName, defaultValue)
        { }

        public override void SetTargetValue(Material target)
        {
            SetTargetValue(target, this);
        }

        public static implicit operator Vector2Param(Vector4Param param) => new Vector2Param(param.propertyName, param.value);
        public static implicit operator Vector4Param(Vector2Param param) => new Vector4Param(param.propertyName, param.value);
    } // class: Vector4Param

    [System.Serializable]
    public sealed class Vector2Param : ShaderParam<Vector2>
    {
        public Vector2Param(string propertyName, Vector2 defaultValue = default(Vector2)) : base(propertyName, defaultValue)
        { }

        public override void SetTargetValue(Material target)
        {
            SetTargetValue(target, this);
        }
    } // class: Vector2Param

    [System.Serializable]
    public sealed class TextureParam : ShaderParam<Texture2D>
    {
        public TextureParam(string propertyName, Texture2D defaultValue = null) : base(propertyName, defaultValue)
        { }

        public override void SetTargetValue(Material target)
        {
            SetTargetValue(target, this);
        }
    } // class: TextureParam
} // namespace