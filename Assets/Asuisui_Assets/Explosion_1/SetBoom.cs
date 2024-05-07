using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoom : MonoBehaviour
{
    public GameObject Boomeffect;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnBoomEffect", 0f, 3f);
    }

    // �o�Ө禡�|�b�C3�����@��
    void SpawnBoomEffect()
    {
        GameObject boom;
        boom = Instantiate(Boomeffect, transform.position, Quaternion.identity);
        boom.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
    }
}
