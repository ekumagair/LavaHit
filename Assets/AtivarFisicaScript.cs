using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarFisicaScript : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Obstaculo")
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
