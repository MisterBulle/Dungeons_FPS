using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage;
    public float range;
    public float fireRate;

    private float nextTimeToFire = 0f;

    public float reloadTime;
    private bool isReloading = false;

    [Header("Ammo")]
    public int maxAmmo;
    public int currentTotalAmmo;
     public int maxAmmoPerRifle;
    public int currentAmmo;
    private int AmmoLeftInRifle;

    [Header("References")]
    public Animator animator;
    public Camera camera;

    void Start()
    {
        // Load stats from DataManager
        if (DataManager.Instance != null)
        {
            damage = DataManager.Instance.weaponStats.damage;
            range = DataManager.Instance.weaponStats.range;
            fireRate = DataManager.Instance.weaponStats.fireRate;
            reloadTime = DataManager.Instance.weaponStats.reloadTime;
            maxAmmo = DataManager.Instance.weaponStats.maxAmmo;
            maxAmmoPerRifle = DataManager.Instance.weaponStats.maxAmmoPerRifle;
        }

        currentAmmo = maxAmmoPerRifle;
        currentTotalAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void Update()
    {
        //if (isReloading)
            //return;

        /*if (currentAmmo <= 0)
        {
            if (gameObject.activeInHierarchy)
                StartCoroutine(Reload());
            return;
        }*/
    }

    public void Shoot()
    {
        // Block shooting while reloading
        if (isReloading)
            return;

        //Block shooting if no ammo left
        if (currentAmmo <= 0 && currentTotalAmmo <= 0)
        {
            Debug.Log("Plus de munitions");
            return;
        }

        // Fire rate check
        if (Time.time < nextTimeToFire)
            return;

        // Ammo check - Auto Reload
        if (currentAmmo <= 0 && currentTotalAmmo > 0)
        {
            if (gameObject.activeInHierarchy)
                StartCoroutine(Reload());
            return;
        }

        currentAmmo--;
        nextTimeToFire = Time.time + 1f / fireRate;
        
        RaycastHit hit;
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log(hit.transform.name);
            TakeDamage target = hit.transform.GetComponent<TakeDamage>();
            if (target != null)
            {
                target.TakeDamageFunction(damage);
            }
        }
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        if (currentTotalAmmo <= 0)
        {
            Debug.Log("Plus de munitions à recharger YOU BASTARD");
            isReloading = false;
            yield break;
        }

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);

        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        AmmoLeftInRifle = maxAmmoPerRifle - currentAmmo;

        //Si il reste moins de munitions que celle par chargeur
        if (currentTotalAmmo < maxAmmoPerRifle)
        {
            currentAmmo = currentTotalAmmo;
        }
        else
        {
            currentAmmo = maxAmmoPerRifle;
        }


        currentTotalAmmo -= AmmoLeftInRifle;
        if (currentTotalAmmo < 0)
        {
            currentTotalAmmo = 0;
        }

        isReloading = false;
    }
}
