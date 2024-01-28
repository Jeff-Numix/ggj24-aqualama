using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Case04_Parc : MonoBehaviour
{
    public Animator piafAnimator;
    public Zone piafZone;
    public AudioSource piafAudioSource;
    public Bulle bulleInfoPiaf;

    private bool piafDead=false;
    private bool isInPiafZone;
    private bool isInSpitZone;

    public float playerKillDelay=1f;
    public AudioSource oiseauAttackAudioSource;
    public AudioSource playerDeadAudioSource;
    public AudioSource oiseauRireAudioSource;
    public void EnterCase(){
        piafDead=false;
        isInSpitZone=false;
    }
    public void Update(){
        if(Input.GetKeyDown(KeyCode.Space) && isInSpitZone && !piafDead){
            KillPiaf();
        }
    }
    void KillPiaf(){
        StartCoroutine(KillPiafCoroutine());
    }
    IEnumerator KillPiafCoroutine(){
        yield return new WaitForSeconds(0.25f);
        piafAnimator.SetTrigger("Fly");
        piafZone.gameObject.SetActive(false);
        piafDead=true;
        piafAudioSource.Play();
        bulleInfoPiaf.Hide();
    }

    public void OnPlayerEnterSpitZone(){
        isInSpitZone=true;
        CheckShowBulleInfo();
    }
    public void OnPlayerExitSpitZone(){
        isInSpitZone=false;
        bulleInfoPiaf.Hide();
    }

    public void OnPlayerEnterPiafZone(){
        isInPiafZone=true;
        if(!piafDead){
            StartCoroutine(PlayerDeadCoroutine());
        }
    }
    public void OnPlayerExitPiafZone(){
        isInPiafZone=false;
    }

    public void ResetCase(){
        piafAnimator.SetTrigger("Default");
        piafZone.gameObject.SetActive(true);
        piafDead=false;
    }

    public void CheckShowBulleInfo(){
        if(!piafDead){
            bulleInfoPiaf.Show();
        }
    }

    IEnumerator PlayerDeadCoroutine(){
        piafAnimator.SetTrigger("Attack");
        oiseauAttackAudioSource.Play();
        yield return new WaitForSeconds(0.25f);
        Player.Instance.inputActive=false;
        yield return new WaitForSeconds(playerKillDelay);
        playerDeadAudioSource.Play();
        Player.Instance.PlayEcrabouilleAnim();
        GameManager.Instance.PlayerDie();
        yield return new WaitForSeconds(0.5f);
        oiseauRireAudioSource.Play();
    }
}
