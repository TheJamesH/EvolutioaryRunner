using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCellController : MonoBehaviour
{
    float maxSize = 1.2f;
    float minSize = 0.8f;
    public float phase;
    public float waitTime;

    void Start()
    {
        if (phase != 0)
        {
            StartCoroutine(Scale());
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        }
    }

    void Update()
    {
        if (phase != 0)
        {
            //GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, (transform.localScale.x - 0.8f) * .8f);
        }
    }

    IEnumerator Scale()
    {
        float timer = 0;

        while (true) // this could also be a condition indicating "alive or dead"
        {
            // we scale all axis, so they will have the same value, 
            // so we can work with a float instead of comparing vectors
            if (phase == 1)
            {
                while (maxSize > transform.localScale.x)
                {
                    timer += Time.deltaTime;
                    transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 1;
                    GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, (transform.localScale.x - 0.8f) * .8f);
                    yield return null;
      
                }
            }
            // reset the timer
            yield return new WaitForSeconds(waitTime);
            timer = 0;
            
            while (minSize < transform.localScale.x)
            {
                timer += Time.deltaTime;
                transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime * 1;
                GetComponent<SpriteRenderer>().color = Color.Lerp(Color.blue, Color.white, (transform.localScale.x - 0.2f) * 1.0f);
                yield return null;
            }

            phase = 1;
            timer = 0;
            yield return new WaitForSeconds(waitTime);
        }
    }
}