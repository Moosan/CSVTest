using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Csvread : MonoBehaviour {
    [SerializeField]private TextAsset Book;
    private List<string[]> _datas;
    public GameObject Bou;
    private List<List<List<float[]>>> Data;
    private GameObject[][] Graphs;
    public int GyouCount;
    public int RetuCount;
    private float deltaTime;
    public float term;
    private float time;
	// Use this for initialization
	void Start () {
        Graphs = new GameObject[GyouCount][];
        for(int i = 0; i < GyouCount; i++)
        {
            Graphs[i] = new GameObject[RetuCount];
            for (int j = 0; j < RetuCount; j++)
            {
                Graphs[i][j] = Instantiate(Bou,new Vector3(i,0,j),new Quaternion());
            }
        }
        _datas = CsvSample.Set(Book);
        int DataCount = 1;
        Data = new List<List<List<float[]>>>();
        bool DateEnd=false;
        while (!DateEnd)
        {
            List<List<float[]>> OneTimeData = new List<List<float[]>>();
            for(int i = 0; i < GyouCount&&!DateEnd; i++)
            {
                List<float[]> OneColumnData = new List<float[]>();
                for(int j = 0; j < RetuCount&&!DateEnd; j++)
                {
                    string[] sdata = _datas[DataCount];
                    float[] fdata = new float[sdata.Length];
                    for(int k = 0; k < sdata.Length; k++)
                    {
                        fdata[k] = float.Parse(sdata[k]);
                    }
                    OneColumnData.Add(fdata);
                    DataCount++;
                    DateEnd = DataCount == _datas.Count;
                }
                OneTimeData.Add(OneColumnData);
            }
            Data.Add(OneTimeData);
        }
        dataTime = 0;
        End = false;
        deltaTime = 0.1f;
	}
    private int dataTime;
    private bool End;
	// Update is called once per frame
	void Update () {
        time += deltaTime;
        if (dataTime * term <= time&&!End)
        {
            for(int i = 0; i < GyouCount; i++)
            {
                for(int j = 0; j < RetuCount; j++)
                {
                    var obj = Graphs[i][j];
                    float height = Data[dataTime][i][j][1]+2;
                    var nowPos = obj.transform.position;
                    obj.transform.position = new Vector3(nowPos.x,height,nowPos.z);
                    obj.transform.localScale = new Vector3(1,height,1);
                }
            }
            dataTime++;
            End = (dataTime >= Data.Count - 1);
        }
        if (End)
        {
            time = 0;
            dataTime = 0;
            End = false;
        }
        Debug.Log(time);
	}
}
