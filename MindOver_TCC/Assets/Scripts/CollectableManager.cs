using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableManager : MonoBehaviour
{
    public int colCount;
    public Text coinText;

    private GameObject audioSourceObject;
    private AudioSource audioSource;

    public AudioClip openClip;

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
        audioSourceObject = GameObject.Find("AudioManager");
        audioSource = audioSourceObject.GetComponent<AudioSource>();
        coinText.text = colCount.ToString();
    }

    void Update()
    {
        coinText.text = colCount.ToString();

        foreach (DoorRequirement door in doors)
        {
            if (!door.isOpen && colCount <= door.collectableRequirement)
            {
                audioSource.PlayOneShot(openClip);
                door.doorAnim.SetBool("open", true);
                door.isOpen = true;
            }
        }
    }
}
