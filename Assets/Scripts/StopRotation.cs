using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRotation : MonoBehaviour
{
    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.transform.parent.rotation.z * -2);

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Blocker"))
        {

            GameManager.instance.HealthControl();
            GameObject a = Instantiate(Explosion, transform) as GameObject;
            a.transform.SetParent(null);
            a.transform.localScale = new Vector3(1, 1, 1);
            Destroy(other.gameObject);
           
           // GameManager.instance.GameEnd();
           
        }
        if (other.gameObject.CompareTag("Finish")) {

            GameManager.instance.Cars +=1;

        }
      //  if (other.gameObject.CompareTag("coin")) {
    //        other.GetComponent<SimpleCollectibleScript>().Collect();
    //    }
    }

}
