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

    // Por quanto tempo está segurando o botão.
    public static float tempoSegurandoMouse = 0f;

    // Tempo máximo de segurar o botão.
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
            // Soltou botão do mouse.
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
                    scriptJogador.velocidade += 0.25f;
                    scriptCameraShake.ShakeCamera(0.25f, tempoSegurandoMouse / 5);
                }
            }

            tempoSegurandoMouse = 0f;
        }
        else if (Input.GetMouseButton(0) && JogadorScript.levandoDano == false && CanvasScript.jogando == true && scriptJogador.vida > 0)
        {
            // Segurando botão do mouse.
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
            // Não está segurando o botão do mouse.
            if (Camera.main.fieldOfView < mainCamFovPadrao)
            {
                Camera.main.fieldOfView += Time.deltaTime * 80f;
            }

            barraDeForcaFundo.SetActive(false);
        }
    }
}
