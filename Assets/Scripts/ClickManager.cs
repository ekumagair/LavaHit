using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ClickManager : MonoBehaviour
{
    public LayerMask podeClicarMask;
    public GameObject[] efeitoImpacto;
    public CameraShake scriptCameraShake;
    public GameObject barraDeForcaFundo;
    public GameObject barraDeForca;
    public GameObject barraDeForcaEfeito;

    // Por quanto tempo está segurando o botão.
    public static float tempoSegurandoMouse = 0f;

    // Tempo máximo de segurar o botão.
    public static float tempoMaxSegurandoMouse = 1f;

    private PlayerScript _playerScript;
    private float _mainCamFovDefault;

    void Start()
    {
        tempoSegurandoMouse = 0f;
        _mainCamFovDefault = Camera.main.fieldOfView;
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        barraDeForcaEfeito.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0) && PlayerScript.playing == true && _playerScript.vida > 0 && PlayerScript.takingDamage == false)
        {
            // Soltou botão do mouse.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, podeClicarMask))
            {
                //Debug.Log("Clicou com força " + tempoSegurandoMouse);
                LavaBall scriptDoObj = hit.collider.gameObject.GetComponent<LavaBall>();

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

                    scriptDoObj.Clicked();
                    _playerScript.velocidade += 0.25f;
                    scriptCameraShake.ShakeCamera(0.25f, tempoSegurandoMouse / 5);
                }
            }

            tempoSegurandoMouse = 0f;
        }
        else if (Input.GetMouseButton(0) && PlayerScript.takingDamage == false && PlayerScript.playing == true && _playerScript.vida > 0)
        {
            // Segurando botão do mouse.
            if (tempoSegurandoMouse < tempoMaxSegurandoMouse)
            {
                tempoSegurandoMouse += Time.deltaTime;
            }

            if (Camera.main.fieldOfView > _mainCamFovDefault - 4)
            {
                Camera.main.fieldOfView -= Time.deltaTime * 30f;
            }

            barraDeForcaFundo.SetActive(true);
            barraDeForcaFundo.transform.position = Input.mousePosition + (Vector3.down * 60f);
            barraDeForca.transform.localScale = new Vector3(tempoSegurandoMouse / tempoMaxSegurandoMouse, 1, 1);

            if (tempoSegurandoMouse >= tempoMaxSegurandoMouse)
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
            if (Camera.main.fieldOfView < _mainCamFovDefault)
            {
                Camera.main.fieldOfView += Time.deltaTime * 80f;
            }

            barraDeForcaFundo.SetActive(false);
        }
    }
}
