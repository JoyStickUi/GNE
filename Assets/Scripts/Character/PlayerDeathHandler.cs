using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField]
    DeathScreenUIController deathScreenUIController;
    public void Handle(){
        Time.timeScale = 0;
        Cursor.visible = true;
        deathScreenUIController.Show();
    }
}
