using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropsGenerator : MonoBehaviour
{
    public GameObject Drop;

    public float timeBetweenDrops;

    private float timeSpawn;

    void Start(){
        timeSpawn=0;
    }
    void Update()
    {
        timeSpawn += Time.deltaTime;
        if(timeSpawn > timeBetweenDrops){
            Instantiate(Drop,transform.position,transform.rotation *  Quaternion.Euler(-90,0,0));
            timeSpawn=0;
        }
    }


}
