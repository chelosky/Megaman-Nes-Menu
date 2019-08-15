using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour{

    public void SignalEndAnimation(){
        ManagerController.instance.StartAnimScene();
    }

    public void SignalChangeMusic(){
        ManagerController.instance.ChangeMusic();
    }

    public void SignalMegamanUpMovement(){
        ManagerController.instance.StartMegamanMovement();
    }
}
