using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] List<string> lines;//×Ö·û´®ÁÐ±í
    public List<string> Lines
    {
        get { return lines; }
    }
}
