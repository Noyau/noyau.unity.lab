using Noyau.Lab.Procedural.NoiseFilters;

namespace Noyau.Lab.Procedural
{
    public static class NoiseFilterFactory
    {
        public static NoiseFilter CreateNoiseFilter(NoiseSettings settings)
        {
            switch (settings.noiseMethod)
            {
                case NoiseMethod.Simple:
                    return new SimpleNoiseFilter(settings);
                case NoiseMethod.Rigid:
                    return new RigidNoiseFilter(settings);
            }
            // Note: I don't want to handle "null" or "exceptions" so
            // we just need to return a "SimpleNoiseFilter" as default
            // noise method.
            return new SimpleNoiseFilter(settings);
        }
    } // class: NoiseFilterFactory
} // namespace