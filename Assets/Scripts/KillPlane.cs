using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    public GameObject hole;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "AwardsScore")
        {
            hole.GetComponent<HoleController>().AwardScore(collision.gameObject.GetComponent<ObjectController>().GetScoreValue());
            Destroy(collision.gameObject);
        }
    }
}
