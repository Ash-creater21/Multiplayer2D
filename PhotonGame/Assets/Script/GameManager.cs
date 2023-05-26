using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ; 

public class GameManager : MonoBehaviour
{
   public GameObject PlayerPrefab ;
   public GameObject GameCanvas ; 
   public GameObject SceneCamera;
   [SerializeField] private Transform SpawnPosition;
   [SerializeField] private Text PingText ; 

   // disconnect Panel 
   private bool off = false ; 
   [SerializeField] private GameObject disconnectUI ; 

   // player feed : player Leave Text "Player1 has left meet "

   [SerializeField] private GameObject playerFeed ; 
   [SerializeField] private GameObject FeedGrid ; 

// when start button would click then this function would load 
public void Awake() 
{
    GameCanvas.SetActive(true);
    
   
}
private void Update() 
{
   PingText.text = "PING " + PhotonNetwork.GetPing();  
   checkInput() ; 
}
   public void SpawnPlayer() 
   {
        float randomValue = Random.Range(-1f,1f); 
      //   PhotonNetwork.Instantiate(PlayerPrefab.name,new Vector2(this.transform.position.x * randomValue,this.transform.position.y),Quaternion.identity,0);
      PhotonNetwork.Instantiate(PlayerPrefab.name,SpawnPosition.position ,Quaternion.identity,0);
      
        GameCanvas.SetActive(false);  
        SceneCamera.SetActive(false); 
   }
   private void checkInput() 
   {
      // To toggle Escape 
      if(Input.GetKeyDown(KeyCode.Escape) && off)
      {
         disconnectUI.SetActive(false) ; 
         Debug.Log("Hi");
         off = false ; 
      }
      else if (!off && Input.GetKeyDown(KeyCode.Escape))
      {
         disconnectUI.SetActive(true) ; 
         off = true ; 
      }
   }
   public void LeaveRoom() 
   {
      PhotonNetwork.LeaveRoom() ; 
      PhotonNetwork.LoadLevel("MainMenu") ; 
   }

   private void OnPhotonPlayerConnected(PhotonPlayer player){

      GameObject obj = Instantiate(playerFeed,new Vector2(0,0), Quaternion.identity) ; 
      obj.transform.SetParent(FeedGrid.transform,false); 
      obj.GetComponent<Text>().text = player.name + "Joined The Game " ; 
      obj.GetComponent<Text>().color = Color.green ;  
   }

   private void OnPhotonPlayerDisconnected(PhotonPlayer player){

      GameObject obj = Instantiate(playerFeed,new Vector2(0,0), Quaternion.identity) ; 
      obj.transform.SetParent(FeedGrid.transform,false); 
      obj.GetComponent<Text>().text = player.name + "Left The Game " ; 
      obj.GetComponent<Text>().color = Color.red ;  
   }
}
