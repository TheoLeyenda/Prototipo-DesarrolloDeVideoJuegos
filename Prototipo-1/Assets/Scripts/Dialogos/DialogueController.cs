using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Threading;

public class DialogueController : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public class Dialogos
    {
        public enum CharacterDialog 
        {
            Player,
            Enemy,
        }
        public CharacterDialog characterDialog;
        [HideInInspector]
        public Sprite spriteHabladorActual;
        public string nameHabladorActual;
        public string dialogoPersonaje;
        public bool finishDialog;
        public Enemy enemy;
        public Player player;
        public bool startAnimation;
        public string nameAnimation;
    }
    public Enemy enemyAsignedDialog;
    public GameObject MarcoDialogo;
    public TextMeshProUGUI textDialog;
    public TextMeshProUGUI textName;
    public Image imageHabladorActual;
    private InputManager inputManager;
    public List<Dialogos> dialogue;
    private List<Dialogos> auxDialogue;
    public List<List<Dialogos>> dialogos;
    public int indexDialog = 0;
    public int ID_Dialog = 0;
    private GameManager gm;
    void Start()
    {
        auxDialogue = new List<Dialogos>();
        inputManager = GameObject.Find("InputManagerController").GetComponent<InputManager>();
        if (GameManager.instanceGameManager != null)
        {
            gm = GameManager.instanceGameManager;
        }
        for (int i = 0; i < dialogue.Count; i++) 
        {

            auxDialogue.Add(dialogue[i]);

            if (dialogue[i].finishDialog) 
            {
                dialogos.Add(auxDialogue);
                auxDialogue.Clear();
            }
        }
    }
    private void OnEnable()
    {
        if (indexDialog < dialogos.Count)
        {
            OpenDialog();
            CheckDialog(indexDialog);
        }
        else 
        {
            gameObject.SetActive(false);
        }
    }
    public void OpenDialog() 
    {
        /*if (dialogue[i].characterDialog == Dialogos.CharacterDialog.Player && inputManager != null)
        {
            dialogue[i].player = inputManager.player1;
        }
        else if (dialogue[i].characterDialog == Dialogos.CharacterDialog.Enemy && inputManager != null)
        {
            dialogue[i].enemy = enemyAsignedDialog;
        }
        else if (inputManager == null)
        {
            Debug.Log("inputManager null");
        }*/
        for (int i = 0; i < dialogos.Count; i++) 
        {
            for (int j = 0; j < dialogos[i].Count; j++) 
            {
                if (dialogos[i][j].characterDialog == Dialogos.CharacterDialog.Player && inputManager != null)
                {
                    dialogos[i][j].player = inputManager.player1;
                }
                else if (dialogos[i][j].characterDialog == Dialogos.CharacterDialog.Enemy && inputManager != null)
                {
                    dialogos[i][j].enemy = enemyAsignedDialog;
                }
                else if (inputManager == null)
                {
                    Debug.Log("inputManager null");
                }
            }
        }
       

        if (inputManager != null)
        {
            inputManager.SetInPause(true);
            inputManager.CheckInPause();
            MarcoDialogo.SetActive(true);
        }
        else 
        {
            Debug.Log("inputManager null");
        }

        
    }
    public void CheckDialog(int ID_Dialog) 
    {
        if (dialogos[indexDialog][ID_Dialog].player != null)
        {
            SpritePlayer spritePlayer = dialogos[indexDialog][ID_Dialog].player.spritePlayerActual;

            dialogos[indexDialog][ID_Dialog].spriteHabladorActual = dialogos[indexDialog][ID_Dialog].player.myHeadSprite;
            if (dialogos[indexDialog][ID_Dialog].startAnimation) 
            {
                spritePlayer.PlayAnimation(dialogos[indexDialog][ID_Dialog].nameAnimation);
            }
        }
        else if (dialogos[indexDialog][ID_Dialog].enemy != null) 
        {
            SpriteEnemy spriteEnemy = dialogos[indexDialog][ID_Dialog].enemy.spriteEnemy;

            dialogos[indexDialog][ID_Dialog].spriteHabladorActual = dialogos[indexDialog][ID_Dialog].enemy.myHeadSprite;
            if (dialogos[indexDialog][ID_Dialog].startAnimation)
            {
                spriteEnemy.PlayAnimation(dialogos[indexDialog][ID_Dialog].nameAnimation);
            }
        }

        imageHabladorActual.sprite = dialogos[indexDialog][ID_Dialog].spriteHabladorActual;
        textName.text = dialogos[indexDialog][ID_Dialog].nameHabladorActual;
        textDialog.text = dialogos[indexDialog][ID_Dialog].dialogoPersonaje;

    }
    // Update is called once per frame
    void Update()
    {
        if (InputPlayerController.GetInputButtonDown("SelectButton_P1"))
        {
            
            ID_Dialog++;
            if (ID_Dialog < dialogos[indexDialog].Count)
            {
                inputManager.SetInPause(true);
                inputManager.CheckInPause();
                CheckDialog(ID_Dialog);
            }
            else 
            {
                inputManager.SetInPause(false);
                inputManager.CheckInPause();
                indexDialog++;
                ID_Dialog = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
