using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameController : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI tmp;
    public GameObject[] gameObj;
    public GameObject enemyActive;
    public bool isEnemyActive = false;
    public GameObject posEnemy;
    public GameObject Character;
    public GameObject Hook;
    public bool reset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = "Score: " + score;
        if(!isEnemyActive){
            
            SpwanEnemy();
            isEnemyActive = true;
        }
    }

    void SpwanEnemy(){
        int random = Random.Range(0, gameObj.Length);
        enemyActive = gameObj[random];
        Instantiate(enemyActive, posEnemy.transform.position, posEnemy.transform.rotation);
    }

}
