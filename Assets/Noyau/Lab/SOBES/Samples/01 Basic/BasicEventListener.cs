using UnityEngine;

namespace Noyau.Lab.SOBES.Samples._01_Basic
{
    [DisallowMultipleComponent, SelectionBase]
    public sealed class BasicEventListener : GameEventListener<BasicEvent>
    {
        public override void OnGameEvent(BasicEvent sender)
        {
            float _realtime = Time.realtimeSinceStartup;
            Debug.Log($"({_realtime}:{sender.time})\n{sender.message}");
        }
    } // class: BasicListener
} // namespace