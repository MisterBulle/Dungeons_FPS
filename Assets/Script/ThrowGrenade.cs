using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public float ThrowForce = 40f;
    public GameObject GrenadePrefab;
    public Vector3 offset = new Vector3(0, 0, 1f);

    public void ThrowGrenadeFunction()
    {
        GameObject grenade = Instantiate(GrenadePrefab, transform.position + offset, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * ThrowForce, ForceMode.VelocityChange);
    }
}
