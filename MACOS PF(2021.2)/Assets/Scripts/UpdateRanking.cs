using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateRanking : MonoBehaviour
{
    [SerializeField]private TMP_Text tmptext;
    public void ChangeRanking()
    {
        tmptext.text = FindObjectOfType<GameManager>().Ranking + "/ 11";
    }
}
