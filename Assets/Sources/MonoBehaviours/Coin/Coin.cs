using System;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public event Action OnCollected;

    public void GetCollected()
    {
        OnCollected?.Invoke();
        Destroy(gameObject);
    }
}
