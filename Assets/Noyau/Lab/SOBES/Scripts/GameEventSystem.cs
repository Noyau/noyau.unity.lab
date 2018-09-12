using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Noyau.Lab.SOBES
{
    public sealed class GameEventSystem : MonoBehaviour, IComparer<GameEvent>
    {
        [SerializeField] private bool m_startListeningOnAwake = true;
        [SerializeField] private List<GameEvent> m_registeredEvents = new List<GameEvent>();

        [Space]
        [SerializeField] private UnityEvent m_onStartListening = new UnityEvent();
        [SerializeField] private UnityEvent m_onStopListening = new UnityEvent();

        public event UnityAction onStartListening
        {
            add { m_onStartListening.AddListener(value); }
            remove { m_onStartListening.RemoveListener(value); }
        }
        public event UnityAction onStopListening
        {
            add { m_onStopListening.AddListener(value); }
            remove { m_onStopListening.RemoveListener(value); }
        }

        #region Game Event Comparer
        public int Compare(GameEvent x, GameEvent y)
        {
            return x.time.CompareTo(y.time);
        }
        #endregion

        private void Awake()
        {
            if (m_startListeningOnAwake)
                StartListening();
        }

        public void StartListening()
        {
            StartListening(Time.realtimeSinceStartup);
        }
        public void StartListening(float startTime)
        {
            StopAllCoroutines();
            StartCoroutine(StartListeningCoroutine(startTime));
        }

        private IEnumerator StartListeningCoroutine(float startTime)
        {
            // Sort events by "time" ascending
            m_registeredEvents.Sort();

            // Start listening
            m_onStartListening.Invoke();

            // TODO implement queueing system
            float _time;
            GameEvent _gameEvent;
            for (int i = 0; i < m_registeredEvents.Count; ++i)
            {
                _time = Time.realtimeSinceStartup - startTime;
                _gameEvent = m_registeredEvents[i];
                yield return _gameEvent.WaitForTime(_time);
                _gameEvent.Raise();
            }

            // Stop listening
            m_onStopListening.Invoke();
        }
    } // class: GameEventSystem

    public static class GameEventSystem<T> where T : GameEvent
    {
        private static readonly List<IGameEventListener<T>> s_listeners = new List<IGameEventListener<T>>();

        public static void AddListener(IGameEventListener<T> listener)
        {
            if (listener != null && !s_listeners.Contains(listener))
                s_listeners.Add(listener);
        }
        public static void RemoveListener(IGameEventListener<T> listener)
        {
            s_listeners.Remove(listener);
        }
        public static void RemoveAllListeners()
        {
            s_listeners.Clear();
        }

        public static void NotifyAll(T gameEvent)
        {
            if (gameEvent == null)
                throw new System.ArgumentNullException();

            for (int i = s_listeners.Count - 1; i >= 0; --i)
                s_listeners[i]?.OnGameEvent(gameEvent);
        }
    } // class: GameEventSystem<T>
} // namespace