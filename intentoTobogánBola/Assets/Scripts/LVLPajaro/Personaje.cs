﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    public float velMovimiento  =5.0f;
    public float velRotacion = 200.0f;
    public Animator anim;
    public float x, y;

    public Rigidbody rb;
    public float jumpHeight=8f;
    public bool isGrounded;

    private float inicioDisparo;
    public float tiempoDisparo;
    public float VelDisparo;
    public Transform lanzador;
    public Rigidbody flechaPrefab;
    void Start() {
        isGrounded=false;
    }

    void FixedUpdate() {
            transform.Rotate(0,x*Time.deltaTime*velRotacion,0);
            transform.Translate(0,0,y*Time.deltaTime*velMovimiento);

    }

    void Update()
    {
        x= Input.GetAxis("Horizontal");
        y= Input.GetAxis("Vertical");
        
        if(Input.GetMouseButtonDown(0) && isGrounded && Time.time > inicioDisparo){
            anim.SetTrigger("Golpe");
            inicioDisparo = Time.time + tiempoDisparo;
            Rigidbody flechaPrefabInstanc;

            flechaPrefabInstanc = Instantiate(flechaPrefab, lanzador.position, Quaternion.identity) as Rigidbody;
            flechaPrefabInstanc.AddForce(lanzador.forward*100*VelDisparo);
        }

        anim.SetFloat("VelX",x);
        anim.SetFloat("VelY",y);



        if (isGrounded){
                if(Input.GetKeyDown(KeyCode.Space)){
                    anim.SetBool("Jump",true);
                    rb.AddForce(new Vector3(0,jumpHeight,0),ForceMode.Impulse);
                }
            anim.SetBool("Grounding",true);
        }else{
            Falling();
        }
    }
    
    public void Falling(){
        anim.SetBool("Jump",false);
        anim.SetBool("Grounding",false);
    }

}
