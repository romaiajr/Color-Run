﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public float speed;
    public float jumpHeight;
    public float gravity;
    public float horizontalSpeed;
    private float jumpVelocity;
    private bool isMovingR = false;
    private bool isMovingL = false;
    public bool isColliding = false;

    void Start()
    {
        controller = GetComponent<CharacterController> ();
    }

    // Update is called once per frame
    void Update()
    {
        //foward é o eixo Z para frente
        Vector3 direction = Vector3.forward * speed;
        if(controller.isGrounded){
            if(Input.GetKeyDown(KeyCode.Space)){
                jumpVelocity = jumpHeight; // faz ele pular
            }
            if(Input.GetKeyDown(KeyCode.RightArrow) && !isMovingR){
                isMovingR = true;
                StartCoroutine(RightMove()); //move para a direita
                if(isColliding)
                    StopCoroutine(RightMove());
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow) && !isMovingL){
                isMovingL = true;
                StartCoroutine(LeftMove()); //move para a esquerda
                if(isColliding)
                    StopCoroutine(LeftMove());
            }
        }
        else{
            jumpVelocity -= gravity; //faz ele cair
        }
        speed += speed*(Time.deltaTime/50); //incrementa velocidade com o tempo
        direction.y= jumpVelocity; //pular
        controller.Move(direction*Time.deltaTime); //mover
    }

    //método que é executado várias vezes
    IEnumerator LeftMove(){
        for(float i = 0; i<10; i += 0.1f){
            controller.Move(Vector3.left * Time.deltaTime * horizontalSpeed);
            yield return null;
        }
        isMovingL = false;
    }
    IEnumerator RightMove(){
        for(float i = 0; i<6; i += 0.1f){
            controller.Move(Vector3.right * Time.deltaTime * horizontalSpeed);
            yield return null;
        }
        isMovingR = false;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Wall")){
            isColliding = true;
        }
    }
}
