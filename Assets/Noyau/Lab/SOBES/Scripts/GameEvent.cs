using System.Collections;
using UnityEngine;

namespace Noyau.Lab.SOBES
{
    public abstract class GameEvent : ScriptableObject
    {
        [SerializeField] protected float m_time = 0F;

        public float time => m_time;

        protected virtual void OnValidate()
        {
            m_time = Mathf.Max(m_time, 0F);
        }

        public IEnumerator WaitForTime(float currentTime)
        {
            float _delay = time - currentTime;

            if (_delay > 0F)
                yield return new WaitForSeconds(_delay);
        }

        public abstract void Raise(); // => GameEventSystem<T>.NotifyAll(this);
    } // class: GameEvent
} // namespace