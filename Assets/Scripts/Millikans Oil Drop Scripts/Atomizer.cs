using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atomizer : MonoBehaviour
{

    [SerializeField] private Oil drop;

    private float currTime;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            ReleaseParticles(200);
        }
    }

    public void ReleaseParticles(int count)
    {
        StartCoroutine(Release());
        IEnumerator Release()
        {
            for (int i = 0; i < count; i++)
            {
                yield return new WaitForSeconds(0.0001f);
                var oil = Instantiate(drop, transform.position, Quaternion.identity);
                oil.GetComponent<Rigidbody>().AddForce(oil.transform.forward * Random.Range(3, 15));
                oil.AssignRandomChargeAndMass();
                oil.name = "Drop" + i;
            }
        }      
    }


}
