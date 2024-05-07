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

    float MaxEnergy = 100;
    [SerializeField]
    float player1_currentEnergy, player2_currentEnergy;
    [SerializeField]
    Image p1EnergyUI, p2EnergyUI;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        player1_currentHp = Maxhp;
        player2_currentHp = Maxhp;
        player1_currentEnergy = 0;
        player2_currentEnergy = 0;
        UpdateEnergyUI();
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
    public void GetEnergy(GameObject target)
    {
        if (target.tag == "Bullet")
        {
            player1_currentEnergy += 5;
            
        }
        if (target.tag == "Bullet1")
        {
            player2_currentEnergy += 5;
            //Debug.Log("p2 :"+player2_currentEnergy);
        }
        UpdateEnergyUI();
    }
    private void UpdateHpUI()
    {
        float p1Hp = player1_currentHp / Maxhp;
        float p2Hp = player2_currentHp / Maxhp;
        p1HpUI.fillAmount = p1Hp;
        p2HpUI.fillAmount = p2Hp;
    }
    private void UpdateEnergyUI()
    {
        float p1Energy = player1_currentEnergy / MaxEnergy;
        Debug.Log("p1 :" + p1Energy);
        float p2Energy = player2_currentEnergy / MaxEnergy;
        p1EnergyUI.fillAmount = p1Energy;
        p2EnergyUI.fillAmount = p2Energy;
    } 

}
    
