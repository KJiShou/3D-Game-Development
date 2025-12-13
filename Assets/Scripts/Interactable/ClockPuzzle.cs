using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ClockPuzzle : MonoBehaviour
{
    public static ClockPuzzle instance;

    public bool[] triggerBools;
    public int[] triggerNums;
    public int currentInx = 0;
    public int errorCount = 0;

    public GameObject purpleFlower;
    private GameObject[] purpleFlowers;

    public GameObject redFlower;
    private GameObject[] redFlowers;

    public GameObject yellowFlower;
    private GameObject[] yellowFlowers;

    public GameObject portal;

    private ClockPuzzlePressurePlate[] plates;

    public TextMeshProUGUI hint;
    private List<string> hintText = new List<string>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        hintText.Add("The statues seem to follow a certain order.");
        hintText.Add("The statues' arrangement and the flowers' numbers seem to point to two different clues.");
        hintText.Add("Statues guide the sequence. Flowers mark the number.");

        hint.text = hintText[errorCount];

        triggerBools = new bool[3] { false, false, false };
        triggerNums = new int[3];
        plates = GetComponentsInChildren<ClockPuzzlePressurePlate>();

        List<int> pool = new List<int>();

        for (int i = 1; i <= 12; i++)
            pool.Add(i);

        triggerNums[0] = GetAndRemove(pool);
        triggerNums[1] = GetAndRemove(pool);
        triggerNums[2] = GetAndRemove(pool);

        purpleFlowers = new GameObject[purpleFlower.transform.childCount];

        for (int i = 0; i < purpleFlower.transform.childCount; i++)
        {
            purpleFlowers[i] = purpleFlower.transform.GetChild(i).gameObject;
        }

        redFlowers = new GameObject[redFlower.transform.childCount];

        for (int i = 0; i < redFlower.transform.childCount; i++)
        {
            redFlowers[i] = redFlower.transform.GetChild(i).gameObject;
        }

        yellowFlowers = new GameObject[yellowFlower.transform.childCount];

        for (int i = 0; i < yellowFlower.transform.childCount; i++)
        {
            yellowFlowers[i] = yellowFlower.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < triggerNums[0]; i++)
        {
            redFlowers[i].SetActive(true);
        }

        for (int i = 0; i < triggerNums[1]; i++)
        {
            purpleFlowers[i].SetActive(true);
        }

        for (int i = 0; i < triggerNums[2]; i++)
        {
            yellowFlowers[i].SetActive(true);
        }
    }

    private void Update()
    {
        if(currentInx >= 2)
        {
            if (triggerBools[0] &&  triggerBools[1] && triggerBools[2])
            {
                portal.SetActive(true);
            }
        }
    }

    private int GetAndRemove(List<int> list)
    {
        int index = Random.Range(0, list.Count);
        int value = list[index];
        list.RemoveAt(index);
        return value;
    }

    private bool OpenPortal()
    {
        return triggerBools[0] && triggerBools[1] && triggerBools[2];
    }

    public void CompareTriggerNumber(int pressurePlateInx)
    {
        if (currentInx >= triggerNums.Length)
            return;
        triggerBools[currentInx] = (triggerNums[currentInx] == pressurePlateInx + 1);
        currentInx++;

        if (currentInx == triggerNums.Length)
        {
            ResolvePuzzle();
        }
    }

    public void ResolvePuzzle()
    {
        bool success = OpenPortal();

        if (!success && errorCount < 2)
        {
            errorCount++;
            hint.text = hintText[errorCount];
        }

        foreach (var plate in plates)
        {
            if (success)
            {
                plate.OnPuzzleSuccess();
            }
            else
            {
                plate.OnPuzzleFail();
                StartCoroutine(plate.ResetTriggered());
            }
        }

        if (success)
        {
            portal.SetActive(true);
        }
        else
        {
            currentInx = 0;
        }
    }

}
