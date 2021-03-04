using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other){
        Debug.Log("__________");
        PlayerStats characterStats = other.transform.GetComponent<PlayerStats>();      

        Debug.Log(characterStats.currentHealth);  
    }
}
