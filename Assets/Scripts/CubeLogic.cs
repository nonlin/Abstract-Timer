/* Copyright (C) 2015 George Erfesoglou*/
using UnityEngine;
using System.Collections;

public class CubeLogic : MonoBehaviour {

    public Material CubeMat;
    public Material DigitCubeMat;
    public int PosX;
    public int PosY;
    private bool MouseExited = false;
    private float TimePassedBegin = 0;
    private float TimePassedEnd = 0;
    private float length = 3;
    public float growthRate = 0.02f;
    public float R, G, B;
    public bool RandomCubeColors = false;
    public float CubeScale = 1;
    public bool ShouldExtend = false;
    public bool ShouldContract = false;
    public bool isADigit = false;
    public float TimePassed = 0f;
	// Use this for initialization
	void Start ()
    {
        CubeScale = GameObject.Find("InitObject").GetComponent<CreateObject>().ScaleCubeBy;
        if (RandomCubeColors)
        {
            R = Random.Range(0.5f, 1.0f);
            G = Random.Range(0.3f, 1.0f);
            B = Random.Range(0.6f, 1.0f);
            GetComponent<Renderer>().material.color = new Color(R, G, B);
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        TimePassed += Time.deltaTime;
        if (MouseExited)
        {
            Contract();
        }

        if (ShouldExtend)
        {
            Extend();
            isADigit = true;
            this.GetComponent<Renderer>().material = DigitCubeMat;
        }

        if (ShouldContract)
        {
            Contract();
        }

        if (!isADigit && System.DateTime.Now.Second % 5 == 0)
        {
  
            if (Random.Range(0.0f, 1.0f) > 0.6f)
            {
                InstantExtend();
            }
            else
            {
                InstantContract();
            }
        }
    }

    void FixedUpdate()
    {

    }

    void OnMouseOver()
    {
        Extend();
        this.GetComponent<Renderer>().material = DigitCubeMat;
    }

    void OnMouseExit()
    {
        MouseExited = true;
        if (!isADigit)
            this.GetComponent<Renderer>().material = CubeMat;
    }

    public void Extend()
    {
        if (TimePassedBegin <= length)
        {
            TimePassedBegin += growthRate;
            this.transform.localScale += new Vector3(0, 0, growthRate);
            //Debug.Log(this.transform.localScale);
        }
        else
        {
            //Max Extend
            ShouldExtend = false;
            this.transform.localScale = new Vector3(CubeScale, CubeScale, length);
        }
    }

    public void InstantExtend()
    {
        this.transform.localScale = new Vector3(CubeScale, CubeScale, length);

    }

    public void Contract()
    {
        if (TimePassedEnd <= gameObject.transform.localScale.z)
        {
            TimePassedEnd += growthRate;
            this.transform.localScale -= new Vector3(0, 0, growthRate);
            //Debug.Log(TimePassedEnd);
        }
        else
        {
            MouseExited = false;
            TimePassedBegin = 0;
            TimePassedEnd = 0;
            ShouldContract = false;
            //Max Contract
            this.transform.localScale = new Vector3(CubeScale, CubeScale, CubeScale);
            //Debug.Log("Shrink End");
        }
    }

    public void InstantContract()
    {
        this.transform.localScale = new Vector3(CubeScale, CubeScale, CubeScale);
    }
}
