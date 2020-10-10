using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpawnInfo
{
    string SceneName;
    float posX;
    float posY;
    float posZ;
    string spawnMonster;
    int spawnCount;

    public SpawnInfo(string _sceneName,string _posX,string _posY,string _posZ,string _spawnMonster,string _spawnCount)
    {
        SceneName = _sceneName;
        posX = float.Parse(_posX);
        posY = float.Parse(_posY);
        posZ = float.Parse(_posZ);
        spawnMonster = _spawnMonster;
        spawnCount = int.Parse(_spawnCount);

    }

}


public class SpawnTable
{ 

    
}
