using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolObject : MonoBehaviour {

    public Pool pool;
	void Start () {
	}
	
	void Update () {
		
	}
    public void Recycle()
    {
        pool.Recycle(this.gameObject);
    }
}
