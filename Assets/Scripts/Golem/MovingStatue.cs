using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MovingStatue : MonoBehaviour
{
    public NavMeshAgent ai;
    
    public Transform player;
    
    Vector3 _dest;
    
    public Camera playerCam, jumpscareCam;
    
    public float aiSpeed;
    
    public float catchDistance;
    
    public float jumpscareTime;
    
    public string sceneAfterDeath;
    
    void Update()
    {
 
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);

  
        float distance = Vector3.Distance(transform.position, player.position);

   
        if(GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            ai.speed = 0;
            ai.SetDestination(transform.position);
        }
        
        if (!GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            ai.speed = aiSpeed;
            _dest = player.position;
            ai.destination = _dest;
            
            if(distance <= catchDistance)
            {
                player.gameObject.SetActive(false);
                jumpscareCam.gameObject.SetActive(true);
                Destroy(GameManager.Player);
            }
        }
    }
}