using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameController : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI tmp;
    public GameObject[] gameObj;
    public GameObject enemyActive;
    public static bool isEnemyActive = false;
    public GameObject posEnemy;

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
