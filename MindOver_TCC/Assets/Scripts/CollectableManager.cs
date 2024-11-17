using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollectableManager : MonoBehaviour
{
    public int colCount;
    public Text coinText;

    [System.Serializable]
    public class DoorRequirement
    {
        public GameObject door;
        public int collectableRequirement;
    }

    public List<DoorRequirement> doors;

    private int currentDoorIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = colCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = colCount.ToString();

        if (currentDoorIndex < doors.Count &&
            colCount <= doors[currentDoorIndex].collectableRequirement)
        {
            DestroyDoor();
        }
    }

    private void DestroyDoor()
    {
        Destroy(doors[currentDoorIndex].door);

        currentDoorIndex++;
    }
}
