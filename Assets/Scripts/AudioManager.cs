using System.Runtime.Versioning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] audioList;
    public static AudioSource buttonClick, bgm, jump, fall, coinCollect, complete, breakSound;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadAudio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Prepare all audio to be used in the program
    void LoadAudio(){
        gameObject.AddComponent<AudioSource>();
        gameObject.AddComponent<AudioSource>();
        gameObject.AddComponent<AudioSource>();
        gameObject.AddComponent<AudioSource>();
        gameObject.AddComponent<AudioSource>();
        gameObject.AddComponent<AudioSource>();
        gameObject.AddComponent<AudioSource>();
        audioList = gameObject.GetComponents<AudioSource>();
        audioList[0].clip = (AudioClip)Resources.Load("Audio/buttonClick");
        audioList[1].clip = (AudioClip)Resources.Load("Audio/track_shortadventure_loop");
        audioList[2].clip = (AudioClip)Resources.Load("Audio/jump");
        audioList[3].clip = (AudioClip)Resources.Load("Audio/fall");
        audioList[4].clip = (AudioClip)Resources.Load("Audio/collectCoin");
        audioList[5].clip = (AudioClip)Resources.Load("Audio/level-completed");
        audioList[6].clip = (AudioClip)Resources.Load("Audio/break");
        
        for(int i = 0; i < audioList.Length; i++){
            gameObject.GetComponents<AudioSource>()[i] = audioList[i];
        }

        buttonClick = gameObject.GetComponents<AudioSource>()[0];
        bgm = gameObject.GetComponents<AudioSource>()[1];
        jump = gameObject.GetComponents<AudioSource>()[2];
        fall = gameObject.GetComponents<AudioSource>()[3];
        coinCollect = gameObject.GetComponents<AudioSource>()[4];
        complete = gameObject.GetComponents<AudioSource>()[5];
        breakSound = gameObject.GetComponents<AudioSource>()[6];

        // Any required adjustments to audio (volume, tone, etc.)
        jump.volume = 0.5f;
    }
}
