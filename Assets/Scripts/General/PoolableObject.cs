using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    protected ObjectPool pool;

    public void setPool(ObjectPool pool) {
        this.pool = pool;
    }

    protected void OnDisable() {
        if (pool != null) {
            pool.addToPool(this);
        }
    }

    public abstract void loadReferences();
}
