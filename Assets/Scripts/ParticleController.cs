using System;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private float activeTime;

    private void OnEnable()
    {
        Invoke(nameof(Deactivate), activeTime);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
