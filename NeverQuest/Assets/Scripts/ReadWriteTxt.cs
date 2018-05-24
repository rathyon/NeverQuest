using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ReadWriteTxt : MonoBehaviour {

    public List<string> nicknames = new List<string>();

    public List<int> scores = new List<int>();



    StreamReader streamReader;
    string filename;
	// Use this for initialization
	void Start () {
        filename = @"log.txt";

        readFile();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void readFile() {
        streamReader = new StreamReader(filename);
        using (streamReader)
        {
            int lineNumber = 0;
            string line = streamReader.ReadLine();
            string actualWord = "";
            int index = 0;
            while (line != null)
            {
                lineNumber++;
                actualWord = "";
                index = 0;

                /*Ler as variaveis*/
                while (index < line.Length)
                {
                    if (line[index].ToString() == " ")
                    {
                       // print("nickname: " + actualWord + "\n");
                        nicknames.Add(actualWord);
                        actualWord = "";
                    }
                    actualWord += line[index];
                    index++;
                }
                scores.Add(int.Parse(actualWord));
                line = streamReader.ReadLine();
            }
        }
    }

    public void WriteFile() {
        File.Create(filename).Close(); // Para apagar o que lá está
        List<string> aux = new List<string>();

        for (int i = 0; i < nicknames.Count; i++)
        {
           // print("AQUI: " + nicknames.ToArray()[i].ToString());
            aux.Add(nicknames.ToArray()[i].ToString() + " " + scores.ToArray()[i].ToString());
        }
        //string[] lines = { "First line", "Second line", "Third line" };

        System.IO.File.WriteAllLines(filename, aux.ToArray());
    }
}
