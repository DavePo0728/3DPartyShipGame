using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public ParticleSystem ps;

    void Start()
    {
       
    }

    void Update()
    {
        if (ps)
        {
            // �ˬd Particle System �O�_���b����
            if (!ps.IsAlive())
            {
                // �p�G Particle System �w�g���񧹲��A�h�P���Ӫ���
                Destroy(gameObject);
            }
        }
    }
}
