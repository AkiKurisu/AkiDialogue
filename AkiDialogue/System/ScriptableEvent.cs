using UnityEngine;
using System;
namespace Kurisu.AkiDialogue
{
[CreateAssetMenu(fileName ="ScriptableEvent",menuName ="AkiDialogue/ScriptableEvent")]
public class ScriptableEvent : ScriptableObject
{
    #if UNITY_EDITOR
        [TextArea(0,3)]
        [Tooltip("Editor Only Description For Developer.")]
        public string _devDescription = "";
    #endif
    public event Action OnTriggerEvent;
    public void Trigger()
    {
        OnTriggerEvent?.Invoke();
    }
}
}