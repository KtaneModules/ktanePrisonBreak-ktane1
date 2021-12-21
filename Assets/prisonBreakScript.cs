using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class prisonBreakScript : MonoBehaviour
{
    public new KMAudio audio;
    public KMAudio.KMAudioRef audioRef;
    public KMBombInfo bomb;
	public KMBombModule module;
    public KMSelectable[] arrowButtons;
    public KMSelectable jail;
    public GameObject jailBars;
    public GameObject prisoner, arrowControl;
    public TextMesh arrow, timer, leftScreen, rightScreen;
    public GameObject background;
    public Material[] mazeBG;

    //Maze stuff
    private string[] maze = new string[]
    {
        "-------------------------" + //12 by 12 Maze
        "|o o|o o o o|o o o|o o o|" +
        "| + + +-+-+-+ +-+ +-+ + |" +
        "|o|o|o|o o o|o|o o o o|o|" +
        "| +-+ + +-+-+ + +-+-+-+ |" +
        "|o|o o|o|o o o|o|o o o|o|" +
        "| + + + + +-+-+ + +-+-+ |" +
        "|o o|o|o|o|o o o o|o|o o|" +
        "|-+-+ + + + +-+-+ + + +-|" +
        "|o o o|o o|o|o o|o|o|o|o|" +
        "| +-+-+-+ + + + +-+ + + |" +
        "|o o o|o o|o o|o|o o|o o|" +
        "|-+-+ +-+-+-+-+ + +-+-+-|" +
        "|o o|o|o o|o o o|o o o|o|" +
        "|-+ + + + +-+ + + +-+ + |" +
        "|o o|o o|o o o|o|o o|o|o|" +
        "| +-+-+-+-+-+-+ + + + + |" +
        "|o o o o o o|o o|o|o|o|o|" +
        "|-+-+ + +-+ + +-+ + + + |" +
        "|o o o|o|o o|o o|o|o|o o|" +
        "| +-+-+-+ +-+-+ + + +-+-|" +
        "|o|o o o|o o o|o o|o o o|" +
        "| + +-+ +-+-+ +-+-+-+-+ |" +
        "|o o|o o o o|o o o o o o|" +
        "-------------------------",

        "-------------------------" + 
        "|o|o o o o|o o o|o|o o|o|" +
        "| + +-+-+ + +-+ + + + + |" +
        "|o|o|o o|o o|o o o|o|o o|" +
        "| + + + +-+-+-+-+ + +-+-|" +
        "|o|o o|o o o|o o o|o|o o|" +
        "| +-+-+ +-+-+ +-+-+ + + |" +
        "|o o o o|o o|o o o o o|o|" +
        "|-+-+-+ + + +-+-+-+-+-+-|" +
        "|o o o o|o|o o o|o o o o|" +
        "| + + +-+ +-+-+ + +-+-+ |" +
        "|o|o|o o o o o|o|o|o o o|" +
        "|-+ +-+-+-+-+-+ + + + +-|" +
        "|o o|o o|o o o|o|o|o|o o|" +
        "| +-+ + + +-+ + + +-+-+ |" +
        "|o o o|o|o|o|o|o o|o o|o|" +
        "|-+-+-+-+ + + + +-+ +-+ |" +
        "|o o o o o|o|o o|o|o|o o|" +
        "| +-+-+-+-+ +-+-+ + + +-|" +
        "|o|o o o o|o o o o|o|o o|" +
        "| + +-+-+ +-+-+-+ + + + |" +
        "|o|o o o|o|o o o o|o o|o|" +
        "| +-+-+ + +-+-+ +-+-+-+ |" +
        "|o o o o|o o o|o o o o o|" +
        "-------------------------",

        "-------------------------" +
        "|o o o|o o o o o|o o o o|" +
        "| +-+ + + +-+-+ + + +-+ |" +
        "|o|o|o o|o o o|o o|o|o o|" +
        "| + +-+-+-+-+ +-+-+ + +-|" +
        "|o|o|o o o o o|o o o|o|o|" +
        "| + + +-+-+ +-+-+-+-+ + |" +
        "|o|o|o|o o|o o o|o o|o o|" +
        "| + + +-+ + +-+ + +-+-+ |" +
        "|o o|o|o o|o o|o|o o o|o|" +
        "|-+-+ + +-+-+-+ +-+-+ + |" +
        "|o o|o o|o o o|o o o|o o|" +
        "| + +-+ + +-+ + +-+ + +-|" +
        "|o|o|o o|o o|o|o o|o|o o|" +
        "|-+ + +-+ +-+ +-+ + +-+ |" +
        "|o o|o o|o o|o o o|o|o o|" +
        "| + + +-+-+ +-+-+-+ +-+-|" +
        "|o|o|o o o|o|o o|o|o o o|" +
        "|-+ +-+-+ + + + + +-+-+ |" +
        "|o o|o o o|o o|o|o o|o|o|" +
        "| +-+ + +-+-+-+ +-+ + + |" +
        "|o o o|o o|o o o|o|o o o|" +
        "|-+-+-+-+ + +-+-+ +-+-+ |" +
        "|o o o o o|o o|o o o o o|" +
        "-------------------------",

        "-------------------------" + 
        "|o o o|o o|o|o o|o o o o|" +
        "| +-+ + +-+ + + + + +-+ |" +
        "|o|o o|o o|o|o|o o|o|o o|" +
        "| + +-+-+ + +-+-+-+-+ + |" +
        "|o|o o o|o|o o o o|o o|o|" +
        "| +-+-+ + +-+-+-+ + +-+ |" +
        "|o|o o|o|o o o|o|o o|o o|" +
        "| + + + +-+ + + +-+-+-+ |" +
        "|o|o|o|o o o|o|o|o o|o o|" +
        "| + + +-+-+-+ + + +-+ +-|" +
        "|o|o|o|o o|o o o|o o o|o|" +
        "| +-+ + +-+ +-+-+ +-+-+ |" +
        "|o o o|o o|o o o o|o o o|" +
        "|-+ +-+-+ +-+-+-+-+ +-+ |" +
        "|o o|o o o|o o o o|o o|o|" +
        "| +-+ +-+ + +-+-+ +-+-+ |" +
        "|o o o o|o|o|o o o|o o|o|" +
        "|-+-+ + + + + +-+-+ + + |" +
        "|o o|o|o|o o|o|o o|o|o o|" +
        "| + + + +-+-+ + + + +-+-|" +
        "|o|o|o|o o o|o o|o|o o o|" +
        "| +-+ +-+-+ + +-+-+-+ + |" +
        "|o o o|o o o|o o o o o|o|" +
        "-------------------------",

        "-------------------------" +
        "|o o o o o o o|o o|o|o o|" +
        "| +-+-+ +-+-+-+ + + + + |" +
        "|o o|o o|o o o|o|o o o|o|" +
        "| + + +-+ +-+ + +-+ + +-|" +
        "|o|o|o o o|o o|o|o o|o o|" +
        "| + +-+ +-+-+ + + +-+ + |" +
        "|o|o o|o|o o|o o|o o|o|o|" +
        "|-+-+-+ + + +-+-+-+-+ + |" +
        "|o o o|o o|o o|o o o o|o|" +
        "| +-+ +-+-+-+-+ +-+ +-+-|" +
        "|o o|o o o o|o o|o|o o o|" +
        "|-+-+-+ +-+ + +-+ +-+-+ |" +
        "|o o o|o o|o o o|o|o o|o|" +
        "|-+-+ + + +-+-+ + + + + |" +
        "|o o|o|o|o|o o o|o o|o|o|" +
        "|-+ + +-+ + +-+-+ +-+ + |" +
        "|o o|o o o|o|o o|o o|o|o|" +
        "| +-+-+-+-+ +-+ +-+ +-+ |" +
        "|o|o o o o|o o o o|o o o|" +
        "| + +-+-+ + +-+-+ +-+-+ |" +
        "|o o|o o|o o|o o o|o o|o|" +
        "|-+-+ + +-+-+ + +-+ +-+ |" +
        "|o o o|o o o o|o o|o o o|" +
        "-------------------------",
    };
    private int[] cellNumber =
    {
        6, 0, 11, 2, 11, 5, 6, 11, 4, 11, 0, 9, 0, 5, 1, 1, 9, 3, 3, 9, 2, 10, 1, 5, 5, 3, 6, 5, 4, 8,
        0, 0, 5, 0, 11, 6, 7, 5, 8, 8, 7, 10, 7, 3, 5, 6, 6, 9, 1, 6, 0, 11, 0, 8, 3, 11, 2, 5, 10, 5,
        6, 5, 4, 6, 3, 3, 7, 3, 8, 8, 3, 9, 2, 7, 1, 1, 6, 0, 8, 0, 9, 10, 7, 10, 2, 11, 11, 6, 11, 7, 
        5, 11, 5, 1, 10, 1, 10, 8, 3, 7, 1, 6, 5, 4, 11, 3, 0, 5, 1, 9, 11, 11, 0, 4, 4, 9, 3, 10, 7, 10,
        0, 4, 2, 5, 3, 2, 3, 9, 4, 6, 4, 11, 5, 1, 5, 8, 7, 0, 7, 3, 8, 6, 8, 10, 10, 10, 11, 0, 11, 8
    };     
    private int cell;
    private int currentPos;
    private int goalPos;
    private int mazeId;
    private int activations = 0;

    private string[] directions = new string[] { "North", "East", "South", "West" };
    private string[] fullDirections = new string[] { "North", "Northeast", "East", "Southeast", "South", "Southwest", "West", "Northwest" };
    private string[] prisonNames = new string[] { "Oozora Prison Department", "Green Dolphin Street Prison", "Center Perks 2.0", "Rattlesnake Springs", "Fort Tundra" };
    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private string initialString;
    private List<int> goalPath = new List<int>();
    private List<int> encryptedPath = new List<int>();
    private List<int> decryptedPath = new List<int>();

    private static int moduleIdCounter = 1;
    private int moduleId;
    private bool moduleActivated, isAnimating, cooldown, reset = true, moduleSolved; 

    void Awake()
    {
    	moduleId = moduleIdCounter++;
        prisoner.SetActive(true);
        arrowControl.SetActive(false);
        mazeId = UnityEngine.Random.Range(0, mazeBG.Length);
        background.GetComponent<MeshRenderer>().material = mazeBG[mazeId];
        module.GetComponent<KMSelectable>().OnInteract += delegate
        {
            if (!moduleSolved && !moduleActivated && !isAnimating && !reset)
            {
                activations++;
                leftScreen.text = (cell+1).ToString();
                rightScreen.text = alphabet[(currentPos % 25 - 1) / 2] + ((currentPos / 25 - 1) / 2 + 1).ToString();
                StartCoroutine(EscapeAnim());
                StartCoroutine(Timer());
                pathFinder();
                arrowModification();
            }
            return true;
        };
        for (int i = 0; i < arrowButtons.Length; i++)
        {
            int k = i;
            arrowButtons[k].OnInteract += () => { move(k); return false; };
        }
        jail.OnInteract += () => { submit(); return false; };
    }

    void Start()
    {
        prisoner.SetActive(false);
        leftScreen.text = "";
        rightScreen.text = "";
        timer.text = "-:--";
        cell = UnityEngine.Random.Range(0, 15);
        currentPos = 25 * (2 * UnityEngine.Random.Range(0, 12) + 1) + 2 * UnityEngine.Random.Range(0, 12) + 1; // 25 * Row + Column, and each cell is denoted as 2n+1
        goalPos = 25 * (2 * cellNumber[30 * mazeId + cell * 2] + 1) + 2 * cellNumber[30 * mazeId + cell * 2 + 1] + 1;
        Debug.LogFormat("[Prison Break #{0}] The prison you're currently in is {1}.", moduleId, prisonNames[mazeId]);
        Debug.LogFormat("[Prison Break #{0}] You are currently at {1}.", moduleId, alphabet[(currentPos % 25 - 1) / 2] + ((currentPos / 25 - 1) / 2 + 1).ToString());
        Debug.LogFormat("[Prison Break #{0}] The prisoner's currently at cell {1}.", moduleId, cell + 1);
        var sn = bomb.GetSerialNumber();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < sn.Length; i++)
        {
            if (alphabet.Contains(sn[i]))
            {
                sb.Append(sn[i] - 64);
            }
            else
            {
                sb.Append(sn[i]);
            }
        }
        Debug.LogFormat("[Prison Break #{0}] The starting string is {1}.", moduleId, sb.ToString());
        for (int i = 1; i < sb.Length; i++)
        {
            if (sb[i-1] % 2 == 0)
            {
                sb[i] = Convert.ToChar((sb[i] - '0' + 5) % 10 + '0');
            }
        }
        Debug.LogFormat("[Prison Break #{0}] Applying the second instruction: {1}", moduleId, sb.ToString());
        Debug.LogFormat("[Prison Break #{0}] Let's hope nothing goes wrong today...", moduleId);
        initialString = sb.ToString();
        reset = false;
    }

    IEnumerator EscapeAnim()
    {
        moduleActivated = true;
        isAnimating = true;
        arrowControl.SetActive(true);
        Debug.LogFormat("[Prison Break #{0}] Prisoner 1201-111 is not in her cell again! Sound the alarms!", moduleId);
        Debug.LogFormat("[Prison Break #{0}] Number of escape attempts: {1}", moduleId, activations);
        float current = jailBars.gameObject.transform.localScale.z;
        audio.PlaySoundAtTransform("Escaped", transform);
        while (current > 0)
        {
            jailBars.gameObject.transform.localScale = new Vector3(17.85714f, 1000f, current);
            current -= 0.25f;
            yield return null;
        }
        jailBars.gameObject.transform.localScale = new Vector3(17.85714f, 1000f, 0f);
        isAnimating = false;
    }

    IEnumerator Escape()
    {
        int waitForIt = UnityEngine.Random.Range(0, 5);
        cell = UnityEngine.Random.Range(0, 15);
        goalPos = 25 * (2 * cellNumber[30 * mazeId + cell * 2] + 1) + 2 * cellNumber[30 * mazeId + cell * 2 + 1] + 1;
        var sn = bomb.GetSerialNumber();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < sn.Length; i++)
        {
            if (alphabet.Contains(sn[i]))
            {
                sb.Append(sn[i] - 64);
            }
            else
            {
                sb.Append(sn[i]);
            }
        }
        for (int i = 1; i < sb.Length; i++)
        {
            if (sb[i - 1] % 2 == 0)
            {
                sb[i] = Convert.ToChar((sb[i] - '0' + 5) % 10 + '0');
            }
        }
        Debug.LogFormat("[Prison Break #{0}] You are currently at {1}.", moduleId, alphabet[(currentPos % 25 - 1) / 2] + ((currentPos / 25 - 1) / 2 + 1).ToString());
        Debug.LogFormat("[Prison Break #{0}] The prisoner's currently at cell {1}.", moduleId, cell + 1);
        Debug.LogFormat("[Prison Break #{0}] Let's hope nothing goes wrong this time...", moduleId);
        initialString = sb.ToString();
        yield return new WaitForSeconds(waitForIt);
        prisoner.SetActive(false);
        reset = false;
    }

    void move(int k)
    {
        if (!moduleSolved && !cooldown && moduleActivated && !reset)
        {
            bool wall = false;
            int offset = 0;
            switch (k)
            {
                case (0)://North
                    offset = -25;
                    break;
                case (1)://East
                    offset = 1;
                    break;
                case (2)://South
                    offset = 25;
                    break;
                case (3)://West
                    offset = -1;
                    break;
            }
            switch (maze[mazeId][currentPos + offset])
            {
                case ' ':
                    wall = false;
                    break;
                case '-':
                case '|':
                case '+':
                    wall = true;
                    break;
            }
            if (wall)
            {
                audio.PlaySoundAtTransform("Wall hit", transform);
                Debug.LogFormat("<Prison Break #{0}> You hit a wall! Cooldown activated!", moduleId);
                StartCoroutine(cooldownTimer());
            }
            else
            {
                audio.PlaySoundAtTransform("Step", transform);
                currentPos += 2 * offset;
                rightScreen.text = alphabet[(currentPos % 25 - 1) / 2] + ((currentPos / 25 - 1) / 2 + 1).ToString();
                Debug.LogFormat("<Prison Break #{0}> Moved to {1}.", moduleId, alphabet[(currentPos % 25 - 1) / 2] + ((currentPos / 25 - 1) / 2 + 1).ToString());
            }
        }
    }

    IEnumerator cooldownTimer()
    {
        cooldown = true;
        for (int i = 2; i >= 1; i--)
        {
            rightScreen.color = new Color(0f, 0f, 1f, 1f);
            rightScreen.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        cooldown = false;
        rightScreen.color = new Color(1f, 0f, 0f, 1f);
        rightScreen.text = alphabet[(currentPos % 25 - 1) / 2] + ((currentPos / 25 - 1) / 2 + 1).ToString();
    }
    void pathFinder()
    {
        int pathLength = UnityEngine.Random.Range(8, 13);
        List<List<int>> pathList = new List<List<int>>();
        List<int> tempList = new List<int>();
        int offset = 0;
        for (int i = 0; i < pathLength; i++)
        {
            if (i == 0)//First case: Checking which directions can take
            {
                for (int j = 0; j < directions.Length; j++)
                {
                    switch (j)
                    {
                        case (0)://North
                            offset = -25;
                            break;
                        case (1)://East
                            offset = 1;
                            break;
                        case (2)://South
                            offset = 25;
                            break;
                        case (3)://West
                            offset = -1;
                            break;
                    }
                    switch (maze[mazeId][goalPos + offset])
                    {
                        case ' ':
                            tempList.Add(2*offset);
                            pathList.Add(tempList);
                            tempList = new List<int>();
                            offset = 0;
                            break;
                        case '-':
                        case '|':
                        case '+':
                            break;
                    }
                }
            }
            else 
            {
                int prevListCount = pathList.Count;
                for (int j = 0; j < prevListCount; j++)
                {
                    for (int k = 0; k < directions.Length; k++)//Checking for each movement
                    {
                        switch (k)
                        {
                            case (0)://North
                                if (pathList[j][pathList[j].Count - 1] != 50) { offset = -25; }
                                break;
                            case (1)://East
                                if (pathList[j][pathList[j].Count - 1] != -2) { offset = 1; }
                                break;
                            case (2)://South
                                if (pathList[j][pathList[j].Count - 1] != -50) { offset = 25; }
                                break;
                            case (3)://West
                                if (pathList[j][pathList[j].Count - 1] != 2) { offset = -1; }
                                break;
                        }
                        if (offset != 0 && maze[mazeId][goalPos + pathList[j].Sum() + offset] == ' ')//Available movement is added to the list
                        {
                            for (int l = 0; l < pathList[j].Count; l++)
                            {
                                tempList.Add(pathList[j][l]);
                            }
                            tempList.Add(2*offset);
                            pathList.Add(tempList);
                            tempList = new List<int>();
                        }
                        offset = 0;
                    }
                }
                for (int j = 0; j < prevListCount; j++)//Removing the previous movement lists
                {
                    pathList.RemoveAt(0);
                }
            }
        }
        //Logging
       /* StringBuilder sb = new StringBuilder();
        for (int i = 0; i < pathList.Count; i++)
        {
            for (int j = 0; j < pathList[i].Count; j++)
            {
                switch (pathList[i][j])
                {
                    case (-50)://North
                        sb.Append("North ");
                        break;
                    case (2)://East
                        sb.Append("East ");
                        break;
                    case (50)://South
                        sb.Append("South ");
                        break;
                    case (-2)://West
                        sb.Append("West ");
                        break;
                }
            }
            Debug.LogFormat("Path {0}: {1}", i, sb.ToString());
            sb.Remove(0, sb.Length);
        }*/
        int pathChooser = UnityEngine.Random.Range(0, pathList.Count);
        goalPath = new List<int>();
        for (int i = 0; i < pathList[pathChooser].Count; i++)
        {
            switch (pathList[pathChooser][i])
            {
                case (-50)://North
                    goalPath.Add(0);
                    break;
                case (2)://East
                    goalPath.Add(2);
                    break;
                case (50)://South
                    goalPath.Add(4);
                    break;
                case (-2)://West
                    goalPath.Add(6);
                    break;
            }
        }
        goalPos += pathList[pathChooser].Sum();//Applying the path to the prisoner

        int rnd = 0;//(okay I probably need to make this two methods)
        decryptedPath = new List<int>();
        for (int i = 1; i < goalPath.Count; i++)//Combining two cardinal dir. to form one ordinal dir.
        {
            rnd = UnityEngine.Random.Range(0, 2);
            if (rnd == 0)
            {
                if ((goalPath[i] == 0 && goalPath[i - 1] == 2) || (goalPath[i] == 2 && goalPath[i - 1] == 0))//Northeast
                {
                    decryptedPath.Add(1);
                    i++;
                }
                else if ((goalPath[i] == 0 && goalPath[i - 1] == 6) || (goalPath[i] == 6 && goalPath[i - 1] == 0))//Northwest
                {
                    decryptedPath.Add(7);
                    i++;
                }
                else if ((goalPath[i] == 4 && goalPath[i - 1] == 2) || (goalPath[i] == 2 && goalPath[i - 1] == 4))//Southeast
                {
                    decryptedPath.Add(3);
                    i++;
                }
                else if ((goalPath[i] == 4 && goalPath[i - 1] == 6) || (goalPath[i] == 6 && goalPath[i - 1] == 4))//Southwest
                {
                    decryptedPath.Add(5);
                    i++;
                }
                else
                {
                    decryptedPath.Add(goalPath[i - 1]);
                }
            }
            else
            {
                decryptedPath.Add(goalPath[i-1]);
            }
            if (i == goalPath.Count - 1) { decryptedPath.Add(goalPath[i]); }
        }
    }

    void arrowModification()
    {
        int mid = 0;
        StringBuilder sb = new StringBuilder();
        while (initialString.Length % 2 != decryptedPath.Count % 2 || initialString.Length < decryptedPath.Count)//The middle chunk of instructions
        {
            if (initialString.Length % 2 != decryptedPath.Count % 2)
            {
                mid = (initialString[0] - '0' + initialString[initialString.Length - 1] - '0') % 10;
                for (int i = 0; i < initialString.Length; i++)
                {
                    if (i == initialString.Length / 2)
                    {
                        sb.Append(mid);
                    }
                    sb.Append(initialString[i]);
                }
                initialString = sb.ToString();
                sb.Remove(0, sb.Length);
            }
            if (initialString.Length < decryptedPath.Count)
            {
                sb.Append(initialString);
                for (int i = 0; i < initialString.Length; i++)
                {
                    sb.Append((initialString[i] + 1) % 10);                   
                }
                initialString = sb.ToString();
                sb.Remove(0, sb.Length);
            }
        }
        Debug.LogFormat("[Prison Break #{0}] Applying the third instruction: {1}.", moduleId, initialString);
        for (int i = (initialString.Length - decryptedPath.Count) / 2; i < initialString.Length - (initialString.Length - decryptedPath.Count) / 2; i++)//Taking the middle string
        {
            sb.Append(initialString[i]);
        }
        Debug.LogFormat("[Prison Break #{0}] The taken middle string to modify directions is {1}.", moduleId, sb.ToString());
        int rotate = activations % 2 == 0 ? -1 : 1;//Inverted because it's encrypting
        initialString = sb.ToString();
        encryptedPath = new List<int>();
        for (int i = 0; i < decryptedPath.Count; i++)//Arrow modification
        {
            if (initialString[i] == '5')
            {
                encryptedPath.Add(decryptedPath[i]);
            }
            else if(initialString[i] == '2' || initialString[i] == '3' || initialString[i] == '7')
            {

                encryptedPath.Add((decryptedPath[i] + 4) % 8);
            }
            else
            {
                if (initialString[i] == '1' || initialString[i] == '4' || initialString[i] == '9')
                {
                    encryptedPath.Add(((decryptedPath[i] + rotate) % 8 + 8 ) % 8);
                }
                else
                {
                    encryptedPath.Add(((decryptedPath[i] + 2*rotate) % 8 + 8) % 8);

                }
                rotate = rotate * -1;
            }
        }
        sb.Remove(0, sb.Length);
        foreach (int k in encryptedPath)//For logging
        {
            sb.AppendFormat(" {0}", fullDirections[k]);
        }
        Debug.LogFormat("[Prison Break #{0}] The shown encrypted directions are {1}.", moduleId, sb.ToString());
        StringBuilder logDirections = new StringBuilder();
        foreach (int k in decryptedPath)//For logging
        {
            logDirections.AppendFormat(" {0}", fullDirections[k]);
        }
        Debug.LogFormat("[Prison Break #{0}] The full decrypted path is{1}, getting the prisoner to {2}.", moduleId, logDirections.ToString(), alphabet[(goalPos % 25 - 1) / 2] + ((goalPos / 25 - 1) / 2 + 1).ToString());
        StartCoroutine(arrowSequenceFlash());
    }

    IEnumerator arrowSequenceFlash()
    {
        int index = 0;
        float rotate = arrowControl.gameObject.transform.localEulerAngles.y;
        while (moduleActivated && !moduleSolved)
        {
            arrow.text = "";
            rotate = 0;
            arrowControl.gameObject.transform.localEulerAngles = new Vector3(90f, rotate, 0f);
            yield return new WaitForSeconds(0.15f);
            rotate = encryptedPath[index] * 45;
            arrowControl.gameObject.transform.localEulerAngles = new Vector3(90f, rotate, 0f);
            arrow.text = "↓";
            yield return new WaitForSeconds(0.15f);
            index++;
            if (index >= encryptedPath.Count) { index = 0; arrow.text = ""; yield return new WaitForSeconds(0.3f); }
        }
        yield return null;
    }

    void submit()
    {
        if (!isAnimating && !moduleSolved && !reset && !cooldown)
        {
            moduleActivated = false;
            timer.text = "";
            leftScreen.text = "";
            rightScreen.text = "";
            if (currentPos == goalPos)
            {
                moduleSolved = true;
            }
            else
            {
                module.HandleStrike();
                Debug.LogFormat("[Prison Break #{0}] You secured the wrong cell, and the prisoner had escaped! Strike!", moduleId);
                reset = true;
                StartCoroutine(Timer());
            }
            StartCoroutine(CloseAnim());
        }
    }

    IEnumerator CloseAnim()
    {
        isAnimating = true;
        arrowControl.SetActive(false);
        float current = jailBars.gameObject.transform.localScale.z;
        if (moduleSolved)
        {
            audio.PlaySoundAtTransform("Prisoner Scream", transform);
            prisoner.SetActive(true);
        }
        else
        {
            audio.PlaySoundAtTransform("Prisoner Laugh", transform);

        }
        audioRef = audio.PlaySoundAtTransformWithRef("Cell Closing", transform);
        while (current < 17.85714f)
        {
            jailBars.gameObject.transform.localScale = new Vector3(17.85714f, 1000f, current);
            current += 0.25f;
            yield return null;
        }
        if (audioRef != null && audioRef.StopSound != null)
        {
            audioRef.StopSound();
        }
        audio.PlaySoundAtTransform("Cell Closed", transform);
        jailBars.gameObject.transform.localScale = new Vector3(17.85714f, 1000f, 17.85714f); 
        isAnimating = false;
        if (moduleSolved)
        {
            Debug.LogFormat("[Prison Break #{0}] Path secured, and the prisoner is now trapped and caught! Module solved, good job.", moduleId);
            module.HandlePass();
            timer.text = "GG";
            timer.color = new Color(0f, 1f, 0f, 1f);
            leftScreen.text = "GOOD";
            leftScreen.color = new Color(0f, 1f, 0f, 1f);
            rightScreen.text = "JOB";
            rightScreen.color = new Color(0f, 1f, 0f, 1f);
        }
    }

    IEnumerator Timer()
    {
        if (!reset)
        {
            timer.text = "5:00";
            timer.color = new Color(1f, 0f, 0f, 1f);
            yield return new WaitForSeconds(1f);
            for (int m = 4; m >= 0 && moduleActivated; m--)
                for (int s = 59; s >= 0 && moduleActivated; s--)
                {
                    timer.text = m + ":" + s.ToString("00");
                    yield return new WaitForSeconds(1f);
                }
            if (moduleActivated)
            {
                Debug.LogFormat("[Prison Break #{0}] Time has ran out, and the prisoner has escaped! Strike!", moduleId);
                module.HandleStrike();
                reset = true;
                StartCoroutine(Timer());
                StartCoroutine(CloseAnim());
            }
        }
        else
        {
            timer.text = "30";
            timer.color = new Color(0f, 0f, 1f, 1f);
            yield return new WaitForSeconds(1f);
            for (int s = 29; s >= 0 && reset; s--)
            {
                timer.text = s.ToString("00");
                yield return new WaitForSeconds(1f);
            }
            timer.text = "-:--";
            timer.color = new Color(1f, 0f, 0f, 1f);
            prisoner.SetActive(true);
            Debug.LogFormat("[Prison Break #{0}] The prisoner has been caught back to her cell.", moduleId);
            StartCoroutine(Escape());
        }
    }
    //Twitch Plays
#pragma warning disable 414
    private readonly string TwitchHelpMessage = @"<!{0} start> to sound the alarms, <!{0} urdl> to move up right down left, <!{0} submit> to press the display";
#pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string command)
    {
        yield return null;
        command = command.ToLowerInvariant();
        if (command == "start")
        {
            module.GetComponent<KMSelectable>().OnInteract();
            yield return "strike";
            yield return "solve";
        }
        else
        {
            if (moduleActivated)
            {
                string[] parameters = command.Split(' ');
                if (parameters.Length == 1)
                {
                    if (Regex.IsMatch(parameters[0], @"^\s*submit\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
                    {
                        jail.OnInteract();
                        yield return null;
                    }
                    else if (!cooldown)
                    {
                        List<KMSelectable> movement = new List<KMSelectable>();
                        for (int i = 0; i < command.Length; i++)
                        {
                            switch (command[i])
                            {
                                case 'u':
                                case 'n':
                                    movement.Add(arrowButtons[0]);
                                    break;
                                case 'r':
                                case 'e':
                                    movement.Add(arrowButtons[1]);
                                    break;
                                case 'd':
                                case 's':
                                    movement.Add(arrowButtons[2]);
                                    break;
                                case 'l':
                                case 'w':
                                    movement.Add(arrowButtons[3]);
                                    break;
                                default:
                                    yield return "sendtochaterror One of the characters in the command is invalid.";
                                    yield break;
                            }
                        }
                        int counter = 0;
                        foreach (KMSelectable btn in movement)
                        {
                            btn.OnInteract();
                            if (cooldown)
                            {
                                yield return "sendtochaterror Movement #" + counter + " caused you to hit a wall!";
                                yield break;
                            }
                            counter++;
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    else
                    {
                        yield return "sendtochaterror Movement cooldown is still activated!";
                        yield break;
                    }
                }
            }
            else
            {
                yield return "sendtochaterror The alarms are not activated yet.";
                yield break;
            }
        }
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        while (reset) yield return true;
        if (!moduleActivated)
            module.GetComponent<KMSelectable>().OnInteract();
        if (!moduleSolved)
        {
            while (isAnimating || cooldown) yield return null;
            var q = new Queue<int>();
            var allMoves = new List<Movement>();
            q.Enqueue(currentPos);
            while (q.Count > 0)
            {
                var next = q.Dequeue();
                if (next == goalPos)
                    goto readyToSubmit;
                var allDirections = "URDL";
                var offsets = new int[] { -25, 1, 25, -1 };
                string paths = "";
                for (int i = 0; i < 4; i++)
                {
                    switch (maze[mazeId][next + offsets[i]])
                    {
                        case ' ':
                            paths += allDirections[i];
                            break;
                    }
                }
                var cell = paths;
                for (int i = 0; i < 4; i++)
                {
                    if (cell.Contains(allDirections[i]) && !allMoves.Any(x => x.start == next + offsets[i] * 2))
                    {
                        q.Enqueue(next + offsets[i] * 2);
                        allMoves.Add(new Movement(next, next + offsets[i] * 2, i));
                    }
                }
            }
            throw new InvalidOperationException("There is a bug in Prison Break's TP autosolver.");
            readyToSubmit:
            if (allMoves.Count != 0) // Checks for position already being target
            {
                var lastMove = allMoves.First(x => x.end == goalPos);
                var relevantMoves = new List<Movement> { lastMove };
                while (lastMove.start != currentPos)
                {
                    lastMove = allMoves.First(x => x.end == lastMove.start);
                    relevantMoves.Add(lastMove);
                }
                for (int i = 0; i < relevantMoves.Count; i++)
                {
                    arrowButtons[relevantMoves[relevantMoves.Count - 1 - i].direction].OnInteract();
                    yield return new WaitForSeconds(.1f);
                }
            }
            jail.OnInteract();
        }
        while (isAnimating) yield return true;
    }

    class Movement
    {
        public int start { get; set; }
        public int end { get; set; }
        public int direction { get; set; }

        public Movement(int s, int e, int d)
        {
            start = s;
            end = e;
            direction = d;
        }
    }
}
