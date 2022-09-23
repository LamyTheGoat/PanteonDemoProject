using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdatePercentage : MonoBehaviour
{
    [SerializeField]private TMP_Text tmptext;
    public void ChangePercentage()
    {
        tmptext.text = (Painting.Instance.getPercentage() * 100) + "%";
    }
}
