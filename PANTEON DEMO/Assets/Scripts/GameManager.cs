using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform GameEndPosition;
    [SerializeField] private CinemachineBrain cameraBrain;
    [SerializeField] private List<GameObject> opponents;
    [SerializeField] private GameObject paintCanvas;
    private int totalOpponents = 11;

    private int _ranking = 0;
    public int Ranking
    {
        get => _ranking;
        private set
        {
            if (value != _ranking)
            {
                _ranking = value;
                rankingChanged.Invoke();
            }
        }
    }

    [SerializeField] private UnityEvent rankingChanged;
    public static GameManager Instance { get; private set; }
    // Start is called before the first frame update
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
        
        
        
    }

    private void Update()
    {
        CalculateRanking();
    }

    private void Start()
    {
        cameraBrain = FindObjectOfType<CinemachineBrain>();
    }

    public void RestartRunner(GameObject runner)
    {
        runner.transform.position = startPoint.position;
    }

    public void GameEnd()
    {
        paintCanvas.SetActive(true);
        Painting.Instance.gameObject.SetActive(true);
        Painting.Instance.gameObject.SetActive(true);
        FindObjectOfType<CinemachineStateDrivenCamera>().gameObject.GetComponent<Animator>().Play("PaintCamera");
        GameObject player = FindObjectOfType<PlayerMovement>().gameObject;
        player.GetComponent<MovingObject>().enabled = false;
        player.transform.position = GameEndPosition.position;
        player.transform.rotation = GameEndPosition.rotation;
    }

    private void CalculateRanking()
    {
        List<GameObject> rankedList = new List<GameObject>(opponents.OrderBy(p => p.transform.position.x));
        for (int i = 0; i < rankedList.Count(); i++)
        {
            if (rankedList[i].name == "Player")
            {
                
                int virtualranking = rankedList.Count - i;
                Ranking = virtualranking;
                break;
            }

        }
    }
}
