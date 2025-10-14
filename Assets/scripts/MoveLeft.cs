using UnityEngine;

public class MoveLeft : MonoBehaviour

{
    private float speed=30;
    private PlayerController PlayerControllerScripts;
    private float leftBound= -15;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerControllerScripts=GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()

    { 
        if(PlayerControllerScripts.gameOver==false)
    {

    
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
    if(transform.position.x<leftBound && gameObject.CompareTag("Obstacle"))
    {
Destroy(gameObject);
    }
}
}