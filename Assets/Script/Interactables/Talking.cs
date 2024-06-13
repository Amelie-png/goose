using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(SpriteRenderer))]
public class Talking : Interactable {

    public DialogueTrigger dialogueTrigger;

    public override void Interact() {
        dialogueTrigger.TriggerDialogue();
    } // Interact
} // Talking