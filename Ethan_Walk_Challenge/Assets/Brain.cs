using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class Brain : MonoBehaviour
{
    public int DNALength = 1;
    public float timeAlive;
    public DNA dna;
    private ThirdPersonCharacter m_Character;
    Vector3 startPosition;
    private Vector3 m_move;
    private bool m_Jump;
    bool alive = true;
    public float distanceTravelled;

    void OnCollisionEnter(Collision Obj)
    {
        if(Obj.gameObject.tag == "dead")
        {
            alive = false;
        }
    }
    public void Init()
    {
        //Initialize dna
        //0 forward, 1 back, 2 left, 3 right, 4 jump, 5 crouch
        dna = new DNA(DNALength,6);
        m_Character = GetComponent<ThirdPersonCharacter>();
        timeAlive = 0;
        alive = true;
        startPosition = this.transform.position;
    }

    void FixedUpdate()
    {
        //read DNA
        float h=0;
        float v=0;
        bool crouch = false;
        if(dna.GetGene(0)==0) v=1;
        else if(dna.GetGene(0)==1) v= -1;
        else if(dna.GetGene(0)==2) h= -1;
        else if(dna.GetGene(0)==3) h= 1;
        else if(dna.GetGene(0)==4) m_Jump= true;
        else if(dna.GetGene(0)==5) crouch= true;
        
        m_move = v*Vector3.forward +h*Vector3.right;
        m_Character.Move(m_move,crouch,m_Jump);
        m_Jump=false;
        if(alive)
        timeAlive += Time.deltaTime;
        distanceTravelled = Vector3.Distance(this.transform.position,startPosition);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
