using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    private float nextTimeToFire = 0f;

    // Ammo system
    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;

    public Animator animator;
    public Camera camera;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            if (gameObject.activeInHierarchy)
                StartCoroutine(Reload());
            return;
        }
    }

    public void Shoot()
    {
        // Fire rate check
        if (Time.time < nextTimeToFire)
            return;

        // Ammo check
        if (currentAmmo <= 0)
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

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);

        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }
}
