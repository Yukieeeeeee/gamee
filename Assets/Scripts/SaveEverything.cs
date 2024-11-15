using System.IO;
using UnityEngine;

public class SaveEverything
{
    public static readonly string Save_Folder = Application.persistentDataPath + "/saves/";
    public static readonly string Ext = ".json";


    public static void Save(string filename, string data) {

        if (!Directory.Exists(Save_Folder)) {
        Directory.CreateDirectory(Save_Folder);
        }
        File.WriteAllText(Save_Folder + filename + Ext, data);

    }

    
    public static string Load(string filename)
    {
        string fileloc = Save_Folder + filename + Ext;
        if (File.Exists(fileloc))
        {
            string loadedData = File.ReadAllText(fileloc);
            return loadedData;
        }
        else {
            return null;
        }
    }
}
