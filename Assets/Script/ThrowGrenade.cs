using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public float ThrowForce = 40f;

    public int MaxGrenade = 3;
    int CurrentGrenadeNumber; 



    public GameObject GrenadePrefab;
    public Vector3 offset = new Vector3(0, 0, 1f);

    public Camera camera;

    void Start()
    {
        CurrentGrenadeNumber = MaxGrenade;
    }

    public void ThrowGrenadeFunction()
    {
        if (CurrentGrenadeNumber > 0)
        {
            GameObject grenade = Instantiate(GrenadePrefab, camera.transform.position + offset, camera.transform.rotation);
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.AddForce(camera.transform.forward * ThrowForce, ForceMode.VelocityChange);
            CurrentGrenadeNumber--;
        }
        else
        {
            Debug.Log("plus de grenades");
        }
    }
}
