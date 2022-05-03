using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
 

   private void OnCollisionEnter(Collision other) {
       switch(other.gameObject.tag) {
           case "Friendly":
             Debug.Log("This thing is Friendly");
             break;
           case "Finish":
             Debug.Log("Congrats, you finished");
             break;
           case "Fuel":
             Debug.Log("Gimme fuel gimme fire gimme that which i desire");
             break;
            default:
             reloadLev();
             break;
       }
   }

   private void reloadLev()
   {
      SceneManager.LoadScene("Sandbox");
   }
}
