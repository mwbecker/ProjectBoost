using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
 
  [SerializeField] float delay  = 1f;

   AudioSource audioSource;

   public float frequency = 440;
   public int position = 0;
   public int samplerate = 44100;
  
  bool isTransitioning = false;
  [SerializeField] AudioClip death;  
  [SerializeField] AudioClip success; 


   [SerializeField] ParticleSystem death_particles;  
   [SerializeField] ParticleSystem success_particles; 


   [SerializeField] ParticleSystem mainThruster;
    [SerializeField] ParticleSystem leftThruster;

    [SerializeField] ParticleSystem rightThruster;


   void Start()
   {
       audioSource = GetComponent<AudioSource>();
   }
   private void OnCollisionEnter(Collision other) {
       switch(other.gameObject.tag) {
           case "Friendly":
             Debug.Log("This thing is Friendly");
             break;
           case "Finish":
             Debug.Log("Congrats, you finished");
             startSuccessSequence();
             break;
            default:
             startCrashSequence();
             break;
       }
   }


  private void startCrashSequence()
    {
       if (!isTransitioning) {
         audioSource.Stop();
         mainThruster.Stop();
         leftThruster.Stop();
         rightThruster.Stop();

        isTransitioning = true;
         death_particles.Play();
        audioSource.PlayOneShot(death);
        
       }
       GetComponent<Movement>().enabled = false;
       Invoke("reloadLev", delay);
    }
    private void startSuccessSequence()
    {
         if (!isTransitioning) {
        isTransitioning = true;
         audioSource.Stop();
         mainThruster.Stop();
         leftThruster.Stop();
         rightThruster.Stop();
        success_particles.Play();
        audioSource.PlayOneShot(success);
       }
       GetComponent<Movement>().enabled = false;
       Invoke("nextLev", delay);
    }
   private void reloadLev()
   {
      isTransitioning = false;
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      SceneManager.LoadScene(currentSceneIndex);
   }

   private void nextLev()
   {
      isTransitioning = false;
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      int nextSceneIndex = (currentSceneIndex + 1) % (SceneManager.sceneCountInBuildSettings);
      
      SceneManager.LoadScene(nextSceneIndex);
   }
}
