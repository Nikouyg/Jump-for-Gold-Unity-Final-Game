using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos= new Vector3(25,0,0);
    private float startDelay=2;
    private float repeatRate=2;
    private PlayerController PlayerControllerScripts;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerControllerScripts=GameObject.Find("Player").GetComponent<PlayerController>();
           InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    void SpawnObstacle()
    {
        if(PlayerControllerScripts.gameOver==false){
            Instantiate(obstaclePrefab,spawnPos,obstaclePrefab.transform.rotation);


        }
    }
}
