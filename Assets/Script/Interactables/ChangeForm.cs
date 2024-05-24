using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ChangeForm : Interactable
{
    public Sprite cool;
    public Sprite notCool;
    private SpriteRenderer sr;
    private bool isCool;

    public override void Interact(){
        if(isCool){
            sr.sprite = notCool;
        }
        else{
            sr.sprite = cool;
        }

        isCool = !isCool;
    }
    private void Start(){
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = cool;
        isCool = true;
    }
}
