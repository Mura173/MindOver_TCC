using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollectableManager : MonoBehaviour
{
    public int colCount;
    public Text coinText;
    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        colCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = colCount.ToString();

        if(colCount <= 0)
        {
            Destroy(door);
        }
    }
}
