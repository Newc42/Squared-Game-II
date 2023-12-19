using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private float enemyHealth;
    [SerializeField] private Slider slider;
    [SerializeField] private RectTransform enemyHealthBar;

    public GameObject damageText;
    public GameObject player;

    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource source;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        slider.maxValue = enemyHealth;
        UpdateSlider();
        SetSliderLength();
    }

    public void LoseHP(float damage){
        enemyHealth -= damage;
        SpawnDamageText(damage);

        UpdateSlider();

        source.PlayOneShot(clip);

        GetComponent<EntityChangeColor>().ChangeColor();

        if(enemyHealth <= 0)
        {
            EnemyDie();
        } 
    }

    private void EnemyDie(){
        SpawnDeathParticles();
        GameObject.Destroy(this.gameObject);
    }

    private void SpawnDeathParticles(){
        GameObject particles = Instantiate(deathParticles, transform);
        particles.transform.parent = null;
        GameObject.Destroy(particles, 6f);
    }

    void SpawnDamageText(float damage){
        GameObject floatingPoint =  Instantiate(damageText, transform.position, transform.rotation);
        floatingPoint.GetComponent<TextMeshPro>().text = "- " + damage.ToString();

        GameObject.Destroy(floatingPoint, 0.5f);
    }

    private void UpdateSlider(){
        slider.value = enemyHealth;
    }

    private void SetSliderLength(){
        enemyHealthBar.sizeDelta = new Vector2(enemyHealth * 3, 12.4f);
    }
}
