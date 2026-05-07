using UnityEngine;

public class StateMachine : MonoBehaviour
{

    public BaseState activeState;

    public void Initialise()
    {
        ChangeState(new PatrolState());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        //check activestate != null
        if (activeState != null)
        {
            //run active cleanup on activestate
            activeState.Exit();
        }
        //change to a new state
        activeState = newState;

        //fail-safe null check to make sure new state wasn't null
        if (activeState != null)
        {
            //Setup new state
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            //assign state enemy state
            activeState.Enter();
        }
    }
}
