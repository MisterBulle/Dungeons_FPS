using UnityEngine;

public class BROZEN_PowerUp : PowerUp
{
    [Header("Children part")]

    [Header("Settings Speed")]
    public float Speed = 7.5f;
    public float MaxSpeed = 7.5f;
    public float JumpHeight = 1f;

    [Header("Settings Health")]
    public float Health = 75f;

    public override void Apply(GameObject player)
    {
        if (player == null)
        {
            Debug.LogWarning("BROZEN_PowerUp.Apply: player is null.");
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
            playerStat.dashingPower = 10f;
            playerStat.dashingTime = 0.25f;
            playerStat.dashingCooldown = 0.5f;
        }
        else
        {
            Debug.LogWarning("BROZEN_PowerUp.Apply: PlayerMotor introuvable sur le player.");
        }

        if (playerHealth != null)
        {
            playerHealth.maxHealth = Health;
            playerHealth.playerhealth = Health;
        }
        else
        {
            Debug.LogWarning("BROZEN_PowerUp.Apply: PlayerHealth introuvable sur le player.");
        }

        // UpgradeAllGun(player);
    }

    public override void Initialize(GameObject player)
    {
        base.Initialize(player);

        //WeaponSwitching ws = player.GetComponentInChildren<WeaponSwitching>();
        //if (ws != null)
        //{
        //    WeaponHolder = ws.gameObject;
        //}
    }

    protected override void CopyTo(PowerUp clone)
    {
        if (clone is BROZEN_PowerUp target)
        {
            //target.WeaponHolder = WeaponHolder;
            target.Speed = Speed;
            target.MaxSpeed = MaxSpeed;
            target.JumpHeight = JumpHeight;
            target.Health = Health;
        }
    }
}
