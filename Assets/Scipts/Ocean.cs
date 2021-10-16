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

    public float waveSpeedChange = 0.001f;
    private float waveSpeed = 1f;
    private bool waveSpeedChanger=false;

    public float wavePeriodChange = 0.001f;
    private float wavePeriod = 1f;
    private bool wavePeriodChanger=false;
    void Start()
    {
        
        
    }


    void Update()
    {

        int execute = Random.Range(1,100);
        if(execute <= 10 && waveChangeOverTime){

            CheckWaveAmplitude();
            //CheckWaveSpeed();
            CheckWavePeriod();

            MaterialToModify.SetFloat("_WaveAmplitude",waveAmplitude);
            //MaterialToModify.SetFloat("_WaveSpeed",waveSpeed);
            MaterialToModify.SetFloat("_WavePeriod",wavePeriod);

        }

        
    }
    private void OnCollisionEnter(Collision other){

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

    void CheckWaveSpeed(){

            if(waveSpeed>=1.5f){
               waveSpeedChanger=false;
            }else if(waveSpeed<=0.6f){
                waveSpeedChanger=true;
            }
            
            if(waveSpeedChanger){
                waveSpeed += waveSpeedChange;
            }else{
               waveSpeed -= waveSpeedChange;
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