using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [Header("PowerUp Parent Settings")]
    public string title;
    public string description;
    // Pour qu'il puisse le détruire
    public GameObject parent;

    [HideInInspector]
    public GameObject player;

    public void DestroyPowerUp()
    {
        Destroy(gameObject);
    }

    public void Initialize(GameObject player)
    {
        this.player = player;
    }

    public PowerUp Clone(GameObject destination)
    {
        PowerUp clone = (PowerUp)destination.AddComponent(GetType());
        clone.title = title;
        clone.description = description;
        clone.parent = parent;
        CopyTo(clone);
        return clone;
    }

    protected virtual void CopyTo(PowerUp clone)
    {
        // Derived classes override this to copy their own fields.
    }

    public abstract void Apply(GameObject player);

    public virtual void Tick()
    {
        // Called every frame by PowerUpManager.
    }

    public virtual bool ShouldRemove => false;
}
