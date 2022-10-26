using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorDeClique : MonoBehaviour
{
    public LayerMask podeClicarMask;
    public GameObject[] efeitoImpacto;

    // Por quanto tempo está segurando o botão.
    public static float tempoSegurandoMouse = 0f;

    // Tempo máximo de segurar o botão.
    public static float tempoMaxSegurandoMouse = 1f;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, podeClicarMask))
            {
                Debug.Log("Clicou com força " + tempoSegurandoMouse);
                BolaDeLavaScript scriptDoObj = hit.collider.gameObject.GetComponent<BolaDeLavaScript>();

                if (scriptDoObj.impulsionado == false)
                {
                    if (tempoSegurandoMouse <= 0.5f)
                    {
                        Instantiate(efeitoImpacto[0], hit.transform.position, transform.rotation);
                    }
                    else
                    {
                        Instantiate(efeitoImpacto[1], hit.transform.position, transform.rotation);
                    }

                    scriptDoObj.Clicou();
                }
            }

            tempoSegurandoMouse = 0f;
        }
        else if (Input.GetMouseButton(0))
        {
            if (tempoSegurandoMouse < tempoMaxSegurandoMouse)
            {
                tempoSegurandoMouse += Time.deltaTime;
            }
        }
    }
}
