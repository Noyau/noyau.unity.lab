namespace Noyau.Lab.Procedural
{
    public abstract class Generator<T> where T : ISettings
    {
        protected T m_settings;

        public virtual void UpdateSettings(T settings)
        {
            m_settings = settings;
        }
    } // class: Generator
} // namespace