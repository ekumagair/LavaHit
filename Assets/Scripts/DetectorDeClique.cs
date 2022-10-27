using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorDeClique : MonoBehaviour
{
    public LayerMask podeClicarMask;
    public GameObject[] efeitoImpacto;
    public CameraShake scriptCameraShake;
    JogadorScript scriptJogador;

    float mainCamFovPadrao;

    // Por quanto tempo est� segurando o bot�o.
    public static float tempoSegurandoMouse = 0f;

    // Tempo m�ximo de segurar o bot�o.
    public static float tempoMaxSegurandoMouse = 1f;

    void Start()
    {
        mainCamFovPadrao = Camera.main.fieldOfView;
        scriptJogador = GameObject.FindGameObjectWithTag("Player").GetComponent<JogadorScript>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && CanvasScript.jogando == true && scriptJogador.vida > 0)
        {
            // Soltou bot�o do mouse.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, podeClicarMask))
            {
                Debug.Log("Clicou com for�a " + tempoSegurandoMouse);
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
                    scriptJogador.velocidade += 0.25f;
                    scriptCameraShake.ShakeCamera(0.3f, tempoSegurandoMouse / 4);
                }
            }

            tempoSegurandoMouse = 0f;
        }
        else if (Input.GetMouseButton(0) && JogadorScript.levandoDano == false && CanvasScript.jogando == true && scriptJogador.vida > 0)
        {
            // Segurando bot�o do mouse.
            if (tempoSegurandoMouse < tempoMaxSegurandoMouse)
            {
                tempoSegurandoMouse += Time.deltaTime;
            }

            if(Camera.main.fieldOfView > mainCamFovPadrao - 4)
            {
                Camera.main.fieldOfView -= Time.deltaTime * 30f;
            }
        }
        else
        {
            // N�o est� segurando o bot�o do mouse.
            if (Camera.main.fieldOfView < mainCamFovPadrao)
            {
                Camera.main.fieldOfView += Time.deltaTime * 80f;
            }
        }
    }
}
