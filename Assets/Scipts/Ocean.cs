using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocean : MonoBehaviour
{
    public Material MaterialToModify;

    public Material MaterialToModifyDrop;

    public bool waveChangeOverTime;
    public float waveAmplitudeChange = 0.00001f;
    private float waveAmplitude = 0.01f;
    private bool waveAmplitudeChanger=false;
    private float maxWaveAmplitude =0.016f;
    private float minWaveAmplitude =0.01f;

    public float wavePeriodChange = 0.001f;
    private float wavePeriod = 1f;
    private bool wavePeriodChanger=false;

    private float maxWavePeriod =2f;
    private float minWavePeriod =1f;

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
        // 10% chance to change amplitude and period per frame
        //This changes amplitude and period for it to not look the same all the time
        int execute = Random.Range(1,100);
        if(execute <= 10 && waveChangeOverTime){

            CheckWaveAmplitude();
            CheckWavePeriod();

            MaterialToModify.SetFloat("_WaveAmplitude",waveAmplitude);
            MaterialToModify.SetFloat("_WavePeriod",wavePeriod);

        }

        rippleAmplitude = MaterialToModify.GetFloat("_RippleAmplitude");
        //Lower the amplitude of the ripple for it to fade away
        if(rippleAmplitude>0){
            rippleAmplitude = rippleAmplitude *.99f;
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
        Mesh mesh = GetComponent<MeshFilter>().mesh;

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
            if(waveAmplitude>=maxWaveAmplitude){
                waveAmplitudeChanger=false;
            }else if(waveAmplitude<=minWaveAmplitude){
                waveAmplitudeChanger=true;
            }
            if(waveAmplitudeChanger){
                waveAmplitude += waveAmplitudeChange;
            }else{
                waveAmplitude -= waveAmplitudeChange;
            }
    }


    void CheckWavePeriod(){

            if(wavePeriod>=maxWavePeriod){
               wavePeriodChanger=false;
            }else if(wavePeriod<=minWavePeriod){
                wavePeriodChanger=true;
            }
            
            if(wavePeriodChanger){
                wavePeriod += wavePeriodChange;
            }else{
               wavePeriod -= wavePeriodChange;
            }
            
    }

}
