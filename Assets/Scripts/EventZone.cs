using UnityEngine;
using UnityEngine.Events;

public class EventZone : MonoBehaviour
{
    [SerializeField] private UnityEvent _onEnter;

    private void OnTriggerEnter2D(Collider2D col)
    {
        _onEnter.Invoke();
    }
}