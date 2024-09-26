using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence // interface defines a virtual class that written with functions we need when using the certain method/class
{
    void LoadData(GameData data);

    void SaveData(ref GameData data);
}
