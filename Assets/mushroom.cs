using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroom : MonoBehaviour
{

    bool canGet = false; //fly for 0.5s before it could be pickedup
    IEnumerator Start()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.8f, 1f), Random.Range(-0.5f, 0.5f)).normalized * 100f);
        float timeCount = 0;
        while (timeCount<0.5)
        {
            timeCount += Time.deltaTime;
            yield return new WaitForSeconds(0);
        }
        canGet = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canGet && collision.gameObject.GetComponent<Inventory>())
        {
            Destroy(gameObject);
        }
    }
}
