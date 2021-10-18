using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public Material MaterialToModifyDrop;
    
    private float dropSize;
    public float dropSizeChange;

    public int Maxbounces = 3;
    public int bounces;
    private float previousheight;
    void Start()
    {
        dropSize = 1;
        bounces = 0;
        MaterialToModifyDrop.SetFloat("_Size",1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(bounces>=Maxbounces){
            Destroy(this.gameObject);
        }
        float dir = GetComponent<Rigidbody>().velocity.y;
        if(dir>0){
            MaterialToModifyDrop.SetFloat("_Direction",-1);
        }else{
            MaterialToModifyDrop.SetFloat("_Direction",1);
        }
    }

    private void OnCollisionEnter(Collision other){
        dropSize += dropSizeChange;
        MaterialToModifyDrop.SetFloat("_Size",dropSize);
        bounces++;
    }


}
