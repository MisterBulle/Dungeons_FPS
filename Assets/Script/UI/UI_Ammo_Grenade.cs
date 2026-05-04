using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


public class UI_Ammo_Grenade : MonoBehaviour
{
    public TMP_Text ammoText;
    public TMP_Text grenadeText;
    public Gun gun;
    public ThrowGrenade grenade;
    public WeaponSwitching weaponSwitching;



    void Update()
    {
        Gun activeGun = GetActiveGun();
        if (activeGun != null)
        {
            ammoText.text = activeGun.currentAmmo + "/" + activeGun.currentTotalAmmo;
        }
        else
        {
            ammoText.text = "0/0";
        }

        //Pour les grenades
        grenadeText.text = grenade.CurrentGrenadeNumber.ToString() + "/" + grenade.MaxGrenade.ToString();
    }

    private Gun GetActiveGun()
    {
        foreach (Transform weapon in weaponSwitching.transform)
        {
            if (weapon.gameObject.activeInHierarchy)
            {
                return weapon.GetComponent<Gun>();
            }
        }
        return null;
    }
}
