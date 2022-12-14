using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class NPCscript : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    //public GameObject dialogueClose;
    public string[] dialogue;
    private int index;

    public GameObject contButton;
    public float wordSpeed;
    public bool playerIsClose;
    bool talking, walking = false;
    public Animator animator;
    public AudioClip ClipTalking;
    public AudioSource audioSource;
    public GameObject interact;
    
    

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("talking", false);
        audioSource = GetComponent<AudioSource>();
        //audioSource.PlayOneShot(ClipTalking, 1.0f);
    }
   
    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if(dialoguePanel.activeInHierarchy)
            {
                zeroText();
                animator.SetBool("talking", false);
                //interact.SetActive(false); 
            }
            else
            {
                interact.SetActive(false);
                audioSource.PlayOneShot(ClipTalking,1.0f);
                animator.SetBool("talking", true);
                dialoguePanel.SetActive(true);
                dialogueText.text = "";
                StartCoroutine(Typing());
                Debug.Log("she working");
                
            }

        }

        if (dialogueText.text == dialogue[index]) 
        {
            contButton.SetActive(true);   
        }

    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }
    
    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);
        

        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {

            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entered box");
            interact.SetActive(true);

            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (!other.CompareTag("Player"))
        {
            //interact.SetActive(false);
            animator.SetBool("talking", false);
            dialoguePanel.SetActive(false);
            zeroText();
            interact.SetActive(false);
            
        }
    }

}
