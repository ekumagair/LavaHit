using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayCanvas : MonoBehaviour
{
    public TMP_Text textTime;
    public TMP_Text textHealth;

    private PlayerScript _playerScript;

    void Start()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    void Update()
    {
        textHealth.text = _playerScript.vida.ToString();
        textTime.text = PlayerScript.timeMinutes.ToString() + ":";

        if (PlayerScript.timeSeconds < 10)
        {
            textTime.text += "0" + PlayerScript.timeSeconds.ToString();
        }
        else
        {
            textTime.text += PlayerScript.timeSeconds.ToString();
        }

        if (PlayerScript.playing == true)
        {
            // Voltar para o menu com esc.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Jogo");
            }
        }
    }
}
