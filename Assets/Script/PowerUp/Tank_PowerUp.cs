using UnityEngine;

public class Tank_PowerUp : PowerUp
{
    [Header("Children part")]
    public GameObject WeaponHolder;
    //public GameObject PowerUpParent;

    [Header("Settings Speed")]
    public float Speed = 3f;
    public float MaxSpeed = 5f;
    public float JumpHeight = 1f;

    [Header("Settings Health")]
    public float Health = 200f;

    public override void Apply(GameObject player)
    {
        if (player == null)
        {
            Debug.LogWarning("Tank_PowerUp.Apply: player is null.");
            return;
        }

        PlayerMotor playerStat = player.GetComponent<PlayerMotor>();
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if (playerStat != null)
        {
            playerStat.speed = Speed;
            playerStat.originalSpeed = Speed;
            playerStat.MaxSpeed = MaxSpeed;
            playerStat.JumpHeight = JumpHeight;
            playerStat.dashingPower = 12.5f;
            playerStat.dashingTime = 0.125f;
            playerStat.dashingCooldown = 2f;
        }
        else
        {
            Debug.LogWarning("Tank_PowerUp.Apply: PlayerMotor introuvable sur le player.");
        }

        if (playerHealth != null)
        {
            playerHealth.maxHealth = Health;
            playerHealth.playerhealth = Health;
        }
        else
        {
            Debug.LogWarning("Tank_PowerUp.Apply: PlayerHealth introuvable sur le player.");
        }

        UpgradeAllGun(player);
    }

    protected override void CopyTo(PowerUp clone)
    {
        if (clone is Tank_PowerUp target)
        {
            target.WeaponHolder = WeaponHolder;
            target.Speed = Speed;
            target.MaxSpeed = MaxSpeed;
            target.JumpHeight = JumpHeight;
            target.Health = Health;
        }
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
                gunScript.damage *= 2f;
            }
        }
    }
}
