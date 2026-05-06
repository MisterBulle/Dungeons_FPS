using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [Header("PowerUp Parent Settings")]
    public string title;
    public string description;
    //Pour qu'il puisse le détruire
    public GameObject parent;

    public void DestroyPowerUp()
    {
        Destroy(gameObject);
    }
    public abstract void Apply(GameObject player);
}
