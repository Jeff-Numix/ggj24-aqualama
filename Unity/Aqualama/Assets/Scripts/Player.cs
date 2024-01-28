using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public float moveSpeed=10f;
    public Animator animator;
    public Transform lamaOrientation;
    public ParticleSystem crachatParticles;
    public Transform lamaLevelScale;
    [Header("Equipment")]
    public GameObject bonnetBain;
    public GameObject bouee;
    public GameObject serviette;
    [Header("Sound")]
    public AudioSource audioSource_DieProut;
    public AudioSource audioSource_Glissade;

    [Header("Debug")]
    public bool inputActive=true;



    void Awake()
    {
        Instance=this;
        bonnetBain.SetActive(false);
        bouee.SetActive(false);
        serviette.SetActive(false);
        
    }

    void Start(){
        GameManager.Instance.stepsAudioSource.volume=0;
    }

    void Update()
    {
        if(inputActive){
            // Manage horizontal move
            float moveX = Input.GetAxis("Horizontal");
            Vector3 newPosition = transform.position + new Vector3(moveX, 0, 0) * Time.deltaTime * moveSpeed;
            Vector3 boundPos = new Vector3(newPosition.x, transform.position.y, 0);
            // Vérifiez si la nouvelle position est à l'intérieur du Collider2D de la zone de déplacement.
            Collider2D moveZone = GameManager.Instance.currentMoveZone;
            
            // Disable move if outside Bounds
            if(moveZone!=null){
                if (!moveZone.bounds.Contains(boundPos))
                {
                    newPosition = transform.position;
                }
            }

            if(newPosition!=transform.position){
                transform.position = newPosition;
                animator.SetBool("IsWalking", true);
                GameManager.Instance.stepsAudioSource.volume=1;
            }
            else {
                animator.SetBool("IsWalking", false);
                GameManager.Instance.stepsAudioSource.volume=0;
            }
            if(moveX>0){
                lamaOrientation.localScale = new Vector3(1, 1, 1);
            }
            else if(moveX<0){
                lamaOrientation.localScale = new Vector3(-1, 1, 1);
            }

        }

        if(Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.playerIsDead){
           PlayCrachatAnim();
        }
    }

    public void PlayIdle(){
        animator.SetBool("IsWalking", false);
        animator.SetTrigger("GoIdle");
        GameManager.Instance.stepsAudioSource.volume=0;
    }

    public void PlayCrachatAnim(){
        animator.SetTrigger("Crachat");
        crachatParticles.Play();
        GameManager.Instance.PlaySpitSound();
    }

    public void PlayFallAnim()
    {
        animator.SetTrigger("Fall");
        animator.SetBool("IsWalking", false);
        GameManager.Instance.stepsAudioSource.volume=0;
        StartCoroutine(DisableInputsForSomeTimeCoroutine(0.3f,1));
        audioSource_Glissade.Play();
        
    }

    public void PlayStairsAnim()
    {
        animator.SetTrigger("Stairs");
    }
    public void GoIdle()
    {
        animator.SetTrigger("GoIdle");
    }

    public void PlayEcrabouilleAnim()
    {
        animator.SetTrigger("Ecrabouille");
        animator.SetBool("IsWalking", false);
        GameManager.Instance.stepsAudioSource.volume=0;
        StartCoroutine(DisableInputsForSomeTimeCoroutine(0,100));
    }
    
    public void PlayDeadVoiture()
    {
        animator.SetTrigger("DeadVoiture");
        animator.SetBool("IsWalking", false);
        GameManager.Instance.stepsAudioSource.volume=0;
        StartCoroutine(DisableInputsForSomeTimeCoroutine(0,100));
    }

    public void PlayDieProut()
    {
        animator.SetTrigger("DieProut");
        audioSource_DieProut.Play();
        StartCoroutine(DisableInputsForSomeTimeCoroutine(0,100));
    }

    public void PlayJumpPiscine(){
        animator.SetTrigger("Fall");
        inputActive=false;
        animator.SetBool("IsWalking", false);
    }

    private IEnumerator DisableInputsForSomeTimeCoroutine(float delay, float feuDuration){
        yield return new WaitForSeconds(delay);
        inputActive=false;
        yield return new WaitForSeconds(feuDuration);
        inputActive=true;
    }

    public void EquipObject(EquipmentType equipmentType){
        switch(equipmentType){
            case EquipmentType.BonnetBain:
                bonnetBain.SetActive(true);
                break;
            case EquipmentType.Bouee:
                bouee.SetActive(true);
                break;
            case EquipmentType.Serviette:
                serviette.SetActive(true);
                break;
        }
    }
}

public enum EquipmentType{
    BonnetBain,
    Bouee,
    Serviette
}