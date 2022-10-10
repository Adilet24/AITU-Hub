using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private GameObject player;
    private PlayerInputActions playerInputActions;
    private bool playerInRange;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Player.Interact.performed += EnterDialogueMode;

        playerInRange = false;
        visualCue.SetActive(false);
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
        }
        else
        {
            visualCue?.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    private void EnterDialogueMode(InputAction.CallbackContext context)
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying && context.performed)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            DialogueManager.GetInstance().EnterDialogue(inkJSON);
        }
    }
}