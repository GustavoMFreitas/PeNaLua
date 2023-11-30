using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using SFB;

public class SaveCSV : MonoBehaviour
{
    [SerializeField] private Movement movement;
    private List<float> data = new List<float>();

    public void SaveCSVFile()
    {
        data = movement.GetgraphPoints();
        string path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "Meus_Dados", "csv");

        int index = 0;

        if (path.Length != 0)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (float row in data)
                {
                    float aux=ExtensionMethods.Remap(row, 2.8f, -2, 100, 0);
                    writer.WriteLine(index+","+string.Join(",", (int)aux));
                    index += 5;
                }
            }
        }
    }
}