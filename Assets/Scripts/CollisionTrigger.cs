using UnityEngine.Events;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onTriggerStay;
    [SerializeField] private UnityEvent onTriggerExit;

    [SerializeField] private string triggerTag;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == triggerTag)
        {
            onTriggerEnter?.Invoke();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == triggerTag)
        {
            onTriggerStay?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if( other.tag == triggerTag)
        {
            onTriggerExit?.Invoke();
        }
    }
}
