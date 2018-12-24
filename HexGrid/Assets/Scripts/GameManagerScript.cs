using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    private int playersNumber;
    private int player;
    private Color32[] colors;
    private GameObject[] grid;
    private HexScript hexScript;
    private IGameStates gameState;

    public float size;
    public int gridDimension;
    public Material hexMaterial;

    public Sprite spr;
    public GameObject menuCanvas;

    private GameObject menuPanel;
    private GameObject[,] menuButt;
    private List<GameObject> menuTex;
    private string[,] menuNames;

    private GameObject endMenuPanel;
    private GameObject endMenuScore;
    private GameObject[,] endMenuButt;
    private List<GameObject> endMenuTex;
    private string[,] endMenuNames;

    private GuiScript guiScr;
    public action onClick;

    public int PlayersNumber
    {
        get
        {
            return playersNumber;
        }

        set
        {
            playersNumber = value;
        }
    }

    void Awake()
    {
        colors = new Color32[5];
        //inactive color
        colors[0] = new Color32(255, 255, 255, 255);
        //player colors
        colors[1] = new Color32(255, 0, 0, 255);
        colors[2] = new Color32(0, 255, 0, 255);
        colors[3] = new Color32(0, 0, 255, 255);
        colors[4] = new Color32(255, 255, 0, 255);

        PlayersNumber = 2;
        player = 1;

        menuNames = new string[1, 5];
        menuNames[0, 0] = "1 PLAYER";
        menuNames[0, 1] = "2 PLAYERS";
        menuNames[0, 2] = "3 PLAYERS";
        menuNames[0, 3] = "4 PLAYERS";
        menuNames[0, 4] = "EXIT";

        endMenuNames = new string[2, 1];
        endMenuNames[0, 0] = "MAIN MENU";
        endMenuNames[1, 0] = "EXIT";
    }

    // Use this for initialization
	void Start ()
    {
        guiScr = new GuiScript();
        menuPanel = guiScr.CreatePanel(menuCanvas, "MenuPanel", new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector3(1, 1, 1), new Vector3(0, 0, 0),
           new Vector2(200, 400), new Vector2(0, 0), spr, new Color32(255, 255, 255, 0));
        menuButt = guiScr.FillWithButtons(menuPanel, 1, 5, 200, 80, spr, new Color32(255, 255, 255, 255));
        menuTex = guiScr.SetMenuText(menuButt, menuNames);

        onClick = new action(SetUpGame);

        for (int i = 0; i < menuButt.GetLength(1)-1; i++)
        {
            int x = i;
            guiScr.SetAction(menuButt[0,x], (x+1), onClick);
        }

        menuButt[0, menuButt.GetLength(1) - 1].GetComponent<Button>().onClick.AddListener(delegate
        { Application.Quit(); });

        ChangeState(new IPlayerTurn());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ChangeState(IGameStates newState)
    {
        if (gameState != null)
        {
            gameState.OnExit();
        }
        gameState = newState;
        gameState.OnEnter(this);
    }

    public void ExecuteState(GameObject hex)
    {
        if (hex.GetComponent<MeshRenderer>().material.color == colors[0])
            gameState.Execute(hex, playersNumber);
    }

    public void SetUpGame(int players)
    {
        hexScript = gameObject.AddComponent<HexScript>();
        grid = hexScript.CreateGrid(gridDimension, size, hexMaterial);
        colors[0] = hexMaterial.color;
        PlayersNumber = players;
        Destroy(menuPanel);
    }

    public void PlayHex(GameObject hex)
    {
        foreach (GameObject go in grid)
        {
            if (Vector3.Distance(hex.transform.position, go.transform.position) <= 2 * size)
            {
                Color32 color = colors[player];
                go.GetComponent<HexActionScript>().ChangeColor(color);
            }                    
        }

        if (IsEnd())
        {
            int[] pkt = CountPoints();
            ShowEndMenu(pkt);
        }
        if (PlayersNumber > 1)
        {
            player += 1;
            if (player > PlayersNumber)
                player = 1;
        }
    }

    private bool IsEnd()
    {
        for (int i = 0; i < grid.Length; i++)
        {
            if (grid[i].GetComponent<MeshRenderer>().material.color == colors[0])
                return false;
        }
        return true;
    }

    private int[] CountPoints()
    {
        int x = Mathf.Max(PlayersNumber, 2);
        int[] points = new int[x];

        for (int i = 0; i < x; i++)
            points[i] = 0;


        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < x; j++)
            {
                if (grid[i].GetComponent<MeshRenderer>().material.color == colors[j + 1])
                    points[j] += 1;
            }
        }

        return points;
    }

    private void ShowEndMenu(int[] pkt)
    {
        endMenuPanel = guiScr.CreatePanel(menuCanvas, "EndMenuPanel", new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector3(1, 1, 1), new Vector3(0, 0, 0),
          new Vector2(400, 400), new Vector2(0, 0), spr, new Color32(255, 255, 255, 125));
        endMenuButt = guiScr.FillWithButtons(endMenuPanel, 2, 1, 200, 100, spr, new Color32(255, 255, 255, 255));
        endMenuTex = guiScr.SetMenuText(endMenuButt, endMenuNames);
        endMenuScore = guiScr.CreateTextField(endMenuPanel, "Score");

        string scor = "";
        if (PlayersNumber > 1)
        {
            scor += ("RED : " + pkt[0].ToString() + "\n");
            scor += ("GREEN : " + pkt[1].ToString() + "\n");
            if (playersNumber >= 3)
            {
                scor += ("BLUE : " + pkt[2].ToString() + "\n");
            }
            if (playersNumber >= 4)
            {
                scor += ("YELLOW : " + pkt[3].ToString() + "\n");
            }
        }
        else
        {
            scor += ("PLAYER : " + pkt[0].ToString() + "\n");
            scor += ("AI : " + pkt[1].ToString() + "\n");
        }
        endMenuScore.GetComponent<Text>().text = scor;

        endMenuButt[0, 0].GetComponent<Button>().onClick.AddListener(delegate
        { SceneManager.LoadScene(SceneManager.GetActiveScene().name); });
        endMenuButt[1, 0].GetComponent<Button>().onClick.AddListener(delegate
        { Application.Quit(); });
    }

    public void EnemyMove()
    {
        StartCoroutine(EnemyMoveInTime());
    }

    public IEnumerator EnemyMoveInTime()
    {
        player = 2;
        yield return new WaitForSeconds(0.5f);
        List<GameObject> availableHex = new List<GameObject>();
        for (int i = 0; i < grid.Length; i++)
        {
            if (grid[i].GetComponent<MeshRenderer>().material.color == colors[0])
            {
                availableHex.Add(grid[i]);
            }
        }
        if (availableHex.Count > 0)
        {
            int index = Random.Range(0, availableHex.Count);
            StartCoroutine(availableHex[index].GetComponent<HexActionScript>().Choose(colors[2]));
            yield return new WaitForSeconds(1.5f);
            PlayHex(availableHex[index]);
            ChangeState(new IPlayerTurn());
            player = 1;
        }
    }
}
