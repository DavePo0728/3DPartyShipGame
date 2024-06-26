using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class RedShipEnergyManager : MonoBehaviour
{
    float MaxEnergy = 100;
    [SerializeField]
    float currentEnergy;
    [SerializeField]
    Image p1EnergyUI;
    Color p1EnergyUIColor;
    [SerializeField]
    GameObject energyBar;
    [SerializeField]
    Attack redShipAttack;
    Vector3 targetScale = new Vector3(1.2f,1.2f,1.2f);
    Vector3 originScale = new Vector3(0.8f, 0.8f, 0.8f);
    float duration = 1.0f;
    bool superMode;
    [SerializeField]
    AudioSource energySound;
    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = 95;
        superMode = false;
        UpdateEnergyUI();
        p1EnergyUIColor = p1EnergyUI.color;
        energySound = GetComponent<AudioSource>();
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
        if (target.tag == "Bullet"&&superMode ==false)
        {
            currentEnergy += 5;

        }
        if (target.tag == "EnergyBar")
        {
            if (currentEnergy <= 100)
            {
                currentEnergy += 5;
                energySound.PlayOneShot(Resources.Load<AudioClip>("GetEnergy"));
            }
        }
        UpdateEnergyUI();
        
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
        UpdateEnergyUI();
    }
    IEnumerator SuperMode()
    {
        energySound.PlayOneShot(Resources.Load<AudioClip>("superModSE"));
        superMode = true;
        currentEnergy = 0;
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
        StartCoroutine(StartCountdown());
        yield return new WaitForSeconds(5f);
        time = 0f;
        energySound.PlayOneShot(Resources.Load<AudioClip>("SuperModOff"));
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
        p1EnergyUI.color = p1EnergyUIColor;
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
            p1EnergyUI.fillAmount = temp;
            float hueValue = Mathf.Lerp(122f / 360f, 0f, totalTime / duration);
            p1EnergyUI.color = Color.HSVToRGB(hueValue, 1, 1);
            yield return null;
        }
    }
}
