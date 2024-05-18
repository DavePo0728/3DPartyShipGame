using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedShipEnergyManager : MonoBehaviour
{
    float MaxEnergy = 100;
    [SerializeField]
    float currentEnergy;
    [SerializeField]
    Image p1EnergyUI;
    [SerializeField]
    GameObject energyBar;
    [SerializeField]
    Attack redShipAttack;
    Vector3 targetScale = new Vector3(1.2f,1.2f,1.2f);
    Vector3 originScale = new Vector3(0.8f, 0.8f, 0.8f);
    float duration = 1.0f;
    bool superMode;
    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = 95;
        superMode = false;
        UpdateEnergyUI();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GetEnergy(GameObject target)
    {
        if (target.tag == "Bullet"&&superMode ==false)
        {
            currentEnergy += 5;

        }
        if (target.tag == "EnergyBar")
        {
            currentEnergy += 5;
        }
        UpdateEnergyUI();
        if(currentEnergy >= MaxEnergy)
        {
            StartCoroutine(SuperMode());
            currentEnergy = 0;
        }
    }
    private void UpdateEnergyUI()
    {
        float p1Energy = currentEnergy / MaxEnergy;
        p1EnergyUI.fillAmount = p1Energy;
    }
    public void DropEnergy()
    {
        if (currentEnergy >= 15)
        {
            currentEnergy -= 15;
            Instantiate(energyBar, transform.position, Quaternion.identity);
            Instantiate(energyBar, transform.position, Quaternion.identity);
            Instantiate(energyBar, transform.position, Quaternion.identity);

        }else if (currentEnergy <= 14 && currentEnergy >= 10)
        {
            currentEnergy -= 10;
            Instantiate(energyBar, transform.position, Quaternion.identity);
            Instantiate(energyBar, transform.position, Quaternion.identity);

        }
        else if (currentEnergy <= 9 && currentEnergy >= 5)
        {
            currentEnergy -= 5;
            Instantiate(energyBar, transform.position, Quaternion.identity);

        }

    }
    IEnumerator SuperMode()
    {
        superMode = true;
        float time = 0f;
        Vector3 initialScale = transform.localScale;
        redShipAttack.superAttack = true;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            t = Mathf.SmoothStep(0f, 1f, t);
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }
        transform.localScale = targetScale;
        yield return new WaitForSeconds(5f);
        time = 0f;
        initialScale = transform.localScale;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            t = Mathf.SmoothStep(0f, 1f, t);
            transform.localScale = Vector3.Lerp(initialScale, originScale, t);
            yield return null;
        }
        redShipAttack.superAttack = false;
        superMode = false;
    }
}
