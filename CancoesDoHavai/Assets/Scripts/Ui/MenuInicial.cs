using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuInicial : MonoBehaviour
{
    public GameObject telaMenu;
    public GameObject telaCreditos;

    public void jogar()
    {
        SceneManager.LoadScene("jogo");
    }
    public void configuracoes()
    {
        telaMenu.SetActive(true);
    }
    public void creditos()
    {
        telaCreditos.SetActive(true);
    }

    public void sair()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void voltar()
    {
        telaCreditos.SetActive(false);
        telaMenu.SetActive(true);   
    }


}
