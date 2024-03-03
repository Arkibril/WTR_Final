using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCinematiqueChange : MonoBehaviour
{
    public bool IsNew;
    public string sceneToLoad;
    private void Start()
    {
        ChangeFirstLaunchCineValue();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void LoadCinematiqueData(CinematiqueData data)
    {
        IsNew = data.FirstLaunchCine;
    }

    public void SaveCinematiqueData( ref CinematiqueData cinematiqueData)
    {
        DataPersistenceManager.dataHandler.Save(cinematiqueData);
        cinematiqueData.FirstLaunchCine = IsNew;
    }

    public void ChangeFirstLaunchCineValue()
    {
        CinematiqueData cinematiqueData = DataPersistenceManager.dataHandler.Load<CinematiqueData>();
        if (cinematiqueData == null)
        {
            cinematiqueData = new CinematiqueData();
        }

        cinematiqueData.FirstLaunchCine = false;
    }


    IEnumerator NewCinematiqueCheck()
    {
        yield return new WaitForSeconds(0.5f);
        DataPersistenceManager.instance.LoadCinematiqueData();
        yield return new WaitForSeconds(1);
        if (IsNew)
        {
            sceneToLoad = "Cinematique";
            IsNew = false;
            DataPersistenceManager.instance.SaveCinematiqueData();
        }
        else
        {
            sceneToLoad = "Map Fixed";
        }
    }

}
