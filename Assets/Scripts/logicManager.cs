using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class logicManager : MonoBehaviour
{
    public string lokasi;
    public string dimana;
    public Text namaLokasi;

    public void pindahLokasi(int number)
    {
        if(number==1)
        {
            lokasi = "Lautan";
            namaLokasi.text = lokasi;
        }

        else if(number==2)
        {
            lokasi = "Daratan";
            namaLokasi.text = lokasi;
        }

        dimana = lokasi;
        
    }

    [ContextMenu("get lokasi")]
    public string getLokasi()
    {
        return dimana;
    }
}
