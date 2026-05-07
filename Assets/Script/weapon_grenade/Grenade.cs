using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float timer = 3f;
    float countdown;
    bool hasExploded = false;

    public float radius = 5f;
    public float explosionforce = 700f;

    public float damage = 40f;

    //public GameObject explosionEffect;

    public void Start()
    {
        countdown = timer;
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        //Instantiate(explosionEffect, transform.position, transform.rotation);

        //On obtient tous les objets dans le rayon de l'explosion
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        //On les parcourt tous
        foreach (Collider nearbyObject in colliders)
        {
            //On récupère tous leurs Rigidbody
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionforce, transform.position, radius);
            }
        }

        Destroy(gameObject);
    }
}
