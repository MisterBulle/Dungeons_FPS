using UnityEngine;

public class Angel_PowerUp : PowerUp
{
    public override void Apply(GameObject player)
    {
        PlayerMotor playerMotor = player.GetComponent<PlayerMotor>();
        playerMotor.speed += 2f;
    }
}
