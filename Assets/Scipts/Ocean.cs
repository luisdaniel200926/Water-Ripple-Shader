using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour
{
    public Material MaterialToModify;

    public bool waveChangeOverTime;
    public float waveAmplitudeChange = 0.00001f;
    private float waveAmplitude = 0.01f;
    private bool waveAmplitudeChanger=false;

    public float wavePeriodChange = 0.001f;
    private float wavePeriod = 1f;
    private bool wavePeriodChanger=false;

    public float rippleMagnifier;
    float rippleAmplitude;

    float distance;
    public float speedRippleSpread;

    Vector2 impactPos;
    void Start()
    {
        
        rippleAmplitude = 0;
        MaterialToModify.SetFloat("_RippleAmplitude",rippleAmplitude);
    }

    void Update()
    {

        int execute = Random.Range(1,100);
        if(execute <= 10 && waveChangeOverTime){

            CheckWaveAmplitude();
            CheckWavePeriod();

            MaterialToModify.SetFloat("_WaveAmplitude",waveAmplitude);
            MaterialToModify.SetFloat("_WavePeriod",wavePeriod);

        }

        rippleAmplitude = MaterialToModify.GetFloat("_RippleAmplitude");

        if(rippleAmplitude>0){
            rippleAmplitude = rippleAmplitude *0.99f;
            distance += speedRippleSpread;
            MaterialToModify.SetFloat("_Distance",distance);
        }
        if(rippleAmplitude<0.0001){
            rippleAmplitude=0;
            distance = 0;
        }
        MaterialToModify.SetFloat("_RippleAmplitude",rippleAmplitude);

        
    }
    private void OnCollisionEnter(Collision other){
        Debug.Log("Collision");
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        if(other.gameObject.GetComponent<LimitBounce>())
        other.gameObject.GetComponent<LimitBounce>().Bounced();

        float velocity = other.gameObject.GetComponent<Rigidbody>().velocity.magnitude;

        float disX = transform.position.x - other.transform.position.x;
        float disZ = transform.position.z - other.transform.position.z;
        distance = 0;
        impactPos.x = other.transform.position.x;
        impactPos.y = other.transform.position.z;

        MaterialToModify.SetFloat("_ImpactX",impactPos.x);
        MaterialToModify.SetFloat("_ImpactZ",impactPos.y);

        MaterialToModify.SetFloat("_OffSetX",disX/mesh.bounds.size.x * 2.5f);
        MaterialToModify.SetFloat("_OffSetZ",disZ/mesh.bounds.size.z* 2.5f);
        rippleAmplitude = velocity * rippleMagnifier;

        MaterialToModify.SetFloat("_RippleAmplitude",rippleAmplitude);

    }

    void CheckWaveAmplitude(){
            if(waveAmplitude>=0.016f){
                waveAmplitudeChanger=false;
            }else if(waveAmplitude<=0.01f){
                waveAmplitudeChanger=true;
            }
            
            if(waveAmplitudeChanger){
                waveAmplitude += waveAmplitudeChange;
            }else{
                waveAmplitude -= waveAmplitudeChange;
            }
    }


    void CheckWavePeriod(){

            if(wavePeriod>=2f){
               wavePeriodChanger=false;
            }else if(wavePeriod<=1f){
                wavePeriodChanger=true;
            }
            
            if(wavePeriodChanger){
                wavePeriod += wavePeriodChange;
            }else{
               wavePeriod -= wavePeriodChange;
            }
            
    }

}
