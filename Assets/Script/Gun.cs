using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    private float nextTimeToFire = 0f;

    public Camera camera;


    public void Shoot()
    {
        // Fire rate check
        if (Time.time < nextTimeToFire)
            return;
        
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
}
