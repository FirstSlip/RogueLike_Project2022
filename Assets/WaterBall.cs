using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

public class WaterBall : MonoBehaviour, ISpell
{
    private Animator anim;
    [SerializeField]
    private AnimationClip impact;
    public bool isUpgraded = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnImpact(RaycastHit2D hitInfo)
    {
        Debug.Log(hitInfo.distance);
        if (isUpgraded)
        {
            StartCoroutine(AddWaterExplode(hitInfo));
        }
        else StartCoroutine(AddWaterExplode(hitInfo));
    }

    private IEnumerator AddWaterExplode(RaycastHit2D hit)
    {
        anim.Play("WaterBallImpact");
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }

    private IEnumerator AddWaterTornado(RaycastHit2D hit)
    {
        anim.Play("WaterBallImpact");
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
