using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;

    public override void Enter()
    {
        
    }

    public override void Perform()
    {
        if(enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            //Time before shot
            if (shotTimer > enemy.fireRate)
            {
                Shoot();
            }
            //Move enemy to random location between 3 and 7
            if (moveTimer > Random.Range(3,7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            //Wait before search time
            if (losePlayerTimer > 8)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public void Shoot()
    {
        Transform gunbarrel = enemy.gunBarrel;
        //Instanciate a new bullet à la position du gunbarrel positon 
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunbarrel.position, enemy.transform.rotation);
        //calculate the direction to the player
        Vector3 shootDirection = (enemy.Player.transform.position - gunbarrel.transform.position).normalized;
        //Add force to the bullet
        //The higher value of random range the less accurate the enemy will be
        bullet.GetComponent<Rigidbody>().linearVelocity = Quaternion.AngleAxis(Random.Range(-3f,3f),Vector3.up) * shootDirection * enemy.bulletSpeed;
        Debug.Log("Shoot");
        shotTimer = 0;
    }

    public override void Exit()
    {
        
    }
}
