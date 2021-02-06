using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPowerUpController : MonoBehaviour
{
    public Enemy userEnemy;
    public Pool poolItemPowerUp;
    public float porcentageDropPowerUp;
    public float ModifyVertical = -0.04f;
    public float ModifyHorizontal = 0.014f;
    private Vector3 ModifyVector;

    void Start()
    {
        ModifyVector = new Vector3(ModifyHorizontal, ModifyVertical, 0);
    }
    private void OnEnable()
    {
        Enemy.OnDie += CheckDropPowerUp;
    }
    private void OnDisable()
    {
        Enemy.OnDie -= CheckDropPowerUp;
    }
    public void CheckDropPowerUp(Enemy e)
    {
        if (e != userEnemy) return;

        float porcentageResult = Random.Range(0, 100);

        if (porcentageResult <= porcentageDropPowerUp)
        {
            GameObject go = poolItemPowerUp.GetObject();
            go.transform.position = transform.position + ModifyVector;
        }
    }
}
