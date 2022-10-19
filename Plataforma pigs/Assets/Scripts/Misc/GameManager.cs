using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static int vida = 3;
    [SerializeField] private int vidaInicial = 3;
    [SerializeField] private Image[] coracoes;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AjustaVida();
    }

    // metodo de trocar de cena
    public void MudaCena(string destino)
    {
        SceneManager.LoadScene(destino);
    }

    public int GetVida()
    {
        return vida;
    }
    
    public void SetVida(int novaVida)
    {
        vida = novaVida;
    }

    public void GameOver()
    {
        vida = vidaInicial;
        Scene currentScene = SceneManager.GetActiveScene();
        string scneName = currentScene.name;
        if (scneName == "Cena 1")
        {
            SceneManager.LoadScene("Cena 1");
        }
        else
        {
            SceneManager.LoadScene("Cena 2");
        }    
        
    }
    
    public void AjustaVida()
    {
        for (var i = 0; i < coracoes.Length; i++)
        {
            if (i < vida)
            {
                coracoes[i].enabled = true;
            }
            else
            {
                coracoes[i].enabled = false;
            }
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
