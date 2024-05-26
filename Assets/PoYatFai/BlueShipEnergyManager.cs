using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BlueShipEnergyManager : MonoBehaviour
{
    float MaxEnergy = 100;
    [SerializeField]
    float currentEnergy;
    [SerializeField]
    Image p2EnergyUI;
    Color p2EnergyUIColor;
    [SerializeField]
    GameObject energyBar;
    [SerializeField]
    Attack blueShipAttack;
    Vector3 targetScale = new Vector3(1.2f, 1.2f, 1.2f);
    Vector3 originScale = new Vector3(0.8f, 0.8f, 0.8f);
    float duration = 1.0f;
    bool superMode;
    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = 25;
        UpdateEnergyUI();
        superMode = false;
        p2EnergyUIColor=p2EnergyUI.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SuperModeInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (currentEnergy >= MaxEnergy)
            {
                StartCoroutine(SuperMode());

            }
        }
    }
    public void GetEnergy(GameObject target)
    {
        if (target.tag == "Bullet1" && superMode == false)
        {
            currentEnergy += 5;
        }
        if (target.tag == "EnergyBar")
        {
            currentEnergy += 5;
        }
        UpdateEnergyUI();
    }

    private void UpdateEnergyUI()
    {
        float p2Energy = currentEnergy / MaxEnergy;
        p2EnergyUI.fillAmount = p2Energy;
    }
    public void DropEnergy()
    {
        Debug.Log("rf");
        if (currentEnergy >= 15)
        {
            Debug.Log(transform.position);
            currentEnergy -= 15;
            Instantiate(energyBar, transform.position, Quaternion.identity);
            Instantiate(energyBar, transform.position, Quaternion.identity);
            Instantiate(energyBar, transform.position, Quaternion.identity);
        }
        else if (currentEnergy <= 14 && currentEnergy >= 10)
        {
            Debug.Log("10");
            currentEnergy -= 10;
            Instantiate(energyBar, transform.position, Quaternion.identity);
            Instantiate(energyBar, transform.position, Quaternion.identity);
        }
        else if (currentEnergy <= 9 && currentEnergy >= 5)
        {
            Debug.Log("5");
            currentEnergy -= 5;
            Instantiate(energyBar, transform.position, Quaternion.identity);
        }
        UpdateEnergyUI();
    }
    IEnumerator SuperMode()
    {
        superMode = true;
        currentEnergy = 0;
        float time = 0f;
        Vector3 initialScale = transform.localScale;
        blueShipAttack.superAttack = true;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            t = Mathf.SmoothStep(0f, 1f, t);
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }
        transform.localScale = targetScale;
        StartCoroutine(StartCountdown());
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
        blueShipAttack.superAttack = false;
        superMode = false;
        p2EnergyUI.color = p2EnergyUIColor;
    }
    IEnumerator StartCountdown()
    {
        float duration = 5f;
        float totalTime = 0;
        float startTime = Time.time;

        while (totalTime <= duration)
        {
            totalTime = Time.time - startTime;
            float currentValue = MaxEnergy * (1 - totalTime / duration);
            float temp = currentValue / MaxEnergy;
            p2EnergyUI.fillAmount = temp;
            float hueValue = Mathf.Lerp(122f / 360f, 0f, totalTime / duration);
            p2EnergyUI.color = Color.HSVToRGB(hueValue, 1, 1);
            yield return null;
        }
    }
}
