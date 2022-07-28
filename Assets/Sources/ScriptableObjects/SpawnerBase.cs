using System;
using UnityEngine;

public abstract class SpawnerBase : ScriptableObject
{
    public abstract event Action<GameObject> OnGameObjectGenerated;

    public abstract void SpawnObjects(Transform parent);
}
