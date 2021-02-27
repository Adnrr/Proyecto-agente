using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : FunctionsFSM
{
    float nextFire;
    float rateFire = 1f;
    public override void EnterState(AgenteEstatico agent)
    {
        //Código para cuando entra al estado de atacar
        Debug.Log("Estado de Atacar");
        agent.agentStatus = AgentState.Attacking;

        //agent.FireBullet();
        nextFire = Time.time + rateFire;
    }

    public override void UpdateState(AgenteEstatico agent)
    {
        //Código para cuando no se detecta al jugador
        if (agent.targetDetected)
        {
            var lookRotation = Quaternion.LookRotation(agent.targetOdj.transform.position - agent.transform.position);
            agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, agent.speedRotation * Time.deltaTime);
            if (nextFire < Time.time)
            {
                agent.FireBullet();
                nextFire = Time.time + rateFire;

            }
        }
        else
        {
            agent.TransitionToState(agent.idleState);
        }
    }
}
