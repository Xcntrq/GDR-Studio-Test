using System;

public interface ICollectable
{
    public event Action OnCollected;

    public void GetCollected();
}
