using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour
{
    private Player[] Players;

    public int ActivePlayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        Players = GetComponentsInChildren<Player>();
        ActivePlayerIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RestartPlayerTurnIndex();
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (Input.GetButtonDown("Right") && Players[ActivePlayerIndex].transform.position.x < 4)
        {
            Players[ActivePlayerIndex].transform.position = new Vector3(Players[ActivePlayerIndex].transform.position.x + 1, Players[ActivePlayerIndex].transform.position.y, Players[ActivePlayerIndex].transform.position.z);
            ActivePlayerIndex++;
        }
        else if (Input.GetButtonDown("Left") && Players[ActivePlayerIndex].transform.position.x > 0)
        {
            Players[ActivePlayerIndex].transform.position = new Vector3(Players[ActivePlayerIndex].transform.position.x - 1, Players[ActivePlayerIndex].transform.position.y, Players[ActivePlayerIndex].transform.position.z);
            ActivePlayerIndex++;
        }
        else if (Input.GetButtonDown("Up") && Players[ActivePlayerIndex].transform.position.y < 4)
        {
            Players[ActivePlayerIndex].transform.position = new Vector3(Players[ActivePlayerIndex].transform.position.x, Players[ActivePlayerIndex].transform.position.y + 1, Players[ActivePlayerIndex].transform.position.z);
            ActivePlayerIndex++;
        }
        else if (Input.GetButtonDown("Down") && Players[ActivePlayerIndex].transform.position.y > 0)
        {
            Players[ActivePlayerIndex].transform.position = new Vector3(Players[ActivePlayerIndex].transform.position.x, Players[ActivePlayerIndex].transform.position.y - 1, Players[ActivePlayerIndex].transform.position.z);
            ActivePlayerIndex++;
        }
    }

    private void RestartPlayerTurnIndex()
    {
        if (ActivePlayerIndex >= Players.Length)
        {
            ActivePlayerIndex = 0;
        }
    }
}
