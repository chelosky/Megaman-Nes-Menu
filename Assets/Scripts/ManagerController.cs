using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerController : MonoBehaviour
{
    [Header("Objects Scene")]
    [SerializeField] private GameObject sceneryGO;
    [SerializeField] private GameObject megamanGO;
    [SerializeField] private AudioClip titleScreenMS;
    [Header("Sound Effect")]
    [SerializeField] private List<AudioClip> listAudios = new List<AudioClip>();
    public AudioSource _audioSource;

    public static ManagerController instance { get ; private set;} 
    //SINGLETON
    void Awake(){
        if(instance == null){
            instance = this;
        }else if(instance != this){
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        this._audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnimScene(){
        this.sceneryGO.GetComponent<Animator>().SetTrigger("IntroStart");
    }

    public void ChangeMusic(){
        this._audioSource.Stop();
        this._audioSource.clip = this.titleScreenMS;
        this._audioSource.Play();
    }

    public void StartMegamanAnimation(){
        this.megamanGO.GetComponent<Animator>().SetTrigger("PlayerReady");
    }

    public void StopMusic(){
        this._audioSource.Stop();
    }
    
    public void StartMegamanMovement(){
        StartCoroutine(this.MegamanMovement());
    }

    private IEnumerator MegamanMovement(){
        this.PlaySoundEffect(1);
        Vector2 destPos = new Vector2(this.megamanGO.transform.position.x,7f);
        while (Vector2.Distance(this.megamanGO.transform.position, destPos)>0.5f) {
           this.megamanGO.transform.position = Vector3.MoveTowards(this.megamanGO.transform.position, destPos, 0.8f);
           yield return null;
        }
        SceneManager.LoadScene("MenuSelect");
    }

    public void PlaySoundEffect(int value){
        this._audioSource.PlayOneShot(this.listAudios[value]);
    }
}
