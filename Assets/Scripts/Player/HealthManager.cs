using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public GameObject pasekZycia;
    private float playerHealth;
    [SerializeField] private float maxPlayerHealth;

    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource source;

    public GameObject hurtText;
    public GameObject lavaText;
    public GameObject floatingLava;
    public float sumOfLavaDamage = 0;
    private TextMeshProUGUI healthText;

    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private BoxCollider2D resistanceCollider;
    private bool isResistant;

    [SerializeField] private SpriteRenderer playerGFX;
    [SerializeField] private SpriteRenderer playerEyesGFX;    
    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject cubeRotator;
    [SerializeField] private AudioClip deathMusic;
    [SerializeField] private GameObject menuButtons;

    void Awake() {
        pasekZycia = GameObject.FindGameObjectWithTag("HealthUI");
        healthText = pasekZycia.GetComponentInChildren<TextMeshProUGUI>();
        pasekZycia.GetComponent<Slider>().maxValue = maxPlayerHealth;
        playerHealth = maxPlayerHealth;

        UpdateSliderValue();
        UpdateHealthText();
    }

    public void LoseHealth(float healthPoints){
        if(!isResistant){
            playerHealth-= healthPoints;
            GetComponent<EntityChangeColor>().ChangeColor();
            UpdateSliderValue();

            if(playerHealth <= 0){
                PlayerDeath();
            }

            source.PlayOneShot(clip);
            FlyingDamage(healthPoints); 

            UpdateHealthText();
            StartCoroutine("EnableResistanceCollider");
        }
    }

    public void GainHealth(float healthPoints){
        playerHealth+= healthPoints;
        UpdateSliderValue();
        if(playerHealth > 100){
            playerHealth = 100;
        } 
        UpdateHealthText();
    }

     void FlyingDamage(float damage){
        GameObject floatingPoint =  Instantiate(hurtText, transform.position + new Vector3(0,2f,0), transform.rotation);
        floatingPoint.GetComponent<TextMeshPro>().text = "- " + damage.ToString();

        GameObject.Destroy(floatingPoint, 0.5f);
    }

    public void StartLavaDamage(){
        InvokeRepeating("LavaDamage", 0, 0.2f);
        floatingLava =  Instantiate(lavaText, transform.position + new Vector3(0,1f,0), transform.rotation);
        floatingLava.transform.parent = this.transform;
    }

    public void LavaDamage(){
        playerHealth -= 1f;
        sumOfLavaDamage += 1f;
        floatingLava.GetComponent<TextMeshPro>().text = "- " + sumOfLavaDamage.ToString();
        GetComponent<EntityChangeColor>().ChangeColor();
        UpdateSliderValue();
        UpdateHealthText();
        if(playerHealth < 0){
            PlayerDeath();
        }
    }

    public void StopLavaDamage(){
        sumOfLavaDamage = 0;
        CancelInvoke();
        Destroy(floatingLava);
    }

    private void UpdateHealthText(){
        healthText.text = playerHealth.ToString() + "/" + maxPlayerHealth.ToString();  
    }

    private void UpdateSliderValue(){
        pasekZycia.GetComponent<Slider>().value = playerHealth;
    }

    IEnumerator EnableResistanceCollider(){
        isResistant = true;
        resistanceCollider.enabled = true;
        playerCollider.enabled = false;
        yield return new WaitForSeconds(1f);

        EnablePlayerCollider();
    }

    private void EnablePlayerCollider(){
        isResistant = false;
        playerCollider.enabled = true;
        resistanceCollider.enabled = false;
    }

    void PlayerDeath(){
        GetComponent<PlayerMovement>().enabled = false;
        playerGFX.enabled = false;
        playerEyesGFX.enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Destroy(playerCollider);
        Destroy(resistanceCollider);
        bow.SetActive(false);
        cubeRotator.SetActive(false);
        AudioSource src;
        src = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<AudioSource>();
        src.clip = deathMusic;
        src.Play(0);
        src.volume = 0.3f;
        menuButtons.SetActive(true);
        Destroy(this);
    }
}
