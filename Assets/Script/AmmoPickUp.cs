using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{

    public Gun gun;
    public int ammoAmount = 30;

    public void Collect()
    {
        Debug.Log("Ammo collected!");
        gun.currentTotalAmmo += ammoAmount;
        Destroy(gameObject);
    }
}
