using UnityEngine;

public class BBB_PowerUp : PowerUp
{
    [Header("Children part")]
    public GameObject WeaponHolder;

    [Header("PowerUp Settings")]
    [Tooltip("Multiplier de dégâts appliqué lorsque currentAmmo == 4")]
    public float damagePU = 2f;

    private Gun boostedGun;
    private float originalDamage;

    public override void Apply(GameObject player)
    {
        if (player == null)
        {
            Debug.LogWarning("BBB_PowerUp.Apply: player is null.");
            return;
        }

        BonusBBB();
    }

    public override void Tick()
    {
        BonusBBB();
    }

    private void BonusBBB()
    {
        if (player == null)
            return;

        Gun activeGun = GetActiveGun(player);
        if (activeGun == null)
        {
            RestoreBoost();
            return;
        }

        if (activeGun.currentAmmo < 4)
        {
            if (boostedGun != activeGun)
            {
                RestoreBoost();
                boostedGun = activeGun;
                originalDamage = activeGun.damage;
                boostedGun.damage = originalDamage * damagePU;
            }
        }
        else
        {
            RestoreBoost();
        }
    }

    private Gun GetActiveGun(GameObject player)
    {
        Transform holder = WeaponHolder ? WeaponHolder.transform : null;
        if (holder == null)
        {
            WeaponSwitching ws = player.GetComponentInChildren<WeaponSwitching>();
            if (ws != null)
                holder = ws.transform;
        }

        if (holder == null)
            return null;

        foreach (Transform weapon in holder)
        {
            if (!weapon.gameObject.activeSelf)
                continue;

            Gun gunScript = weapon.GetComponent<Gun>();
            if (gunScript != null)
                return gunScript;
        }

        return null;
    }

    private void RestoreBoost()
    {
        if (boostedGun == null)
            return;

        if (boostedGun != null)
            boostedGun.damage = originalDamage;

        boostedGun = null;
    }

    protected override void CopyTo(PowerUp clone)
    {
        if (clone is BBB_PowerUp target)
        {
            target.WeaponHolder = WeaponHolder;
            target.damagePU = damagePU;
        }
    }
}
