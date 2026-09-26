using System;
using UnityEngine;

public class CollisionBroadcaster : MonoBehaviour
{
    public static event Action OnEntitiesCollided;

    private void OnCollisionEnter(Collision collision)
    {
        OnEntitiesCollided?.Invoke();
    }
}