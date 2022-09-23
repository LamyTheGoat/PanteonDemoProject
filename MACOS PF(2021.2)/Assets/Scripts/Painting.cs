using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Painting : MonoBehaviour
{
    public Color paintColor = Color.red;
    public float brushSize = 0.1f;
    public Transform paintpanelsParent;
    
    [SerializeField] private GameObject brushIndicator;
    [SerializeField] private GameObject paintPrefab;
    [SerializeField] private int current = 0;
    [SerializeField] private UnityEvent percentageUpdate;

    private Dictionary<string, Color> colorDict = new Dictionary<string, Color>()
    {
        {"Red", Color.red},
        {"Blue", Color.blue},
        {"Yellow", Color.yellow}
    };




    private Camera camera;
    private List<Transform> paintPanelsList;
    private int total = 0;

    public static Painting Instance { get; private set; }
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
        gameObject.SetActive(false);
        
    }

    private void OnEnable()
    {
        paintPanelsList = new List<Transform>(paintpanelsParent.GetComponentsInChildren<Transform>().Where(child => child.gameObject != this.gameObject));
        total = paintPanelsList.Count;
        current = total;
        camera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Paintable")
            {
                brushIndicator.transform.position = hit.point;
                brushIndicator.transform.localScale = new  Vector3(0.03f, brushSize, brushSize);
            }
        }

        if (Input.GetButton("Fire1"))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Paintable")
                {
                    
                    
                    
                    GameObject paintobj = Instantiate(paintPrefab);
                    paintobj.transform.position = hit.point;
                    paintobj.transform.localScale = new  Vector3(0.03f, brushSize, brushSize);
                    paintobj.GetComponent<Renderer>().material.color = paintColor;

                    paintPanelsList.Remove(hit.transform);
                    current = paintPanelsList.Count;
                    percentageUpdate.Invoke();
                    
                    if(hit.transform.gameObject.name!="IndestructibleWall")
                        Destroy(hit.transform.gameObject);
                }
            }
        }
    }

    public float getPercentage()
    {
        return 1f - ((current * 1f) / (total* 1f));
    }

    public void ChangeColor(string colorName)
    {
        this.paintColor = colorDict[colorName];
    }

    public void ChangeBrushSize(float value)
    {
        Instance.brushSize = value;
    }
}
