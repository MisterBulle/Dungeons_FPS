using UnityEngine;

public class Angel_PowerUp : PowerUp
{

    [Header("Children part")]
    public GameObject WeaponHolder;
    //public GameObject PowerUpParent;

    [Header ("PowerUp Settings")]
    public float damagePU;
    public bool isGrounded;

    public Grenade Grenade;

    private PlayerMotor playerStat;

    public override void Apply(GameObject player)
    {
        Grenade.damage += damagePU;

        playerStat = player.GetComponent<PlayerMotor>();

        if (playerStat != null)
        {
            isGrounded = playerStat.isGrounded;
        }
        else
        {
            Debug.LogWarning("Tank_PowerUp.Apply: PlayerMotor introuvable sur le player.");
        }

        UpgradeAllGun(player);
    }

    void Update()
    {
        isGrounded = playerStat.isGrounded;
    }


    public void UpgradeAllGun(GameObject player)
    {
        Transform holder = WeaponHolder ? WeaponHolder.transform : null;

        if (holder == null)
        {
            WeaponSwitching ws = player.GetComponentInChildren<WeaponSwitching>();
            if (ws != null)
                holder = ws.transform;
        }

        if (holder == null)
        {
            Debug.LogWarning("Tank_PowerUp.UpgradeAllGun: WeaponHolder introuvable.");
            return;
        }

        foreach (Transform weapon in holder)
        {
            Gun gunScript = weapon.GetComponent<Gun>();
            if (gunScript != null)
            {
                Debug.Log("Upgrade de l'arme : " + weapon.name);
                gunScript.damage += damagePU;
            }
        }
    }
}
