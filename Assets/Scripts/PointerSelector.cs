using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerSelector : MonoBehaviour
{

    [Header("Positions")]
    [SerializeField] List<Transform> posSelector = new List<Transform>();
    private int posAct = 0;
    private bool optionSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            this.optionSelected = true;
            ManagerController.instance.StopMusic();
            this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r,this.GetComponent<SpriteRenderer>().color.b,this.GetComponent<SpriteRenderer>().color.g,0f);
            ManagerController.instance.StartMegamanAnimation();
        }
        if(this.optionSelected == false){
            int UPMOV = Input.GetKeyDown(KeyCode.UpArrow) ? 1:0;
            int DOWNMOV = Input.GetKeyDown(KeyCode.DownArrow) ? -1:0;
            if(UPMOV + DOWNMOV != 0){
                this.posAct += (DOWNMOV+UPMOV);
                if(this.posAct > 1){
                    this.posAct = 0;
                }else if(this.posAct < 0){
                    this.posAct = 1;
                }
                ManagerController.instance.PlaySoundEffect(0);
                this.transform.position = this.posSelector[this.posAct].position;
            }
        }
    }
}
