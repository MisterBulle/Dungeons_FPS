using UnityEngine;

public class PatrolState : BaseState
{

    //Check which waypoints index we targeting
    public int waypointsIndex;
    public float waitTimer;

    public override void Enter()
    {
        
    }

    public override void Perform()
    {
        PatrolCycle();
    }

    public override void Exit()
    {
        
    }

    public void PatrolCycle()
    {
        //implement patrol logic
        if(enemy.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 3)
            {
                if(waypointsIndex < enemy.enemy_path.waypoints.Count - 1)
                    waypointsIndex++;
            //On a atteint la fin
                else
                    waypointsIndex = 0;

                enemy.Agent.SetDestination(enemy.enemy_path.waypoints[waypointsIndex].position); 
                waitTimer = 0;  
            }
        }
    }
}
