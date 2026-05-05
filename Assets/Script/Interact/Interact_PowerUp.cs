using UnityEngine;

public class Interact_PowerUp : Interactable
{

    public Tank_PowerUp Tank_powerUp;
    public GameObject player;

    protected override void Interact()
    {
        Tank_powerUp.Apply(player);
        Tank_powerUp.DestroyPowerUp();
    }
}
