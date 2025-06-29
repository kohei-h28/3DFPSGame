using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    public Transform player;//playerのtransfoamをアサイン
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            agent.SetDestination(player.position);//Playerの位置に向かう
        }
    }
}
