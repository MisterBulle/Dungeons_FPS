using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{

    public Gun gun;
    private int a;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision avec : " + other.tag);
        a = gun.currentTotalAmmo;

        if (other.CompareTag("Player"))
        {
        Debug.Log("AAAA");
            
            if (a == gun.maxAmmo)
            {
                return;
            }
            else
            {
                a = gun.currentTotalAmmo += gun.maxAmmoPerRifle;
                Destroy(gameObject);
            }

            //Pour ne pas qu'il dépasse son montant max de munitions
            
            if (a > gun.maxAmmo)
            {
                gun.currentTotalAmmo = gun.maxAmmo;
                //Destroy(gameObject);
            }
            /*if (gun.maxAmmo != gun.currentTotalAmmo)
            {
                Destroy(gameObject);;
            }*/
        }
    }
}
