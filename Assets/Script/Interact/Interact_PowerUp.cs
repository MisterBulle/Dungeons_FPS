using UnityEngine;

public class Interact_PowerUp : Interactable
{

    public PowerUp powerUp;
    public GameObject player;

    protected override void Interact()
    {
        powerUp.Apply(player);
        powerUp.DestroyPowerUp();
    }
}
