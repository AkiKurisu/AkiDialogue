using UnityEngine;
namespace Kurisu.AkiDialogue.Example
{
public class ScriptableEventListener : MonoBehaviour
{
    [SerializeField]
    private ScriptableEvent Event;
    [SerializeField]
    private GameObject Object;
    void Start()
    {
        Event.OnTriggerEvent+=ShowObject;
    }
    private void OnDestroy() {
        Event.OnTriggerEvent-=ShowObject;
    }

    private void ShowObject()
    {
        Object.SetActive(true);
    }
}
}