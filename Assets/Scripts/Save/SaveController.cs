using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveController : MonoBehaviour
{
    public static void SaveGame()
    {
        Save save = Save.CreateSave();

        //create binary formatter and filestream to save game
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        //save the game and close file
        bf.Serialize(file, save);
        file.Close();
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            //create binary formatter and file stream to read file
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            //get save data from file and close file
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            //set player stats based on file
            PlayerStats.instance.playerClass.currentHealth = save.playerHealth;
            PlayerStats.instance.playerClass.deckList = save.deck;
            PlayerStats.instance.roundsWon = save.roundsWon;
        }
    }
}
