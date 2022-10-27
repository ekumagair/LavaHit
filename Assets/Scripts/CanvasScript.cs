using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public GameObject menuRoot;

    public static bool jogando = false;

    void Start()
    {
        jogando = false;
    }

    void Update()
    {
        
    }

    public void Iniciar()
    {
        StartCoroutine(IniciarCoroutine());
    }

    IEnumerator IniciarCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        menuRoot.SetActive(false);
        jogando = true;
    }
}
