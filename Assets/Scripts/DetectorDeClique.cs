using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DetectorDeClique : MonoBehaviour
{
    public LayerMask podeClicarMask;
    public GameObject[] efeitoImpacto;
    public CameraShake scriptCameraShake;
    public GameObject barraDeForcaFundo;
    public GameObject barraDeForca;
    public GameObject barraDeForcaEfeito;

    JogadorScript scriptJogador;

    float mainCamFovPadrao;

    // Por quanto tempo est� segurando o bot�o.
    public static float tempoSegurandoMouse = 0f;

    // Tempo m�ximo de segurar o bot�o.
    public static float tempoMaxSegurandoMouse = 1f;

    void Start()
    {
        tempoSegurandoMouse = 0f;
        mainCamFovPadrao = Camera.main.fieldOfView;
        scriptJogador = GameObject.FindGameObjectWithTag("Player").GetComponent<JogadorScript>();
        barraDeForcaEfeito.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && CanvasScript.jogando == true && scriptJogador.vida > 0 && JogadorScript.levandoDano == false)
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
                    scriptCameraShake.ShakeCamera(0.25f, tempoSegurandoMouse / 5);
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

            barraDeForcaFundo.SetActive(true);
            barraDeForcaFundo.transform.position = Input.mousePosition + (Vector3.down * 60f);
            barraDeForca.transform.localScale = new Vector3(tempoSegurandoMouse / tempoMaxSegurandoMouse, 1, 1);

            if(tempoSegurandoMouse >= tempoMaxSegurandoMouse)
            {
                barraDeForcaEfeito.SetActive(true);
            }
            else
            {
                barraDeForcaEfeito.SetActive(false);
            }
        }
        else
        {
            // N�o est� segurando o bot�o do mouse.
            if (Camera.main.fieldOfView < mainCamFovPadrao)
            {
                Camera.main.fieldOfView += Time.deltaTime * 80f;
            }

            barraDeForcaFundo.SetActive(false);
        }
    }
}
