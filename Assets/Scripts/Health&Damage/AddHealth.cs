using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddHealth : MonoBehaviour
{
    public int healingAmount=1;
    public AudioSource collisionAudio, destroyAudio;
    private bool alreadyDealt=false;
    private Text healingAmountText;
    void Start()
    {
        Debug.Log("start add health");
        //audioSources=GetComponents<AudioSource>();
        var audioSources=GetComponents<AudioSource>();

        collisionAudio=audioSources[0];
        destroyAudio=audioSources[1];
        healingAmountText=GetComponentInChildren<Text>();
        healingAmountText.enabled=false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(alreadyDealt){
            return;
        }
        collisionAudio.Play();
        
        Debug.Log("OnCollisionEnter2D for spaceship");
        DealHealth(collision.gameObject);
        
    }
    private void DealHealth(GameObject collisionGameObject){
        Debug.Log("DealHealth for spaceship");
        Health collidedHealth = collisionGameObject.GetComponent<Health>();
        collidedHealth.ReceiveHealing(healingAmount);
        healingAmountText.text=healingAmount+" HEALING!";
        healingAmountText.enabled=true;
        Debug.Log("added healing to player");
        alreadyDealt=true;

        destroyAudio.Play();
        StartCoroutine(WaitThenDestroy());
        
    }
    private IEnumerator WaitThenDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            Destroy(this.gameObject);
        }
        
    }
}
