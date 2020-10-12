using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Transitions : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    [SerializeField] private float waitForSeconds = 1.5f;
    [SerializeField] private bool useAnimationsEnter;
    [SerializeField] private Image image;
    [SerializeField] private bool useLoadScene;
    [SerializeField] Button[] buttons;

    private Selectable auxNavegationLeftButton;
    private Selectable auxNavegationRightButton;
    private Selectable auxNavegationUpButton;
    private Selectable auxNavegationDownButton;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (useAnimationsEnter && animator != null && LevelLoader.prevLevel != "MENU" 
            && LevelLoader.prevLevel != "Multijugador" && LevelLoader.prevLevel != "Creditos"
            && LevelLoader.prevLevel != "Controles")
            animator.SetTrigger("entrada");
        else
        {
            image.enabled = false;
        }
    }
    public void LoadScene(string nameScene)
    {
        image.enabled = true;
        StartCoroutine(Transition(nameScene));
    }
    IEnumerator Transition(string nameScene)
    {
        animator.SetTrigger("salida");
        yield return new WaitForSeconds(waitForSeconds);
        if(useLoadScene)
            SceneManager.LoadScene(nameScene);
    }

    public void EnableButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] != null)
            {
                buttons[i].interactable = true;
            }
        }
    }
    public void DisableButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] != null)
            {
                buttons[i].interactable = false;
            }
        }
    }
}
