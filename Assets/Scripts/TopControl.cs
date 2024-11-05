using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopControl : MonoBehaviour
{
    public UnityEngine.UI.Button btn;
    public UnityEngine.UI.Text zaman, can, durum;
    private Rigidbody rg;
    public float hiz=1.5f;
    float zamanSayaci = 30f;
    int canSayaci = 3;
    bool oyunDevam=true;
    bool oyunTamam = false;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (oyunDevam&&!oyunTamam)
        {
            zamanSayaci -= Time.deltaTime;
            zaman.text = (int)zamanSayaci + "";
        }
        else if (!oyunTamam)
        { 
            durum.text = "Oyun Tamamlanamadý";
            btn.gameObject.SetActive(true);
        }

        

        if (zamanSayaci<0)
        {
            oyunDevam = false;
        }
    }
    private void FixedUpdate()
    {
        if (oyunDevam&&!oyunTamam)
        {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(dikey, 0, -yatay);
            rg.AddForce(kuvvet * hiz);
        }
        else
        {
            rg.velocity = Vector3.zero;
            rg.angularVelocity = Vector3.zero;
        }
    }
    void OnCollisionEnter(Collision cls)
    {
        string objIsmi = cls.gameObject.name;
        if (objIsmi.Equals("bitis"))
        {
            //print("Oyun Tamamlandý");
            oyunTamam = true;
            durum.text = "Oyun Tamamlandý";
            btn.gameObject.SetActive(true);
        }
        else if (!objIsmi.Equals("icYuzey")&&!objIsmi.Equals("disYuzey"))
        {
            canSayaci -= 1;
            can.text = canSayaci+"";
            if (canSayaci == 0)
            {
                oyunDevam=false;
            }
        }
    }

}
