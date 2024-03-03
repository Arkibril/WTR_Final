using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLaunch : MonoBehaviour, IDataPersistence<PlayerData>
{
    private bool isNewPlayer;
    private string sceneToLoad;
    void Start()
    {
        DataPersistenceManager.dataHandler = new FileDataHandler(Application.persistentDataPath, "player_data", DataPersistenceManager.doIUseEncryption);
        StartCoroutine(NewPlayerCheck());
    }

    public void LoadData(PlayerData data)
    {
        isNewPlayer = data.isNewPlayer;
    }

    public void SaveData(ref PlayerData data)
    {
        data.isNewPlayer = isNewPlayer;
    }

    IEnumerator NewPlayerCheck()
    {
        yield return new WaitForSeconds(0.5f);
        DataPersistenceManager.instance.LoadPlayerData();
        yield return new WaitForSeconds(1);
        if (isNewPlayer)
        {
            sceneToLoad = "Cinematique";
            isNewPlayer = false;
            DataPersistenceManager.instance.SavePlayerData();
        }
        else
        {
            sceneToLoad = "Map Fixed";
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
