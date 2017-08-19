using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GPVReader : MonoBehaviour {
    private List<List<GPVdata>> Datas;

    [SerializeField]private int year;
    [SerializeField]private int month;
    [SerializeField]private int day;
    [SerializeField]private int hour;
    [SerializeField]private int minute;
    [SerializeField]private int second;
    [SerializeField]private int time;
    private static string dataPath;

    public void Start()
    {
        Datas = new List<List<GPVdata>>();
        while (true) {
            string path = DataPathGetter();
            List<GPVdata> datas = OneTimeDataGetter(path);
            if (datas == null) {
                Debug.Log(path+"はありません。");
                break;
            }
            Datas.Add(datas);
            TimeMove();
        }
    }



    private void TimeMove() {
        time++;
        if (time == 2) {
            time = 0;
            minute += 10;
            if (minute == 60) {
                minute = 0;
                hour++;
                if (hour == 24) {
                    hour = 0;
                    day++;
                    if (day == 32) {
                        day = 0;
                    }
                    if (day == 31) {
                        if (month == 4 || month == 6 || month == 9 || month == 11) {
                            day = 0;
                        }
                    }
                    if (day == 30&&month==2) {
                        day = 0;
                    }
                    if (day == 29 && month == 2) {
                        day = 0;
                        if (year % 4 == 0) {
                            if (year % 100 == 0)
                            {
                                if (year % 400 == 0) {
                                    day = 29;
                                }
                            }
                            else {
                                day = 29;
                            }
                        }
                    }
                    if (day == 0) {
                        month++;
                        if (month == 13) {
                            month = 0;
                            year++;
                        }
                    }
                }
            }
        }
    }


    private List<GPVdata> OneTimeDataGetter(string path) {
        if (!File.Exists(path)) {
            return null;
        }
        List<GPVdata> dataList = new List<GPVdata>();
        StreamReader sr = new StreamReader(path);
        if (sr == null) {
            return null;
        }
        string strStream = sr.ReadToEnd();
        List<string[]> originData=CsvSample.Set(strStream);
        for (int i=14;i<originData.Count;i++) {
            GPVdata data = new GPVdata(originData[i]);
            dataList.Add(data);
        }
        return dataList;
    }

    private string DataPathGetter() {
        string moStr=ToStr(month);
        string daStr = ToStr(day);
        string hoStr = ToStr(hour);
        string miStr = ToStr(minute);
        string seStr = ToStr(second);
        string s = "/";
        string filename = year + moStr + daStr + "_" + hoStr + miStr + seStr + ".00" + time+".csv";
        string textPath = Application.dataPath+"/../../"+"CsvDatas"+s+"data"+s+year+s+moStr+s+daStr+s+filename;
        return textPath;
    }

    private string ToStr(int n) {
        string str;
        if (n >= 10)
        {
            str = n.ToString();
        }
        else
        {
            str = "0" + n;
        }
        return str;
    }
}
