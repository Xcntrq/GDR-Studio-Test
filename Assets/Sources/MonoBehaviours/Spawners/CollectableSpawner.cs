using System;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private SpawnerBase _spawnerBase;
    [SerializeField] private GameStateSwitch _gameStateSwitch;

    private int _collectablesCollected;
    private int _collectableTotalCount;

    public event Action<int, int> OnCountersChanged;

    private void Awake()
    {
        _collectablesCollected = 0;
        _collectableTotalCount = 0;
        _spawnerBase.OnGameObjectGenerated += SpawnerBase_OnGameObjectGenerated;
    }

    private void Start()
    {
        OnCountersChanged?.Invoke(_collectablesCollected, _collectableTotalCount);
        _spawnerBase.SpawnObjects(transform);
    }

    private void OnDestroy()
    {
        _spawnerBase.OnGameObjectGenerated -= SpawnerBase_OnGameObjectGenerated;
    }

    private void SpawnerBase_OnGameObjectGenerated(GameObject newGameObject)
    {
        if (newGameObject.TryGetComponent(out ICollectable collectable))
        {
            _collectableTotalCount++;
            collectable.OnCollected += Collectable_OnCollected;
            OnCountersChanged?.Invoke(_collectablesCollected, _collectableTotalCount);
        }
    }

    private void Collectable_OnCollected()
    {
        _collectablesCollected++;
        OnCountersChanged?.Invoke(_collectablesCollected, _collectableTotalCount);

        if (_collectablesCollected == _collectableTotalCount)
            _gameStateSwitch.Win();
    }
}
