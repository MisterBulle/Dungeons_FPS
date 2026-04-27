using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{

    public Gun gun;
    private int a;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision avec : " + other.tag);

        if (other.CompareTag("Player"))
        {
            a = gun.currentTotalAmmo += gun.maxAmmoPerRifle;
            //Pour ne pas qu'il dépasse son montant max de munitions
            if (a > gun.maxAmmo)
            {
                gun.currentTotalAmmo = gun.maxAmmo;
            }
            if (gun.maxAmmo != gun.currentTotalAmmo)
            {
                Destroy(gameObject);;
            }
        }
    }
}
