using UnityEngine;

public class DefaultSpawner : MonoBehaviour
{
    [SerializeField] private SpawnerBase _spawnerBase;

    private void Start()
    {
        _spawnerBase.SpawnObjects(transform);
    }
}
