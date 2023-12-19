using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identifier : MonoBehaviour
{

    [SerializeField]
    Material friendMat;

    [SerializeField]
    Material enemyMat;

    [SerializeField]
    MeshRenderer objectToChange;

    [SerializeField]
    SkinnedMeshRenderer objectToChange2;

    private MeshRenderer mesh;
    private Card card;
    private MeshRenderer unitMesh;
    

    // Start is called before the first frame update
    void Start() {
        card = (Card)this.transform.parent.GetComponent<UnitComponent>().unit;
        mesh = this.gameObject.GetComponent<MeshRenderer>();

        unitMesh = this.transform.parent.GetComponentInChildren<MeshRenderer>();

    }

    // Update is called once per frame
    void Update() {
        if( mesh.material != friendMat && card.Owner == Game.players[Game.GetHumanPlayer()]) {
            mesh.material = friendMat;

            if(objectToChange) {
                objectToChange.material = friendMat;
            }
            if (objectToChange2) {
                objectToChange2.material = friendMat;
            }
            
        }
        else if(mesh.material != enemyMat && card.Owner != Game.players[Game.GetHumanPlayer()]) {
            //Debug.Log("Player: " + card.Owner.Name);
            mesh.material = enemyMat;
            if (objectToChange) {
                objectToChange.material = enemyMat;
            }
            if (objectToChange2) {
                objectToChange2.material = enemyMat;
            }
        }
    }
}
