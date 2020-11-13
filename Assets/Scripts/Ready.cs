using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ready : MonoBehaviour
{
    [SerializeField] private float interval;
    [SerializeField] private Text ready;
    private float countdown = 2.2f;
    private float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        ready.text = "READY";
        timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        timer += Time.fixedDeltaTime;
        if (countdown > 0)
        {
            countdown -= Time.fixedDeltaTime;
            if (timer >= 2 * interval)
            {
                ready.enabled = true;
                timer = 0;
            } else if (timer >= interval)
            {
                ready.enabled = false;             
            }
        }
    }
}
