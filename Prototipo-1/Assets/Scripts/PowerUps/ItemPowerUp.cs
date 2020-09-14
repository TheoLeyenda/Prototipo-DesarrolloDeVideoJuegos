using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public class DataItemPowerUp
    {
        public string powerUpName;
        public Sprite spriteItem;
        public Color trailColor;
    }
    public Player Target;
    public float speed = 20;
    [SerializeField] TrailRenderer trailRenderer;
    public Collider2D myCollider2D;
    public float distaceDisableMe = 0.5f;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] List<DataItemPowerUp> dataItemsPowerUps;
    [SerializeField] PowerUp powerUpInstance;
    [SerializeField] PowerUp.TypeCheckCollision typeCheckCollisionPowerUpItem;
    // Update is called once per frame
    void Start()
    {
        InitData();
    }
    void InitData()
    {
        int selectName = Random.Range(0, dataItemsPowerUps.Count);
        powerUpInstance.namePowerUp = dataItemsPowerUps[selectName].powerUpName;
        trailRenderer.startColor = dataItemsPowerUps[selectName].trailColor;
        //trailRenderer.endColor = dataItemsPowerUps[selectName].trailColor;
        spriteRenderer.sprite = dataItemsPowerUps[selectName].spriteItem;
    }
    void Update()
    {
        if (Target != null)
            GoToTarget();
    }
    private void OnEnable()
    {
        trailRenderer.Clear();
        trailRenderer.emitting = true;
        myCollider2D.enabled = true;
        InitData();
    }
    private void OnDisable()
    {
        trailRenderer.Clear();
        trailRenderer.emitting = false;
        myCollider2D.enabled = true;
    }
    public void GoToTarget()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, step);

        Vector3 distance = transform.position - Target.transform.position;
        if (distance.magnitude <= distaceDisableMe)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Target = collision.GetComponent<Player>();
            myCollider2D.enabled = false;
            powerUpInstance.typeCheckCollision = typeCheckCollisionPowerUpItem;
        }
    }
}
