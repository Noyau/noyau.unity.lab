using UnityEngine;

namespace Noyau.Lab.SOBES.Samples._01_Basic
{
    [CreateAssetMenu(menuName = "Lab/SOBES/Sample/Basic Event")]
    public sealed class BasicEvent : GameEvent
    {
        [SerializeField, TextArea(3, 5)] private string m_message = string.Empty;

        public string message => m_message;

        public override void Raise()
        {
            GameEventSystem<BasicEvent>.NotifyAll(this);
        }
    } // class: BasicEvent
} // namespace