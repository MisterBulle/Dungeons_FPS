using UnityEngine;

public class CursorLock : MonoBehaviour
{
    public bool isCursorLock = true;
    void Start()
    {
        if (isCursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }
}
