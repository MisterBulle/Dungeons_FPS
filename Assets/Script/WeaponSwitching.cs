using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;

    void Start()
    {
        SelectedWeapon();
    }

    void SelectedWeapon()
    {
        int i = 0;
        //On regarde tous les enfants de WeaponHolder
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    public void WeaponSwitchUp_Function()
    {
        if (selectedWeapon >= transform.childCount - 1)
            selectedWeapon = 0;
        else
            selectedWeapon++;
        SelectedWeapon();
    }

    public void WeaponSwitchDown_Function()
    {
        if (selectedWeapon <= 0)
            selectedWeapon = transform.childCount - 1;
        else
            selectedWeapon--;
        SelectedWeapon();
    }

    public void Weapon_Number_Key(int weaponNumber)
    {
        if(transform.childCount >= weaponNumber + 1) 
        {
            selectedWeapon = weaponNumber;
            SelectedWeapon();
        }
    }
}
