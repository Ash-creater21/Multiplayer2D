using UnityEngine;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    [SerializeField] private string VersionName = "0.1" ; 
    [SerializeField] private GameObject userNameMenu ; 
    [SerializeField] private GameObject ConnectPanel ; 

    // UI Stuff related variables 

    [SerializeField] private InputField usernameInput ; 
    [SerializeField] private InputField CreateGameInput ; 
    [SerializeField] private InputField JoinGameInput ; 

    [SerializeField] private GameObject StartButton ; 


    private void Start() {

    }

    private void Awake() 
    {
        // it will set isMessageQueueRunning to true
        PhotonNetwork.ConnectUsingSettings(VersionName);
    }
   // calls automatically when Server get activated 
   private void OnConnectedToMaster() 
   {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected"); 
   }

   public void ChangeUserNameInput()
   {
    if(usernameInput.text.Length >=3 )
    {
        StartButton.SetActive(true); 
    }
    else 
    {
        StartButton.SetActive(false); 
    }

   }

   // this is the method where we set userName 

   public void SetUserName()
   {
    userNameMenu.SetActive(false) ; 
    PhotonNetwork.playerName = usernameInput.text ; 
   }

   public void CreateGame() 
   {
    PhotonNetwork.CreateRoom(CreateGameInput.text, new RoomOptions() {maxPlayers = 5 }, null) ; 
   }

    public void JoinGame() 
   {
        RoomOptions roomOptions = new RoomOptions
        {
            maxPlayers = 5 
        }; 
       
        PhotonNetwork.JoinOrCreateRoom(JoinGameInput.text,roomOptions,TypedLobby.Default);
   }
    /*
    typedLobby: Lobby you want a new room to be listed in. 
    Ignored if the room was existing and got joined.
    */

    // Build in method triggers when room gets joined that is load the level 
   public void OnJoinedRoom()
   {
        PhotonNetwork.LoadLevel("MainGame") ; 
        // Application.LoadLevel("MainGame");
   }

 


}

/*

bool PhotonNetwork.ConnectUsingSettings(string versionname);
Connect to Photon as configured in the editor (saved in PhotonServerSettings file).

This method will disable offlineMode (which won't destroy any instantiated GOs) 
and it will set isMessageQueueRunning to true.
 Your server configuration is created by the PUN Wizard and contains the AppId and region 
 for Photon Cloud games and the server address if you host Photon yourself. 
 These settings usually don't change often. 
 To ignore the config file and connect anywhere call: PhotonNetwork.ConnectToMaster. 
 To connect to the Photon Cloud, a valid AppId must be in the settings file
 */ 


