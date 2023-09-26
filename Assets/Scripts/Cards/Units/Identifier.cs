using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identifier : MonoBehaviour
{

    [SerializeField]
    Material friendMat;

    [SerializeField]
    Material enemyMat;

    private MeshRenderer mesh;
    private Card card;

    // Start is called before the first frame update
    void Start()
    {
        card = (Card)this.transform.parent.GetComponent<UnitComponent>().unit;
        mesh = this.gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if( mesh.material != friendMat && card.Owner == Game.players[0])
        {
            mesh.material = friendMat;
        }else if(mesh.material != enemyMat && card.Owner != Game.players[0])
        {
            //Debug.Log("Player: " + card.Owner.Name);
            mesh.material = enemyMat;
        }
    }
}
