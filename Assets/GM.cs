using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{

    public int x;
    public int y;

    private int xLimit;

    private int yLimit;

    public GameObject myPrefab;
    public GameObject[,] allPoints;
    public Camera mainCam;
    [SerializeField]
    private Slider FpsSlider;
    public bool isStarted = false;

    int[,] allPointsBlackResult;
    public bool done = false;
    float fps = 1;
    float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        mainCam.transform.position = new Vector3(x/2, y/2, -1);
        mainCam.orthographicSize = x / 2;
        xLimit = x - 1;
        yLimit = y - 1;
        allPoints = new GameObject[x, y];
        allPointsBlackResult = new int[x, y];

        for (int i = 0; i < x; i++)
        {
            for (int o = 0; o < y; o++)
            {
                allPointsBlackResult[i, o] = 0;
                GameObject newBox = (GameObject)Instantiate(myPrefab, new Vector3(i, o, 0), Quaternion.identity);
                allPoints[i, o] = newBox;
                newBox.name = i + "," + o;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted) {
            currentTime += Time.deltaTime;
            if (currentTime >= 1 / fps)
            {
                checkPoints();
                oneIteration();
                currentTime = 0;
            }
        }


    }
    public void setFps()
    {
        fps = FpsSlider.value;
    }
    public void StartGame()
    {
        isStarted = true;
    }
    int CheckNeighboursCount(int xPos, int yPos)
    {
        int result = 0;
        if (xPos == 0)
        {

            if (yPos == 0)
            {
                if (checkPointIsBlack(allPoints[xPos + 1, yPos])) result++;
                if (checkPointIsBlack(allPoints[xPos + 1, yPos + 1])) result++;
                if (checkPointIsBlack(allPoints[xPos, yPos + 1])) result++;
            }
            else if (yPos == yLimit)
            {
                if (checkPointIsBlack(allPoints[xPos + 1, yPos])) result++;
                if (checkPointIsBlack(allPoints[xPos + 1, yPos - 1])) result++;
                if (checkPointIsBlack(allPoints[xPos, yPos - 1])) result++;
            }
            else
            {
                if (xPos < xLimit && yPos > 0 && yPos < yLimit)
                {
                    if (checkPointIsBlack(allPoints[xPos + 1, yPos])) result++;
                    if (checkPointIsBlack(allPoints[xPos + 1, yPos + 1])) result++;
                    if (checkPointIsBlack(allPoints[xPos, yPos + 1])) result++;
                    if (checkPointIsBlack(allPoints[xPos + 1, yPos - 1])) result++;
                    if (checkPointIsBlack(allPoints[xPos, yPos - 1])) result++;
                }
            }

        }
        else if (xPos == xLimit)
        {
            if (yPos == 0)
            {
                if (checkPointIsBlack(allPoints[xPos - 1, yPos])) result++;
                if (checkPointIsBlack(allPoints[xPos - 1, yPos + 1])) result++;
                if (checkPointIsBlack(allPoints[xPos, yPos + 1])) result++;
            }
            else if (yPos == yLimit)
            {
                if (checkPointIsBlack(allPoints[xPos - 1, yPos])) result++;
                if (checkPointIsBlack(allPoints[xPos - 1, yPos - 1])) result++;
                if (checkPointIsBlack(allPoints[xPos, yPos - 1])) result++;
            }
            else
            {
                if (checkPointIsBlack(allPoints[xPos - 1, yPos])) result++;
                if (checkPointIsBlack(allPoints[xPos - 1, yPos + 1])) result++;
                if (checkPointIsBlack(allPoints[xPos, yPos + 1])) result++;
                if (checkPointIsBlack(allPoints[xPos - 1, yPos - 1])) result++;
                if (checkPointIsBlack(allPoints[xPos, yPos - 1])) result++;
            }
        }
        else if (yPos == 0)
        {
            if (checkPointIsBlack(allPoints[xPos + 1, yPos])) result++;
            if (checkPointIsBlack(allPoints[xPos - 1, yPos])) result++;
            if (checkPointIsBlack(allPoints[xPos + 1, yPos + 1])) result++;
            if (checkPointIsBlack(allPoints[xPos, yPos + 1])) result++;
            if (checkPointIsBlack(allPoints[xPos - 1, yPos + 1])) result++;
        }
        else if (yPos == yLimit)
        {
            if (checkPointIsBlack(allPoints[xPos + 1, yPos])) result++;
            if (checkPointIsBlack(allPoints[xPos - 1, yPos])) result++;
            if (checkPointIsBlack(allPoints[xPos + 1, yPos - 1])) result++;
            if (checkPointIsBlack(allPoints[xPos, yPos - 1])) result++;
            if (checkPointIsBlack(allPoints[xPos - 1, yPos - 1])) result++;
        }
        else
        {
            if (xPos > 0 && xPos < xLimit && yPos > 0 && yPos < yLimit)
            {
                if (checkPointIsBlack(allPoints[xPos + 1, yPos])) result++;
                if (checkPointIsBlack(allPoints[xPos - 1, yPos])) result++;
                if (checkPointIsBlack(allPoints[xPos + 1, yPos + 1])) result++;
                if (checkPointIsBlack(allPoints[xPos, yPos + 1])) result++;
                if (checkPointIsBlack(allPoints[xPos - 1, yPos + 1])) result++;
                if (checkPointIsBlack(allPoints[xPos + 1, yPos - 1])) result++;
                if (checkPointIsBlack(allPoints[xPos, yPos - 1])) result++;
                if (checkPointIsBlack(allPoints[xPos - 1, yPos - 1])) result++;
            }

        }
        return result;
    }

    bool checkPointIsBlack(GameObject GridPoint)
    {
        bool isReallyBlack = false;

        if (!GridPoint.GetComponent<Click>().getIsWhite())
        {
            isReallyBlack = true;
        }
        return isReallyBlack;
    }

    void checkPoints()
    {
        for (int i = 0; i < x; i++)
        {
            for (int o = 0; o < y; o++)
            {
                allPointsBlackResult[i, o] = CheckNeighboursCount(i, o);
            }

        }
    }
    void oneIteration()
    {
        for (int i = 0; i < x; i++)
        {
            for (int o = 0; o < y; o++)
            {
                if (allPointsBlackResult[i, o] <= 1)
                {
                        allPoints[i, o].GetComponent<Click>().setWhite();
                }
                else if (allPointsBlackResult[i, o] == 2)
                {
                    if (allPoints[i, o].GetComponent<Click>().getIsBlack())
                    {
                        allPoints[i, o].GetComponent<Click>().setBlack();
                    }

                }
                else if (allPointsBlackResult[i, o] == 3)
                {
                        allPoints[i, o].GetComponent<Click>().setBlack();
                }
                else if (allPointsBlackResult[i, o] >= 4)
                {
                        allPoints[i, o].GetComponent<Click>().setWhite();

                }

            }
        }
    }
}
