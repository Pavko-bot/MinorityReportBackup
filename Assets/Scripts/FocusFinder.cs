using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusFinder : MonoBehaviour
{
    public GameObject currentFocus;
    public GameObject FocusUIPrefab;
    private GameObject currentFocusUI;
    public GameObject cam;

    void Start()
    {
        currentFocusUI = Instantiate(FocusUIPrefab);
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(cam.transform.position, Vector2.zero);
        if (hit.collider != null && hit.transform.tag == "VideoPlayer" && currentFocusUI != hit.transform.gameObject)
        {
            currentFocus = hit.transform.gameObject;
            currentFocusUI.transform.SetParent(hit.transform);
            currentFocusUI.transform.position = hit.transform.position;
        }
    }
}
