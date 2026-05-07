using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }

    //Just for debugging
    [SerializeField]
    private string currentState;
    public Enemy_Path enemy_path;

    private GameObject player;
    public float sightDistance = 20f;
    public float fieldOfView = 85f;

    public float eyeHeight = 0.5f;

    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();

        //Le script va parcourir tous les gameObjects et dès qu'il trouve le premier objet avec ce tag
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            //Si le joueur entre dans le rayon de vision de l'ennemie
            if(Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                //On calcule l'angle du player
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                //Si le joueur est dans le rayon de vision et qu'il est dans l'angle de vision de l'ennemi
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray,out hitInfo, sightDistance))
                    {
                        if(hitInfo.transform.gameObject == player)
                        {
                            //Dessine un raycast entre le joueur et l'ennemi
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    } 
                }
            }
        }
        return false;
    }

}
