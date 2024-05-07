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
            // 檢查 Particle System 是否正在播放
            if (!ps.IsAlive())
            {
                // 如果 Particle System 已經播放完畢，則銷毀該物件
                Destroy(gameObject);
            }
        }
    }
}
