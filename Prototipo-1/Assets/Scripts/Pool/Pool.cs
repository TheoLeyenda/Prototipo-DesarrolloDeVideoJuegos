using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour {

    public GameObject Ball;
    private GameObject parent;
    public string parentName;
    private List<GameObject> CommonBalls;
    public int count;
    private int id;
    private bool substractValuesBalls;
    public bool GeneratePoolOnEnable;
    // Use this for initialization
    void Awake()
    {
        
    }
    private void OnEnable()
    {
        if (GeneratePoolOnEnable)
        {
            GeneratePool();
        }
    }
    private void Start()
    {
        FindParent(parentName);
        GeneratePool();
    }
    public void CheckParent(GameObject go)
    {
        if (parent != null)
        {
            go.transform.SetParent(parent.transform);
        }
    }
    public void FindParent(string parentName)
    {
        parent = GameObject.Find(parentName);
    }
    public void GeneratePool()
    {
        CommonBalls = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(Ball);
            CheckParent(go);
            PoolObject po;
            go.SetActive(false);
            CommonBalls.Add(go);
            po = go.AddComponent<PoolObject>();
            po.pool = this;
        }
        id = 0;
    }
    // Update is called once per frame
    void Update () {
    }
    public List<GameObject> GetListPelotasComunes()
    {
        return CommonBalls;
    }
    public GameObject GetObject()
    {
        if (CommonBalls != null)
        {
            if (id < CommonBalls.Count)
            {
                GameObject go = CommonBalls[id];
                go.SetActive(true);
                id++;
                return go;
            }
            else
            {
                GameObject go = null;
                id = 0;
                if (id >= 0)
                {
                    go = CommonBalls[id];
                    go.SetActive(true);
                }

                id++;
                return go;
            }
        }
        return null;
        
    }
    public void Recycle(GameObject go)
    {
        if (id > 0)
        {
            id--;
            go.SetActive(false);
            CommonBalls[id] = go;
        }
    }
    public void SetId(int _id)
    {
        id = _id;
    }
    public void SubstractId()
    {
        id = id - 1;
    }
    public void AddId()
    {
        id = id + 1;
    }
    public int GetId()
    {
        return id;
    }
}