﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class ReadWriteTxt : MonoBehaviour {

    public List<string> nicknames = new List<string>();

    public List<int> scores = new List<int>();





    StreamReader streamReader, streamReader2, streamReader3;
    string filename;
	// Use this for initialization
	void Start () {
        filename = @"../NeverQuest/stats/leaderboard.txt";

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
        //print(s);

        for (int i = 0; i < nicknames.Count; i++)
        {
           // print("AQUI: " + nicknames.ToArray()[i].ToString());
            aux.Add(nicknames.ToArray()[i].ToString() + " " + scores.ToArray()[i].ToString());
        }
        //string[] lines = { "First line", "Second line", "Third line" };

        System.IO.File.WriteAllLines(filename, aux.ToArray());
    }

    public void WavesWriteFile()
    {
        List<string> aux = new List<string>();

        List<int> new_lista = new List<int>();

        streamReader3 = new StreamReader(@"../NeverQuest/stats/waves.txt");
        using (streamReader3)
        {
            int lineNumber = 0;
            string line = streamReader3.ReadLine();

            while (line != null)
            {
                lineNumber++;

                string[] digits = Regex.Split(line, @"\D+");
                // foreach(string i in digits) print(i + "\n");
                foreach (string value in digits)
                {
                    int number;
                    if (int.TryParse(value, out number))
                    {
                        new_lista.Add(int.Parse(value));

                    }


                }

                line = streamReader3.ReadLine();
            }
        }
        int count = 0;
        foreach (int wavetime in new_lista)
        {
            if (this.GetComponent<PlayerController>().WavesTimers.Count > count) aux.Add("" + (wavetime + Mathf.RoundToInt(this.GetComponent<PlayerController>().WavesTimers[count])) + " segundos.\n");
            else aux.Add("" + Mathf.RoundToInt(wavetime) + " segundos.\n");

            count++;
        }
        System.IO.File.WriteAllLines(@"../NeverQuest/stats/waves.txt", aux.ToArray());
    }

    public void WritePlayerStats()
    {
        print("A atualizar o player.txt\n");
        int i = 0;
        while (File.Exists(@"../NeverQuest/stats/player" + i + ".txt")) i++;
        File.Create(@"../NeverQuest/stats/player" + i + ".txt").Close(); // Para apagar o que lá está
        List<string> aux = new List<string>();


        aux.Add("/*=========== New Player ===========*/\n");
        aux.Add("Tempo total de jogo: " + this.GetComponent<PlayerController>().numTimePlayed + " segundos.\n");

        int g = 1;
        foreach (int time in this.GetComponent<PlayerController>().WavesTimers)
        {
            if (time != 0) { 
            aux.Add("Duração da wave" + g + ": " + Mathf.RoundToInt(time) + " segundos.\n");
            g++;
            }
        }

        aux.Add("Número de doors catched: " + this.GetComponent<PlayerController>().numDoorsCatched + "\n");
        aux.Add("Número de mobs mortos: " + this.GetComponent<PlayerController>().numMobsKilled + "\n");
        aux.Add("Número de vezes que Flamethrower foi usado : " + this.GetComponent<PlayerController>().numFlamethrowerUsed + "\n");
        aux.Add("Número de balas disparadas: " + this.GetComponent<PlayerController>().numBulletsUsed + "\n");
        aux.Add("Número de traps usadas: " + this.GetComponent<PlayerController>().numTrapsUsed + "\n\n");

        aux.Add("BearTrap: " + this.GetComponent<PlayerController>().numBearTrap + "\n");
        aux.Add("Fire Trap: " + this.GetComponent<PlayerController>().numFireTrap + "\n");
        aux.Add("Poison Trap: " + this.GetComponent<PlayerController>().numPoisonTrap + "\n");
        aux.Add("DDOS Trap: " + this.GetComponent<PlayerController>().numDDOSTrap + "\n");
        aux.Add("MoneyTrap: " + this.GetComponent<PlayerController>().numMoneyTrap + "\n");
        aux.Add("IronMaidenTrap: " + this.GetComponent<PlayerController>().numIronMaidenTrap + "\n");




        //string[] lines = { "First line", "Second line", "Third line" };

        System.IO.File.WriteAllLines(@"../NeverQuest/stats/player" + i + ".txt", aux.ToArray());
    }
    public List<int> lista = new List<int>();

    public void ActualizeOverviewStats()
    {
        print("A atualizar o overviewStats.txt\n");

        int numMobsKilled = 0, numFlamethrowerUsed = 0, numBulletsUsed = 0, numTrapsUsed = 0, numBearTrap = 0, numFireTrap = 0, numPoisonTrap = 0, numDDOSTrap = 0, numMoneyTrap = 0, numIronMaidenTrap = 0;

        int[] arrayStats = { numMobsKilled, numFlamethrowerUsed, numBulletsUsed, numTrapsUsed, numBearTrap, numFireTrap, numPoisonTrap, numDDOSTrap, numMoneyTrap, numIronMaidenTrap };

        lista = new List<int>();

        streamReader2 = new StreamReader(@"../NeverQuest/stats/overviewStats.txt");
        using (streamReader2)
        {
            int lineNumber = 0;
            string line = streamReader2.ReadLine();

            while (line != null)
            {
                lineNumber++;

                string[] digits = Regex.Split(line, @"\D+");
                // foreach(string i in digits) print(i + "\n");
                foreach (string value in digits)
                {
                    int number;
                    if (int.TryParse(value, out number))
                    {
                        lista.Add(int.Parse(value));

                    }


                }

                line = streamReader2.ReadLine();
            }
            foreach (int id in arrayStats) print("stats " + id);
        }

        File.Create(@"../NeverQuest/stats/overviewStats.txt").Close(); // Para apagar o que lá está
        List<string> aux = new List<string>();
        aux.Add("/*=========== Overrall Stats===========*/\n");
        aux.Add("Tempo total de jogo: " + (lista[0] + this.GetComponent<PlayerController>().numTimePlayed) + " segundos.\n");
        //aux.Add("Tempo de cada wave:\n");
        //int aux_int = 1;
        //foreach (float time in this.GetComponent<PlayerController>().WavesTimers)
        //{
        //    aux.Add("Tempo wave" + aux_int + ": " + (lista[aux_int] + Mathf.RoundToInt(time)) + " segundos.\n");
        //    aux_int++;
        //}

        aux.Add("Número de doors catched: " + this.GetComponent<PlayerController>().numDoorsCatched + "\n");
        aux.Add("Número de mobs mortos: " + (lista[1] + this.GetComponent<PlayerController>().numMobsKilled) + "\n");
        aux.Add("Número de vezes que Flamethrower foi usado : " + (lista[2] + this.GetComponent<PlayerController>().numFlamethrowerUsed) + "\n");
        aux.Add("Número de balas disparadas: " + (lista[3] + this.GetComponent<PlayerController>().numBulletsUsed) + "\n");
        aux.Add("Número de traps usadas: " + (lista[4] + this.GetComponent<PlayerController>().numTrapsUsed) + "\n\n");

        aux.Add("BearTrap: " + (lista[5] + this.GetComponent<PlayerController>().numBearTrap) + "\n");
        aux.Add("Fire Trap: " + (lista[6] + this.GetComponent<PlayerController>().numFireTrap) + "\n");
        aux.Add("Poison Trap: " + (lista[7] + this.GetComponent<PlayerController>().numPoisonTrap) + "\n");
        aux.Add("DDOS Trap: " + (lista[8] + this.GetComponent<PlayerController>().numDDOSTrap) + "\n");
        aux.Add("MoneyTrap: " + (lista[9] + this.GetComponent<PlayerController>().numMoneyTrap) + "\n");
        aux.Add("IronMaidenTrap: " + (lista[10] + this.GetComponent<PlayerController>().numIronMaidenTrap) + "\n");

        System.IO.File.WriteAllLines(@"../NeverQuest/stats/overviewStats.txt", aux.ToArray());
    }
}
