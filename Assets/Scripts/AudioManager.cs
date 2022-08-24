using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioSource[] audioList;
    public static AudioSource buttonClick, bgm, jump, fall, forestBgm, complete, footstep, plainsTheme, towerTheme, ironMtTheme;
    public static AudioSource currentAudio = null;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadAudio();
    }

    // Update is called once per frame
    void Update()
    {
        // Refactored audio - let the audio manager handle the bgms
        // Starting menus bgm
        if(SceneManager.GetActiveScene().buildIndex < 3 && currentAudio != bgm){
            if(currentAudio != null){
                currentAudio.Stop();
            }
            bgm.loop = true;
            currentAudio = bgm;
            currentAudio.Play();
        }
        // Plains themes - Fettuccine Plains, Waxwing Mountain, and Maillo Shores
        else if(SceneManager.GetActiveScene().buildIndex == 3 && currentAudio != plainsTheme){
            currentAudio.Stop();
            plainsTheme.loop = true;
            plainsTheme.volume = 0.4f;
            currentAudio = plainsTheme;
            currentAudio.Play();
        }
        // Tower theme - Witcher's Tower
        else if((SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 6) && currentAudio != towerTheme){
            currentAudio.Stop();
            towerTheme.loop = true;
            towerTheme.volume = 0.4f;
            currentAudio = towerTheme;
            currentAudio.Play();
        }
        // Mountain climb theme - Iron Climb
        else if((SceneManager.GetActiveScene().buildIndex == 7 || SceneManager.GetActiveScene().buildIndex == 8) && currentAudio != ironMtTheme){
            currentAudio.Stop();
            ironMtTheme.loop = true;
            ironMtTheme.volume = 0.5f;
            currentAudio = ironMtTheme;
            currentAudio.Play();
        }
        // Iron Shrine theme

        // Windward Pools

        // Buttefly Forest
        else if(SceneManager.GetActiveScene().buildIndex == 4 && currentAudio != forestBgm){
            currentAudio.Stop();
            forestBgm.loop = true;
            currentAudio = forestBgm;
            currentAudio.Play();
        }
    }

    // Prepare all audio to be used in the program
    void LoadAudio(){
        // Change this if loading more audio
        int numAudio = 10;

        for(int i = 0; i < numAudio; i++){
            gameObject.AddComponent<AudioSource>();
        }

        audioList = gameObject.GetComponents<AudioSource>();
        audioList[0].clip = (AudioClip)Resources.Load("Audio/buttonClick");
        audioList[1].clip = (AudioClip)Resources.Load("Audio/09 Games with stones");
        audioList[2].clip = (AudioClip)Resources.Load("Audio/jump");
        audioList[3].clip = (AudioClip)Resources.Load("Audio/fall");
        audioList[4].clip = (AudioClip)Resources.Load("Audio/05 The fairy forest");
        audioList[5].clip = (AudioClip)Resources.Load("Audio/level-completed");
        audioList[6].clip = (AudioClip)Resources.Load("Audio/footstep");
        audioList[7].clip = (AudioClip)Resources.Load("Audio/01Town0");
        audioList[8].clip = (AudioClip)Resources.Load("Audio/01 Riding dragons");
        audioList[9].clip = (AudioClip)Resources.Load("Audio/Over the Far Hills");
        
        for(int i = 0; i < audioList.Length; i++){
            gameObject.GetComponents<AudioSource>()[i] = audioList[i];
        }

        buttonClick = gameObject.GetComponents<AudioSource>()[0];
        bgm = gameObject.GetComponents<AudioSource>()[1];
        jump = gameObject.GetComponents<AudioSource>()[2];
        fall = gameObject.GetComponents<AudioSource>()[3];
        forestBgm = gameObject.GetComponents<AudioSource>()[4];
        complete = gameObject.GetComponents<AudioSource>()[5];
        footstep = gameObject.GetComponents<AudioSource>()[6];
        plainsTheme = gameObject.GetComponents<AudioSource>()[7];
        towerTheme = gameObject.GetComponents<AudioSource>()[8];
        ironMtTheme= gameObject.GetComponents<AudioSource>()[9];

        // Any required adjustments to audio (volume, tone, etc.)
        jump.volume = 0.2f;
        complete.volume = 0.2f;
    }

    public void StopAudio(){
        for(int i = 0; i < audioList.Length; i++){
            gameObject.GetComponents<AudioSource>()[i].Stop();
        }
    }
}
