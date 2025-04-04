using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject startUI;
    public GameObject startUI2;
    public GameObject createRoomUI;
    public GameObject joinRoomUI;

    public InputField playerNameInput;
    public InputField createRoomNameInput;
    public InputField joinRoomNameInput;

    private string playerName;
    private string roomName;

    void Start()
    {
        ShowStartUI(); // Start from first screen
    }

    public void ShowStartUI()
    {
        startUI.SetActive(true);
        startUI2.SetActive(false);
        createRoomUI.SetActive(false);
        joinRoomUI.SetActive(false);
    }

    public void OnStartNextButton()
    {
        playerName = playerNameInput.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            startUI.SetActive(false);
            startUI2.SetActive(true);
        }
    }

    public void OnCreateRoomButton()
    {
        startUI2.SetActive(false);
        createRoomUI.SetActive(true);
    }

    public void OnJoinRoomButton()
    {
        startUI2.SetActive(false);
        joinRoomUI.SetActive(true);
    }

    public void OnCreateRoomNext()
    {
        roomName = createRoomNameInput.text;
        if (!string.IsNullOrEmpty(roomName))
        {
            Debug.Log("Creating room: " + roomName + " by player: " + playerName);
            // Add logic to create room
        }
    }

    public void OnJoinRoomNext()
    {
        roomName = joinRoomNameInput.text;
        if (!string.IsNullOrEmpty(roomName))
        {
            Debug.Log("Joining room: " + roomName + " by player: " + playerName);
            // Add logic to join room
        }
    }
}
