using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;

    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //calcule camera rotation for looking up and down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        //-80 , 80 sont les min et max de rotation de la camera pour éviter de faire un tour complet
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //Application des calculs à la caméra
        cam.transform.localRotation = Quaternion.Euler(xRotation,0,0);
        //Application des calculs à la rotation du player
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

}
