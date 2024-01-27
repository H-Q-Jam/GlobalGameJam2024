using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
//using System.Diagnostics;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{

    public TextMeshProUGUI playerNickNameTM;

    public static NetworkPlayer Local { get; set; }

    public Transform playerModel;

    [Networked(OnChanged = nameof(OnNickNameChanged))]
    public NetworkString<_16> nickName { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        
    } 

    public override void Spawned()
    {
        if(Object.HasInputAuthority)
        {
            Local = this;
            Debug.Log("Spawned local Player");

            RPC_SetNickName(PlayerPrefs.GetString("PlayerNickname"));


        }
        else Debug.Log("Spawned remote Player");

        transform.name = $"P_{Object.Id}";// plus simple pour savoir qui est qui
    }

    public void PlayerLeft(PlayerRef player)
    {
        if(player == Object.InputAuthority) 
        {
            Runner.Despawn(Object);
        }
    }

    static void OnNickNameChanged(Changed<NetworkPlayer> changed)
    {
        UnityEngine.Debug.Log($"{Time.time} OnHPChanged value {changed.Behaviour.nickName}");

        changed.Behaviour.OnNickNameChanged();
    }


    private void OnNickNameChanged()
    {
        UnityEngine.Debug.Log($"Nickname changed for player to {nickName} for player {gameObject.name}");

        playerNickNameTM.text = nickName.ToString();
    }


    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetNickName(string nickName, RpcInfo info = default)
    {
        Debug.Log($"[RPC] SetNickName {nickName}");
        this.nickName = nickName;
    }
}
