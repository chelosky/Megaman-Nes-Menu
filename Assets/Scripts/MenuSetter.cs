using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class MenuSetter : MonoBehaviour
{
    [Header("Enemy Prefab")]
    public GameObject enemyIconPrefab;
    [Header("Miscellaneous")]
    [SerializeField] private List<Transform> listTransformGrid = new List<Transform>();
    [SerializeField] private List<Sprite> listSpriteEnemy = new List<Sprite>();
    [SerializeField] private List<Sprite> listSpritePlayer = new List<Sprite>();

    
    // Start is called before the first frame update
    void Start()
    {
        this.VerifyResources();
        this.SetUpEnemies();
    }

    private void SetUpEnemies(){
        for(int i=0;i<this.listSpriteEnemy.Count;i++){
            GameObject enemyGO = (GameObject)Instantiate(this.enemyIconPrefab,this.transform.position,Quaternion.identity);
            enemyGO.transform.SetParent(this.transform);
            enemyGO.transform.localScale = new Vector3(1f,1f,1f);
            enemyGO.transform.localPosition = this.listTransformGrid[i + i/4].localPosition;
            enemyGO.transform.GetComponent<Image>().sprite = this.listSpriteEnemy[i];
        }
    }

    private void VerifyResources(){
        if(this.enemyIconPrefab == null){
            this.enemyIconPrefab = Resources.Load("Prefab/EnemyIcon", typeof(GameObject)) as GameObject;
        }
        if(this.listSpritePlayer.Count == 0){
            Sprite[] spriteEx = Resources.LoadAll <Sprite> ("MegamanFace") as Sprite[];  
            foreach (Sprite t in spriteEx)
            {
                this.listSpritePlayer.Add(t);
            }
        }
        if(this.listSpriteEnemy.Count == 0){
            Sprite[] spriteEx = Resources.LoadAll <Sprite> ("EnemiesFace") as Sprite[];  
            for(int i=0;i<spriteEx.Length;i++){
                if(i!=4){
                    this.listSpriteEnemy.Add(spriteEx[i]);
                }
            }
        }
        if(this.listTransformGrid.Count == 0){
            Transform ts = this.transform.Find("SelectorPosition");
            for (int i = 0; i < ts.childCount; i++){
                listTransformGrid.Add(ts.GetChild(i));
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
