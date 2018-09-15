namespace Noyau.Lab.SOBES
{
    public interface IGameEventListener<in T> where T : GameEvent
    {
        void OnGameEvent(T sender);
    } // interface: IGameEventListener<T>
} // namespace