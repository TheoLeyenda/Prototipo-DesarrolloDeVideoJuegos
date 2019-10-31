using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class PoolObject : MonoBehaviour {

    // Use this for initialization
    public Pool pool;
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Recycle()
    {
        pool.Recycle(this.gameObject);
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
