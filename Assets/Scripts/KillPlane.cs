using UnityEngine;

public class KillPlane : MonoBehaviour
{
    public GameObject hole;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("AwardsScore"))
        {
            hole.GetComponent<HoleController>().AwardScore(collision.gameObject.GetComponent<ObjectController>().GetScoreValue());
            Destroy(collision.gameObject);
        }
    }
}
