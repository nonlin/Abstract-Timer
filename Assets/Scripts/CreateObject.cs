/* Copyright (C) 2015 George Erfesoglou*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateObject : MonoBehaviour {

    public GameObject CubePiece;
    public Material CubeMat;
    public int maxX = 10;
    public int maxY = 10;
    private Vector3 CurrentPos = new Vector3(0, 0, 1);
    private Vector3 CubeSize = new Vector3();
    public float ScaleCubeBy = 1;
    //clock settings
    private List<Vector2> Positions = new List<Vector2>();
    private List<GameObject> Cubes = new List<GameObject>();
    private float distance = 5f;
    private string LastUpdatedTime = "";
    // Use this for initialization
    void Start () {

        for (int x = 0; x <= maxX; x++)
        {
            for (int y = 0; y <= maxY; y++)
            {
                CubeSize = Vector3.Scale(CubePiece.transform.localScale, new Vector3(x, y, ScaleCubeBy));
                CurrentPos = CubeSize;
                GameObject CurrentCube = Instantiate(CubePiece, CurrentPos, Quaternion.identity) as GameObject;
                CurrentCube.GetComponent<CubeLogic>().PosX = x;
                CurrentCube.GetComponent<CubeLogic>().PosY = y;
                Cubes.Add(CurrentCube);
            }
        }

        //Debug.Log(System.DateTime.Now.ToString("hh:mm:ss"));
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(System.DateTime.Now.ToString("hh:mm") != LastUpdatedTime)
        {
            LastUpdatedTime = System.DateTime.Now.ToString("hh:mm");
            ResetCubes();
            DisplayTime();
            //Debug.Log("Updating Time");
        }

	}

    void ResetCubes()
    {
        for (int i = 0; i < Cubes.Count; i++)
        {
            Cubes[i].GetComponent<Renderer>().material = CubeMat;
            Cubes[i].GetComponent<CubeLogic>().InstantContract();
            Cubes[i].GetComponent<CubeLogic>().isADigit = false;
        }
    }

    void DisplayTime()
    {
        string time = System.DateTime.Now.ToString("hh:mm");
        string[] TimeSplit = time.Split(':');

        string digit1 = TimeSplit[0].Substring(0, 1);
        string digit2 = TimeSplit[0].Substring(1, 1);
        string digit3 = TimeSplit[1].Substring(0, 1);
        string digit4 = TimeSplit[1].Substring(1, 1);

        DigitCheck(digit1, 1);
        DigitCheck(digit2, 3);
        DigitCheck(digit3, 6);
        DigitCheck(digit4, 8);

        MakeColon();
    }

    void DigitCheck(string DigitToCheck, float digitPlace)
    {
        switch (DigitToCheck)
        {
            case "0":
                MakeZero(digitPlace);
                break;
            case "1":
                MakeOne(digitPlace);
                break;
            case "2":
                MakeTwo(digitPlace);
                break;
            case "3":
                MakeThree(digitPlace);
                break;
            case "4":
                MakeFour(digitPlace);
                break;
            case "5":
                MakeFive(digitPlace);
                break;
            case "6":
                MakeSix(digitPlace);
                break;
            case "7":
                MakeSeven(digitPlace);
                break;
            case "8":
                MakeEight(digitPlace);
                break;
            case "9":
                MakeNine(digitPlace);
                break;

        }
    }

    void MakeColon()
    {
        //Create middle colon to seprate hours from minutes
        int ColonPosX = 25;
        for (int i = 0; i < Cubes.Count; i++)
        {
            CubeLogic CurrentCubeLogic = Cubes[i].GetComponent<CubeLogic>();

            if (CurrentCubeLogic.PosY == 8 && CurrentCubeLogic.PosX == ColonPosX || CurrentCubeLogic.PosY == 14 && CurrentCubeLogic.PosX == ColonPosX)
            {
                CurrentCubeLogic.InstantExtend();
                CurrentCubeLogic.isADigit = true;
                Cubes[i].GetComponent<Renderer>().material = CurrentCubeLogic.DigitCubeMat;
            }

        }
    }

    void MakeZero(float digitPos)
    {
        int startX = (int)(distance * digitPos);
        for (int i = 0; i < Cubes.Count; i++)
        {
            CubeLogic CurrentCubeLogic = Cubes[i].GetComponent<CubeLogic>();
            for (int bot = startX; bot < startX + 5; bot++)
            {
                if(CurrentCubeLogic.PosX == bot && CurrentCubeLogic.PosY == 5)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }

            for (int top = startX; top < startX + 5; top++)
            {
                if (CurrentCubeLogic.PosX == top && CurrentCubeLogic.PosY == 15)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }

            for (int left = 5; left < 15; left++)
            {
                if (CurrentCubeLogic.PosY == left && CurrentCubeLogic.PosX == startX)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }

            for (int right = 5; right < 16; right++)
            {
                if (CurrentCubeLogic.PosY == right && CurrentCubeLogic.PosX == startX + 5)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
        }
    }

    void MakeOne(float digitPos)
    {
        //Make a one
        int startX = (int)(distance * digitPos);
        for (int i = 0; i < Cubes.Count; i++)
        {
            CubeLogic CurrentCubeLogic = Cubes[i].GetComponent<CubeLogic>();
            //indent
            if (CurrentCubeLogic.PosX == startX - 1 && CurrentCubeLogic.PosY == 15)
            {
                //CurrentCubeLogic.InstantExtend();
                CurrentCubeLogic.ShouldExtend = true;
                Cubes[i].GetComponent<Renderer>().material.color = Color.black;
            }
            //long line
            for (int Y = 5; Y < 16; Y++)
            {
                if (CurrentCubeLogic.PosY == Y && CurrentCubeLogic.PosX == startX)
                {
                    //CurrentCubeLogic.InstantExtend();
                    CurrentCubeLogic.ShouldExtend = true;
                    Cubes[i].GetComponent<Renderer>().material.color = Color.black;
                }
            }
            //short line
            for (int x = startX - 1; x < startX + 2; x++)
            {
                if (CurrentCubeLogic.PosX == x && CurrentCubeLogic.PosY == 5)
                {
                    //CurrentCubeLogic.InstantExtend();
                    CurrentCubeLogic.ShouldExtend = true;
                    Cubes[i].GetComponent<Renderer>().material.color = Color.black;
                }
            }
        }
    }

    void MakeTwo(float digitPos)
    {
        int startX = (int)(distance * digitPos);
        for (int i = 0; i < Cubes.Count; i++)
        {
            CubeLogic CurrentCubeLogic = Cubes[i].GetComponent<CubeLogic>();
            //right
            for (int r = 0; r < 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + r && CurrentCubeLogic.PosY == 15)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //down
            for (int d = 0; d < 5; d++)
            {
                if (CurrentCubeLogic.PosX == startX + 5 && CurrentCubeLogic.PosY == 15 - d)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }

            //left
            for (int l = 0; l <= 5; l++)
            {
                if (CurrentCubeLogic.PosX == startX + l && CurrentCubeLogic.PosY == 10)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //down
            for (int d = 0; d <= 5; d++)
            {
                if (CurrentCubeLogic.PosX == startX && CurrentCubeLogic.PosY == 10 - d)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //right
            for (int d = 0; d <= 5; d++)
            {
                if (CurrentCubeLogic.PosX == startX + d && CurrentCubeLogic.PosY == 5)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
        }
    }

    void MakeThree(float digitPos)
    {
        int startX = (int)(distance * digitPos);
        for (int i = 0; i < Cubes.Count; i++)
        {
            CubeLogic CurrentCubeLogic = Cubes[i].GetComponent<CubeLogic>();
            //right
            for (int r = 0; r < 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + r && CurrentCubeLogic.PosY == 15)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            for (int r = 0; r < 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + 5 && CurrentCubeLogic.PosY == 15 - r)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            for (int l = 0; l <= 5; l++)
            {
                if (CurrentCubeLogic.PosX == startX + l && CurrentCubeLogic.PosY == 10)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            for (int r = 0; r < 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + 5 && CurrentCubeLogic.PosY == 10 - r)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            for (int l = 0; l <= 5; l++)
            {
                if (CurrentCubeLogic.PosX == startX + l && CurrentCubeLogic.PosY == 5)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
        }
    }

    void MakeFour(float digitPos)
    {
        int startX = (int)(distance * digitPos);
        for (int i = 0; i < Cubes.Count; i++)
        {
            CubeLogic CurrentCubeLogic = Cubes[i].GetComponent<CubeLogic>();

            for (int r = 0; r < 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + 5 && CurrentCubeLogic.PosY == 15 - r)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            for (int l = 0; l <= 5; l++)
            {
                if (CurrentCubeLogic.PosX == startX + l && CurrentCubeLogic.PosY == 10)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            for (int r = 0; r <= 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + 5 && CurrentCubeLogic.PosY == 10 - r)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            for (int l = 0; l <= 5; l++)
            {
                if (CurrentCubeLogic.PosX == startX && CurrentCubeLogic.PosY == 10 + l)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
        }
    }

    void MakeFive(float digitPos)
    {
        int startX = (int)(distance * digitPos);
        for (int i = 0; i < Cubes.Count; i++)
        {
            CubeLogic CurrentCubeLogic = Cubes[i].GetComponent<CubeLogic>();
            //right
            for (int r = 0; r <= 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + r && CurrentCubeLogic.PosY == 15)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //down
            for (int d = 0; d < 5; d++)
            {
                if (CurrentCubeLogic.PosX == startX && CurrentCubeLogic.PosY == 15 - d)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }

            //left
            for (int l = 0; l <= 5; l++)
            {
                if (CurrentCubeLogic.PosX == startX + l && CurrentCubeLogic.PosY == 10)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //down
            for (int d = 0; d <= 5; d++)
            {
                if (CurrentCubeLogic.PosX == startX + 5 && CurrentCubeLogic.PosY == 10 - d)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //right
            for (int d = 0; d <= 5; d++)
            {
                if (CurrentCubeLogic.PosX == startX + d && CurrentCubeLogic.PosY == 5)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
        }
    }

    void MakeSix(float digitPos)
    {
        int startX = (int)(distance * digitPos);
        for (int i = 0; i < Cubes.Count; i++)
        {
            CubeLogic CurrentCubeLogic = Cubes[i].GetComponent<CubeLogic>();
            //right
            for (int r = 0; r <= 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + r && CurrentCubeLogic.PosY == 15)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //down
            for (int d = 0; d < 5; d++)
            {
                if (CurrentCubeLogic.PosX == startX && CurrentCubeLogic.PosY == 15 - d)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }

            //left
            for (int l = 0; l <= 5; l++)
            {
                if (CurrentCubeLogic.PosX == startX + l && CurrentCubeLogic.PosY == 10)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //down
            for (int d = 0; d <= 5; d++)
            {
                if (CurrentCubeLogic.PosX == startX + 5 && CurrentCubeLogic.PosY == 10 - d)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //right
            for (int d = 0; d <= 5; d++)
            {
                if (CurrentCubeLogic.PosX == startX + d && CurrentCubeLogic.PosY == 5)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //up left
            for (int d = 0; d <= 5; d++)
            {
                if (CurrentCubeLogic.PosX == startX  && CurrentCubeLogic.PosY == 5 +d)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
        }
    }

    void MakeSeven(float digitPos)
    {
        int startX = (int)(distance * digitPos);
        for (int i = 0; i < Cubes.Count; i++)
        {
            CubeLogic CurrentCubeLogic = Cubes[i].GetComponent<CubeLogic>();
            //right
            for (int r = 0; r <= 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + r && CurrentCubeLogic.PosY == 15)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            for (int r = 0; r < 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + 5 && CurrentCubeLogic.PosY == 15 - r)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            for (int r = 0; r <= 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + 5 && CurrentCubeLogic.PosY == 10 - r)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
        }
    }

    void MakeEight(float digitPos)
    {
        int startX = (int)(distance * digitPos);
        for (int i = 0; i < Cubes.Count; i++)
        {
            CubeLogic CurrentCubeLogic = Cubes[i].GetComponent<CubeLogic>();
            for (int r = 0; r <= 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + r && CurrentCubeLogic.PosY == 15)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //Entire right side
            for (int r = 0; r < 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + 5 && CurrentCubeLogic.PosY == 15 - r)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            for (int r = 0; r <= 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + 5 && CurrentCubeLogic.PosY == 10 - r)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //up left
            for (int d = 0; d <= 5; d++)
            {
                if (CurrentCubeLogic.PosX == startX && CurrentCubeLogic.PosY == 5 + d)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            for (int d = 0; d < 5; d++)
            {
                if (CurrentCubeLogic.PosX == startX && CurrentCubeLogic.PosY == 15 - d)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //middle line
            for (int l = 0; l <= 5; l++)
            {
                if (CurrentCubeLogic.PosX == startX + l && CurrentCubeLogic.PosY == 10)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //right
            for (int d = 0; d <= 5; d++)
            {
                if (CurrentCubeLogic.PosX == startX + d && CurrentCubeLogic.PosY == 5)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
        }
    }

    void MakeNine(float digitPos)
    {

        int startX = (int)(distance * digitPos);
        for (int i = 0; i < Cubes.Count; i++)
        {
            CubeLogic CurrentCubeLogic = Cubes[i].GetComponent<CubeLogic>();
                        for (int r = 0; r <= 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + r && CurrentCubeLogic.PosY == 15)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //Entire right side
            for (int r = 0; r < 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + 5 && CurrentCubeLogic.PosY == 15 - r)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            for (int r = 0; r <= 5; r++)
            {
                if (CurrentCubeLogic.PosX == startX + 5 && CurrentCubeLogic.PosY == 10 - r)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            for (int d = 0; d < 5; d++)
            {
                if (CurrentCubeLogic.PosX == startX && CurrentCubeLogic.PosY == 15 - d)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //middle line
            for (int l = 0; l <= 5; l++)
            {
                if (CurrentCubeLogic.PosX == startX + l && CurrentCubeLogic.PosY == 10)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
            //right
            for (int d = 0; d <= 5; d++)
            {
                if (CurrentCubeLogic.PosX == startX + d && CurrentCubeLogic.PosY == 5)
                {
                    CurrentCubeLogic.ShouldExtend = true;
                }
            }
        }
    }

}
