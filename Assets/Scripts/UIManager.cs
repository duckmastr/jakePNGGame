using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    private Scrollbar energyBar;
    private PlayerController pc;
    private TextMeshProUGUI sprintValueTxt;

    private float sprintValue;

    // Start is called before the first frame update
    void Start()
    {
        energyBar = FindObjectOfType<Scrollbar>();
        pc = FindObjectOfType<PlayerController>();
        sprintValueTxt = FindObjectOfType<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        energyBar.size = pc.energy / 100;
        sprintValue = Mathf.Round(pc.energy * 1);
        sprintValueTxt.text = sprintValue.ToString();
    }
}
