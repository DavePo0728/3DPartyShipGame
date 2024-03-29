using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    float Maxhp = 1000;
    [SerializeField]
    float player1_currenthp, player2_currenthp;
    [SerializeField]
    Image p1HpSlider, p2HpSlider;
    // Start is called before the first frame update
    void Start()
    {
        player1_currenthp = Maxhp;
        player2_currenthp = Maxhp;
        // p1HpSlider.fillAmount = player1_currenthp;
        // p2HpSlider.fillAmount = player2_currenthp;
        //Debug.Log(string.Join("\n", Gamepad.all));

    }
    private void Update()
    {

    }
    public void GetHit(GameObject target, float damage)
    {
        if (target.tag == "Player")
        {
            //Debug.Log("hit: " + target.tag);
            player1_currenthp -= damage;
            //p1HpSlider.value = player1_currenthp;
        }
        if (target.tag == "Player2")
        {
            //Debug.Log("hit: " + target.tag);
            player2_currenthp -= damage;
            //p2HpSlider.value = player2_currenthp;
        }
        if (player1_currenthp <= 0 )
        {
            Destroy(target.gameObject);
        }
        if(player2_currenthp <= 0)
        {
            Destroy(target.gameObject);
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
    
