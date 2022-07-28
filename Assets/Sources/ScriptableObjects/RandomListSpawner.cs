using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RandomListSpawner : SpawnerBase
{
    [SerializeField] private List<GameObject> _spawnableGameObjects;
    [SerializeField] [Range(1, 50)] private int _desiredAmount;
    [SerializeField] [Range(0f, .5f)] [Tooltip("As a fraction of the height of the screen.")] private float _minDistanceFromPlayer;

    public override event System.Action<GameObject> OnGameObjectGenerated;

    public override void SpawnObjects(Transform parent)
    {
        for (int counter = 0; counter < _desiredAmount; counter++)
        {
            int randomIndex = Random.Range(0, _spawnableGameObjects.Count);

            GameObject newGameObject = Instantiate(_spawnableGameObjects[randomIndex]);

            newGameObject.transform.SetParent(parent);
            Vector3 randomPosition = GetRandomSpawnPosition();
            newGameObject.transform.SetPositionAndRotation(randomPosition, Quaternion.identity);

            OnGameObjectGenerated?.Invoke(newGameObject);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Camera mainCamera = Camera.main;

        var screenBottomRight = new Vector3(Screen.width, 0, 0);
        var screenTopLeft = new Vector3(0, Screen.height, 0);
        var screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        Vector3 cameraBottomLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
        Vector3 cameraBottomRight = mainCamera.ScreenToWorldPoint(screenBottomRight);
        Vector3 cameraTopLeft = mainCamera.ScreenToWorldPoint(screenTopLeft);
        Vector3 cameraScreenCenter = mainCamera.ScreenToWorldPoint(screenCenter);

        cameraScreenCenter.z = 0;

        float xMin = cameraBottomLeft.x;
        float xMax = cameraBottomRight.x;

        float yMin = cameraBottomLeft.y;
        float yMax = cameraTopLeft.y;

        float distanceToClear = _minDistanceFromPlayer * (yMax - yMin);

        int counter = 0;
        while (counter++ < 100)
        {
            float randomX = Random.Range(xMin, xMax);
            float randomY = Random.Range(yMin, yMax);

            var randomPosition = new Vector3(randomX, randomY, 0);

            float distanceFromCenter = Vector3.Distance(randomPosition, cameraScreenCenter);

            if (distanceFromCenter > distanceToClear) return randomPosition;
        }

        return Vector3.zero;
    }
}
