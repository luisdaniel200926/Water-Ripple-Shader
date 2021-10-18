using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitBounce : MonoBehaviour
{
    public int Maxbounces = 5;
    public int bounces;
    private float previousheight;
    void Start()
    {
        bounces = 0;
    }

    public void Bounced(){
        bounces++;
    }

    // Update is called once per frame
    void Update()
    {
        if(bounces>=Maxbounces){
            Destroy(this.gameObject,0.1f);
        }



    }

    void LateUpdate() {

        
        float currentheight = this.gameObject.transform.position.y;
        float travel = currentheight - previousheight;

        if(travel>0){
            //Cayendo

        }else{
            //Subiendo

        }

        previousheight = currentheight;

    }


}
