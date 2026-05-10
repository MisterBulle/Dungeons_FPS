using UnityEngine;

public class Angel_PowerUp : PowerUp
{
    [Header("Children part")]
    public GameObject WeaponHolder;
    //public GameObject PowerUpParent;

    [Header("PowerUp Settings")]
    public float damagePU;

    public Grenade Grenade;

    private PlayerMotor playerStat;
    private bool effectApplied;

    public override void Apply(GameObject player)
    {
        if (player == null)
        {
            Debug.LogWarning("Angel_PowerUp.Apply: player is null.");
            return;
        }

        playerStat = player.GetComponent<PlayerMotor>();
        if (playerStat == null)
        {
            Debug.LogWarning("Angel_PowerUp.Apply: PlayerMotor introuvable sur le player.");
            return;
        }

        EvaluateGroundedState();
    }

    public override void Initialize(GameObject player)
    {
        base.Initialize(player);

        WeaponSwitching ws = player.GetComponentInChildren<WeaponSwitching>();
        if (ws != null)
        {
            WeaponHolder = ws.gameObject;
        }   

        Grenade = player.GetComponent<Grenade>();
    }


    public override void Tick()
    {
        if (player == null)
            return;

        if (playerStat == null)
            playerStat = player.GetComponent<PlayerMotor>();

        EvaluateGroundedState();
    }

    protected override void CopyTo(PowerUp clone)
    {
        if (clone is Angel_PowerUp target)
        {
            target.WeaponHolder = WeaponHolder;
            target.damagePU = damagePU;
            target.Grenade = Grenade;
        }
    }

    private void EvaluateGroundedState()
    {
        if (playerStat == null)
            return;

        bool grounded = playerStat.isGrounded;
        if (!grounded && !effectApplied)
        {
            ApplyBuff();
        }
        else if (grounded && effectApplied)
        {
            RemoveBuff();
        }
    }

    private void ApplyBuff()
    {
        if (effectApplied)
            return;

        if (Grenade != null)
            Grenade.damage += damagePU;

        UpdateAllGuns(player, damagePU);
        effectApplied = true;
    }

    private void RemoveBuff()
    {
        if (!effectApplied)
            return;

        if (Grenade != null)
            Grenade.damage -= damagePU;

        UpdateAllGuns(player, -damagePU);
        effectApplied = false;
    }

    private void UpdateAllGuns(GameObject player, float damageDelta)
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
            Debug.LogWarning("Angel_PowerUp.UpdateAllGuns: WeaponHolder introuvable.");
            return;
        }

        foreach (Transform weapon in holder)
        {
            Gun gunScript = weapon.GetComponent<Gun>();
            if (gunScript != null)
            {
                gunScript.damage += damageDelta;
            }
        }
    }
}
