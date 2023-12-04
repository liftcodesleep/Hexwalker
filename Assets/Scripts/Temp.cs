using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Temp : NetworkBehaviour {
  private  NetworkVariable<float> randInt = new NetworkVariable<float>(1f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
  public struct TempData : INetworkSerializable {
    public int _int;
    public bool _bool;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
      serializer.SerializeValue(ref _int);
      serializer.SerializeValue(ref _bool);
    }
  }

  [ServerRpc]
  private void TestServerRPC() {
    Debug.Log("Test RPC " + OwnerClientId);
  }

  [ClientRpc]
  private void TestClientRpc() {
    Debug.Log("Test RPC " + OwnerClientId);
  }

  // Start is called before the first frame update
  void Start() {
    transform.position = new Vector3(-36, 0 , 16);
  }

  public override void OnNetworkSpawn() {
    randInt.OnValueChanged += (float old, float updatedValue) => { Debug.Log(randInt.Value); };
  }

  // Update is called once per frame
  void Update() {
    if(!IsOwner) {
      return;
    }
    Vector3 moveDir = new Vector3(0,0,0);
    if (Input.GetKey(KeyCode.I)) {
      randInt.Value+=1f;
      TestServerRPC();
    }
    if (Input.GetKey(KeyCode.K)) {
      randInt.Value -= 1f;
    }
    if (Input.GetKey(KeyCode.J)) {
      moveDir += Vector3.left;
    }
    if (Input.GetKey(KeyCode.L)) {
      moveDir += Vector3.right;
    }
    //float moveSpeed = 3f;
    transform.position += moveDir * randInt.Value * Time.deltaTime;
  }
}
