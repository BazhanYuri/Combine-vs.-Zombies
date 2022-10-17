using Sirenix.OdinInspector;
using System;

public class StorageManager : StorageManagerBase
{
    public static event Action onCoinsAmountChanged;

    [Title("Currency")]
    [ShowInInspector, PropertyOrder(0)]
    public int CoinsAmount
    {
        get
        {
            return GetCollectable(eCollectable.Coin);
        }

        set
        {
            onCoinsAmountChanged?.Invoke();
            SetCollectable(eCollectable.Coin, value);
        }
    }
}
