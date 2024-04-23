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
    [SerializeField]
    float player1_currenthp, player2_currenthp;
    [SerializeField]
    Image p1HpSlider, p2HpSlider;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        player1_currenthp = Maxhp;
        player2_currenthp = Maxhp;
        // p1HpSlider.fillAmount = player1_currenthp;
        // p2HpSlider.fillAmount = player2_currenthp;
        //Debug.Log(string.Join("\n", Gamepad.all));

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
            player1_currenthp -= damage;
        }
        if (target.tag == "Player2")
        {
            player2_currenthp -= damage;
        }
        if (player1_currenthp <= 0 )
        {
            gameOver = true;
            Time.timeScale = 0;
        }
        if(player2_currenthp <= 0)
        {
            gameOver = true;
            Time.timeScale = 0;
        }
        UpdateHpUI();
    }
    private void UpdateHpUI()
    {
        float p1Hp = player1_currenthp / Maxhp;
        float p2Hp = player2_currenthp / Maxhp;
        p1HpSlider.fillAmount = p1Hp;
        p2HpSlider.fillAmount = p2Hp;
    }
}
    
