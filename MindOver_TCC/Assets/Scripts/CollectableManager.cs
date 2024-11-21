using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableManager : MonoBehaviour
{
    public int colCount;
    public Text coinText;

    [System.Serializable]
    public class DoorRequirement
    {
        public GameObject door;
        public int collectableRequirement;
        public Animator doorAnim;
        public bool isOpen = false;
    }

    public List<DoorRequirement> doors;

    void Start()
    {
        coinText.text = colCount.ToString();
    }

    void Update()
    {
        coinText.text = colCount.ToString();

        foreach (DoorRequirement door in doors)
        {
            if (!door.isOpen && colCount <= door.collectableRequirement)
            {
                door.doorAnim.SetBool("open", true);
                door.isOpen = true;
            }
        }
    }
}
