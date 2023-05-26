using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ; 

public class ChatManager : MonoBehaviour
{
    // public Player plmove ; 
    public PhotonView photonView ; 
    public GameObject Cloudspeech ; 

    private bool DisableSend = false ; 

    private InputField ChatInputField ; 

    public Text UpdatedText ; 

    [SerializeField] private float timetodestroy =4f ; 

    private void Awake() 
    {
        ChatInputField = GameObject.Find("InputField").GetComponent<InputField>(); 
        
    }

    private void Update() 
    {
        if(photonView.isMine) 
        {
            if(!DisableSend && ChatInputField.isFocused) 
            {
                if(ChatInputField.text!="" && ChatInputField.text.Length > 0 && Input.GetKeyDown(KeyCode.Slash))
                {
                    photonView.RPC("SendMessage",PhotonTargets.AllBuffered,ChatInputField.text); 
                    Cloudspeech.SetActive(true); 
                    ChatInputField.text = "" ; 
                    DisableSend = true ; 
                }
            }
        }


    }
[PunRPC]
private void SendMessage(string message)
{
    UpdatedText.text = message ; 
    StartCoroutine("Remove") ; 
}
IEnumerator Remove()
{
    yield return new WaitForSeconds(timetodestroy) ; 
    Cloudspeech.SetActive(false); 
    DisableSend = false ; 
}

private void OnPhotonSerializeView(PhotonStream stream , PhotonMessageInfo info){ 
    if(stream.isWriting)
    {
        stream.SendNext(Cloudspeech.active);
    }
    else if(stream.isReading)
    {
        Cloudspeech.SetActive((bool)stream.ReceiveNext()); 
    }
}
}



