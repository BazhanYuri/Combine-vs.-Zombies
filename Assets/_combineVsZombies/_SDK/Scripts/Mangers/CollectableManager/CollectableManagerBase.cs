using Sirenix.OdinInspector;
using UnityEngine;

public class CollectableManagerBase : Singleton<CollectableManager>
{
    [SerializeField, ReadOnly] protected CollectableWallet[] m_CollectableWallets;

    #region Editor
    [Button]
    protected virtual void setRefs()
    {
        m_CollectableWallets = GetComponentsInChildren<CollectableWallet>();
    }

    private void OnValidate()
    {
        setRefs();
    }
    #endregion

    public CollectableWallet GetWallet(eCollectable collectable)
    {
        foreach (var wallet in m_CollectableWallets)
        {
            if (wallet.CollectableType == collectable)
                return wallet;
        }

        Debug.LogError("Collectable wallet wasn't found!");
        return null;
    }
}
