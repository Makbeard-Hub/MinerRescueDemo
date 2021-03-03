using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    TextMeshPro textMesh;

    [SerializeField] float textFloatSpeed = 1f;
    [SerializeField] float lifeTime = 3f;

    float timeAlive = 0f;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;
        if(timeAlive > lifeTime)
        {
            Destroy(gameObject);
        }

        transform.rotation = Camera.main.transform.rotation;

        transform.Translate(Vector3.up * Time.deltaTime * textFloatSpeed);
    }

    public void Setup(string message)
    {
        textMesh.SetText(message.ToString());
    }
}