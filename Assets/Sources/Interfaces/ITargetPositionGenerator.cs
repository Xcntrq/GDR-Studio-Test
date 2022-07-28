using System;
using UnityEngine;

public interface ITargetPositionGenerator
{
    public event Action<Vector2> OnTargetPositionGenerated;
}
