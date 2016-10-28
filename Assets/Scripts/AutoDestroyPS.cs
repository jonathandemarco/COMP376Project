using UnityEngine;
using System.Collections;

public class AutoDestroyPS : MonoBehaviour {
    float tTime = 0.0f;
    ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();

    }

    void Update()
    {
        tTime += Time.deltaTime;
        if (tTime > ps.duration)
        {
            ParticleSystem s = GetComponent<ParticleSystem>();
            Destroy(s);
            Destroy(gameObject);
        }
    }
}
