using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class astroidController : MonoBehaviour
{
    public Vector2 minMaxVSpeed, minMaxHSpeed;
    public float rotation;
    private Vector2 screenBounds;
    public GameObject UIWarnPrefab;
    private GameObject uiWarn;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        Vector2 force = new Vector2(
            Random.Range(minMaxHSpeed.x, minMaxHSpeed.y) * ((screenBounds.x - Camera.main.orthographicSize * Camera.main.aspect < transform.position.x) ? -1 : 1),
            Random.Range(minMaxVSpeed.x, minMaxVSpeed.y) * -1);

        rb.AddForce(force);
        rb.AddTorque(rotation * (Random.Range(0, 1) == 0 ? -1 : 1));

        uiWarn = Instantiate(UIWarnPrefab, new Vector3(0, screenBounds.y - 1, 1), Quaternion.identity);
        var UIWarnVars = uiWarn.GetComponent<Follower>();
        UIWarnVars.followX = true;
        UIWarnVars.toFollow = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < screenBounds.y - Camera.main.orthographicSize * 2)
        {
            Destroy(gameObject);
            Destroy(uiWarn);
        }

        uiWarn.SetActive((transform.position.y > screenBounds.y));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Controller con = col.GetComponent<Controller>();
        if (con != null)
            con.destroyShip();
    }
}
