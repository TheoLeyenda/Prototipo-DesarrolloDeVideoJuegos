using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Threading;

public class DialogueController : MonoBehaviour
{
    // Start is called before the first frame update


    //REEMPLAZAR LA PAUSA DEL ImputManager con el enableMovemet = false del player y el enemy.
    
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
        [HideInInspector]
        public string nameHabladorActual;
        public string dialogoPersonaje;
        public bool finishDialog;
        [HideInInspector]
        public Enemy enemy;
        [HideInInspector]
        public Player player;
        public bool startAnimation;
        public string nameAnimation;
    }
    //public LevelManager levelManager;
    public bool enableDialogVictory = true;
    public GameObject CamvasInicioPelea;
    public Enemy enemyAsignedDialog;
    public GameObject MarcoDialogo;
    public TextMeshProUGUI textDialog;
    //public TextMeshProUGUI textName;
    public Image imageHabladorActual;
    private InputManager inputManager;
    public List<Dialogos> dialogue;
    private List<Dialogos> auxDialogue;
    public List<List<Dialogos>> dialogos;
    public int indexDialog = 0;
    public int ID_Dialog = 0;
    private GameManager gm;
    public DataCombatPvP dataCombatPvP;
    [HideInInspector]
    public bool OpenDialogInEnableObject = true;
    void Awake()
    {
        if (dialogue.Count > 0)
        {
            int initIndex = 0;
            dialogos = new List<List<Dialogos>>();
            auxDialogue = new List<Dialogos>();
            inputManager = GameObject.Find("InputManagerController").GetComponent<InputManager>();
            if (GameManager.instanceGameManager != null)
            {
                gm = GameManager.instanceGameManager;
            }
            //Debug.Log(dialogue.Count);
            for (int i = 0; i < dialogue.Count; i++)
            {
                auxDialogue.Add(dialogue[i]);
            }
            List<Dialogos> dialog = new List<Dialogos>();
            bool finish = false;
            while (!finish)
            {
                bool asignedList = false;
                int j = initIndex;
                while (j < auxDialogue.Count)
                {
                    if (!auxDialogue[j].finishDialog)
                    {
                        dialog.Add(auxDialogue[j]);
                    }
                    else if (!asignedList)
                    {
                        initIndex = j;
                        dialog.Add(auxDialogue[j]);
                        dialogos.Add(dialog);
                        asignedList = true;
                        j = auxDialogue.Count;
                        if (initIndex >= auxDialogue.Count - 1)
                        {
                            finish = true;
                        }
                    }
                    j++;
                }

            }
        }
    }
    private void OnEnable()
    {
        imageHabladorActual.gameObject.SetActive(true);
        if (OpenDialogInEnableObject)
        {
            if (dialogos != null)
            {
                if (indexDialog < dialogos.Count)
                {
                    OpenDialog();
                    CheckDialog(ID_Dialog);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
    private void OnDisable()
    {
        OpenDialogInEnableObject = true;
    }
    //private void OnDisable()
    //{
    //inputManager.SetInPause(false);
    //inputManager.CheckInPause();
    //}
    public void DialogVictoryEnemy(Enemy enemy,string fraseVictoria,string nameEnemy,Sprite headSprite)
    {
        if (enableDialogVictory && fraseVictoria != " " && nameEnemy != " " && !MarcoDialogo.activeSelf)
        {
            Debug.Log("DENTRO DE FUNCION");
            MarcoDialogo.SetActive(true);
            imageHabladorActual.sprite = headSprite;
            textDialog.text = nameEnemy + ": " + fraseVictoria;
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
                    dialogos[i][j].nameHabladorActual = dialogos[i][j].player.namePlayer;
                }
                else if (dialogos[i][j].characterDialog == Dialogos.CharacterDialog.Enemy && inputManager != null)
                {
                    dialogos[i][j].enemy = enemyAsignedDialog;
                    dialogos[i][j].nameHabladorActual = dialogos[i][j].enemy.nameEnemy;
                }
                else if (inputManager == null)
                {
                    Debug.Log("inputManager null");
                }
            }
        }

        if (inputManager != null)
        {
            if (dataCombatPvP != null)
            {
                if (dataCombatPvP.player1 != null & dataCombatPvP.player2 != null)
                {
                    dataCombatPvP.player1.enableMovementPlayer = false;
                    dataCombatPvP.player1.enableMovement = false;
                    dataCombatPvP.player2.enableMovementPlayer = false;
                    dataCombatPvP.player2.enableMovement = false;
                }
            }
            else if (inputManager.player1 != null && enemyAsignedDialog != null)
            {
                inputManager.player1.enableMovementPlayer = false;
                inputManager.player1.enableMovement = false;
                enemyAsignedDialog.enableMovement = false;
            }

            MarcoDialogo.SetActive(true);
        }
        else 
        {
            Debug.Log("inputManager null");
        }
    }
    public void CheckDialog(int ID_Dialog) 
    {
        //Debug.Log(dialogos.Count);
        //Debug.Log(dialogos[0].Count);

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
        textDialog.text = dialogos[indexDialog][ID_Dialog].nameHabladorActual+": " +dialogos[indexDialog][ID_Dialog].dialogoPersonaje;

    }
    // Update is called once per frame
    void Update()
    {
        if (InputPlayerController.GetInputButtonDown("SelectButton_P1") && OpenDialogInEnableObject)
        {
            
            ID_Dialog++;
            if (ID_Dialog < dialogos[indexDialog].Count)
            {
                CheckDialog(ID_Dialog);
            }
            else 
            {
                indexDialog++;
                ID_Dialog = 0;
                MarcoDialogo.SetActive(false);
                if (CamvasInicioPelea != null)
                {
                    CamvasInicioPelea.SetActive(true);
                }
                else
                {
                    enemyAsignedDialog.enableMovement = true;
                    //inputManager.player1.enableMovement = true;
                    inputManager.player1.enableMovementPlayer = true;


                }
                gameObject.SetActive(false);
            }
        }
    }
}
