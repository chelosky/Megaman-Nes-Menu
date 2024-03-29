﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MenuPlayer : MonoBehaviour
{
    [Header("Main Icons")]
    [SerializeField] private GameObject selectorGO;
    [SerializeField] private GameObject megamanIcon;
    [Header("Sound Effect")]
    [SerializeField] private List<AudioClip> listAudios = new List<AudioClip>();
    [Header("Grid Position")]
    [SerializeField] private int actualPos = 4;
    [Header("Miscellaneous")]
    [SerializeField] private List<Transform> listTransformGrid = new List<Transform>();
    [SerializeField] private List<Sprite> listSpritePlayer = new List<Sprite>();
    private AudioSource _audioSource;
    private bool enemySelected = false;
    // Start is called before the first frame update
    void Start()
    {
        this._audioSource = this.GetComponent<AudioSource>();
        this.VerifyResources();
    }

    private void VerifyResources(){
        if(this.listSpritePlayer.Count == 0){
            Sprite[] spriteEx = Resources.LoadAll <Sprite> ("MegamanFace") as Sprite[];  
            foreach (Sprite t in spriteEx)
            {
                this.listSpritePlayer.Add(t);
            }
        }
        if(this.listTransformGrid.Count == 0){
            Transform ts = this.transform.Find("SelectorPosition");
            for (int i = 0; i < ts.childCount; i++){
                listTransformGrid.Add(ts.GetChild(i));
            }
        }
        if(this.selectorGO == null){
            this.selectorGO = this.transform.Find("Selector").gameObject;
        }
        if(this.megamanIcon == null){
            this.megamanIcon = this.transform.Find("Player").gameObject;
        }
        if(this.listAudios.Count == 0){
            AudioClip sound1 = (AudioClip)Resources.Load("Sounds/MM6_bossSELECTION",typeof(AudioClip));
            AudioClip sound2 = (AudioClip)Resources.Load("Sounds/MM6_bossSELECTED",typeof(AudioClip));
            this.listAudios.Add(sound1);
            this.listAudios.Add(sound2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // float vMov = Input.GetAxisRaw("Vertical"); //-1 aba || +1 arr
        // float hMov = Input.GetAxisRaw("Horizontal");//-1 izq || +1 der
        if(Input.GetKeyDown(KeyCode.Return) && this.actualPos != 4){
            this.enemySelected = true;
            this.transform.Find("Selector").GetComponent<Animator>().SetTrigger("EnemySelected");
            this.PlaySoundEffect(1);
        }
        
        if(this.enemySelected == false){
            //TEMPORAL POSITION
            float tmpPos = this.actualPos;
            //VERTICAL INPUT
            float UPMOV = Input.GetKeyDown(KeyCode.UpArrow) ? 1f:0f;
            float DOWNMOV = Input.GetKeyDown(KeyCode.DownArrow) ? -1f:0f;
            float vMov = UPMOV + DOWNMOV;
            //HORIZONTAL INPUT
            float RIGHTMOV = Input.GetKeyDown(KeyCode.RightArrow) ? 1f:0f;
            float LEFTMOV = Input.GetKeyDown(KeyCode.LeftArrow) ? -1f:0f;
            float hMov = RIGHTMOV + LEFTMOV;
            //CASOS PARA VMOV
            if(vMov == 1){// ARRIBA
                if(this.actualPos > 2){
                    this.actualPos -= 3;
                }
            }else if(vMov == -1){ // ABAJO
                if(this.actualPos < 6){
                    this.actualPos += 3;
                }
            }

            //CASOS PARA HMOV
            if(hMov == 1){ //DERECHA
                if(this.actualPos != 2 && this.actualPos != 5 && this.actualPos != 8){
                    this.actualPos +=1;
                }
            }else if(hMov == -1 ){ //IZQUIERDA
                if(this.actualPos != 0 && this.actualPos != 3 && this.actualPos != 6){
                    this.actualPos -=1;
                }
            }
            if(this.actualPos != tmpPos){
                this.UpdateSelectorPosition();
                this.UpdateMegamanIcon();
                this.PlaySoundEffect(0);
            }
        }
    }

    private void PlaySoundEffect(int value){
        this._audioSource.PlayOneShot(this.listAudios[value]);
    }


    private void UpdateSelectorPosition(){
        this.selectorGO.transform.localPosition = this.listTransformGrid[this.actualPos].localPosition;
    }

    private void UpdateMegamanIcon(){
        this.megamanIcon.transform.GetComponent<Image>().sprite = this.listSpritePlayer[this.actualPos];
    }
}
