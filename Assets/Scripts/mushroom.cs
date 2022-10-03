using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroom : MonoBehaviour
{
    [SerializeField] ScriptableObject thisSO;
    bool canGet = false; //fly for 0.5s before it could be pickedup
    IEnumerator Start()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.8f, 1f), Random.Range(-0.5f, 0f)).normalized * 150f);
        float timeCount = 0;
        while (timeCount<0.5)
        {
            timeCount += Time.deltaTime;
            yield return new WaitForSeconds(0);
        }
        canGet = true;
    }

    /*
    private void OnTriggerEnter(Collider other) //be picked up when near
    {
        if (canGet && other.gameObject.GetComponent<Inventory>())
        {
            Inventory.i.ObtainItem(thisSO);
            Destroy(gameObject);
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (canGet && collision.gameObject.GetComponent<Inventory>())
        {
            Inventory.i.ObtainItem(thisSO);
            Destroy(gameObject);
        }
    }

}
