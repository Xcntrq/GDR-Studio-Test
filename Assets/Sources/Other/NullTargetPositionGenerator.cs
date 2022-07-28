using System;
using UnityEngine;

public class NullTargetPositionGenerator : ITargetPositionGenerator
{
    public event Action<Vector2> OnTargetPositionGenerated;

    public void SuppressUnityWarning()
    {
        OnTargetPositionGenerated?.Invoke(Vector2.zero);
    }
}
