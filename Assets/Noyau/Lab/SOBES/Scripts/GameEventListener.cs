using UnityEngine;

namespace Noyau.Lab.SOBES
{
    public abstract class GameEventListener<T> : MonoBehaviour, IGameEventListener<T>
        where T : GameEvent
    {
        protected virtual void OnEnable()
        {
            GameEventSystem<T>.AddListener(this);
        }
        protected virtual void OnDisable()
        {
            GameEventSystem<T>.RemoveListener(this);
        }

        public abstract void OnGameEvent(T sender);
    } // class: GameEventListener
} // namespace