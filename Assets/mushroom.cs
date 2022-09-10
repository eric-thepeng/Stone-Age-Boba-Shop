using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroom : MonoBehaviour
{
    IEnumerator Start()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.8f, 1f), Random.Range(-0.5f, 0.5f)).normalized * 100f);
        float timeCount = 0;
        while (timeCount<0.5)
        {
            timeCount += Time.deltaTime;
            yield return new WaitForSeconds(0);
        }
        GetComponent<CapsuleCollider>().enabled = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0, 1f), Random.Range(-0.5f, 0.5f)).normalized * 100f);
        }
    }

}
