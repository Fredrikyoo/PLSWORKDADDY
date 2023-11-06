using UnityEngine;

public class Death : MonoBehaviour
{
    private bool dead = false;
    public GameObject bullet;
    void OnTriggerEnter(Collider bullet){
        if(dead == false){
            Debug.Log("kill");
            transform.Rotate(-90, 0, 0);
            transform.Translate(0,0,0.2f);
            dead = true;
        }
    }
}
