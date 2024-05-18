using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameOver = false;
    float Maxhp = 1000;
    float player1_currentHp, player2_currentHp;
    [SerializeField]
    Image p1HpUI, p2HpUI;

    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        player1_currentHp = Maxhp;
        player2_currentHp = Maxhp;

        
        UpdateHpUI();
    }
    private void Update()
    {

    }
    public void RestartGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("re");
            SceneManager.LoadScene("Game");
        }
    }
    public void GetHit(GameObject target, float damage)
    {
        if (target.tag == "Player")
        {
            player1_currentHp -= damage;
        }
        if (target.tag == "Player2")
        {
            player2_currentHp -= damage;
        }
        if (player1_currentHp <= 0 )
        {
            gameOver = true;
            Time.timeScale = 0;
        }
        if(player2_currentHp <= 0)
        {
            gameOver = true;
            Time.timeScale = 0;
        }
        UpdateHpUI();
    }
    
    private void UpdateHpUI()
    {
        float p1Hp = player1_currentHp / Maxhp;
        float p2Hp = player2_currentHp / Maxhp;
        p1HpUI.fillAmount = p1Hp;
        p2HpUI.fillAmount = p2Hp;
    }
   

}
    
