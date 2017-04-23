using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {
    public Scenehandler sh;
    public InputField nameBox;
    public GameObject namePanel;
    private XmlDocument playerDoc;

    private string savePath;
    
    public void Start()
    {
        savePath = Application.dataPath + "/Resources/PlayerInfo.xml";
    }

    public void StartGame()
    {
        playerDoc = new XmlDocument();
        if (!File.Exists(savePath))
        {
            namePanel.SetActive(true);
        }
        //In this case load the character information and advance the scene.
        else
        {
            playerDoc.LoadXml(File.ReadAllText(savePath));
            readXml();
            sh.AdvanceScene();
        }
    }

    private void readXml()
    {
        foreach(XmlElement node in playerDoc.SelectNodes("//Characters"))
        {
            DataManager.data.Jean = int.Parse(node.SelectSingleNode("Jean").InnerText);
            DataManager.data.MrBones = int.Parse(node.SelectSingleNode("MrBones").InnerText);
            DataManager.data.Emo = int.Parse(node.SelectSingleNode("Emo").InnerText);
            DataManager.data.Dere = int.Parse(node.SelectSingleNode("Dere").InnerText);
        }

        foreach(XmlElement node in playerDoc.SelectNodes("//Player"))
        {
            DataManager.data.PlayerName = node.SelectSingleNode("Name").InnerText;
        }
    }

    //Deletes the playerinfo file and starts a new game.
    public void NewGame()
    {
        namePanel.SetActive(true);
    }

    public void CreateNew()
    {
        //Just setting the PlayerName
        DataManager.data.PlayerName = nameBox.text;

        //If the file doesn't exist. Create it.
        if (!File.Exists(savePath))
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";

            using (XmlWriter writer = XmlWriter.Create(savePath, settings))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("Info");

                writer.WriteStartElement("Characters");
                writer.WriteElementString("Jean", DataManager.data.Jean.ToString());
                writer.WriteElementString("Emo", DataManager.data.Emo.ToString());
                writer.WriteElementString("MrBones", DataManager.data.Jean.ToString());
                writer.WriteElementString("Dere", DataManager.data.Jean.ToString());
                writer.WriteEndElement();//End Characters

                writer.WriteStartElement("Player");

                writer.WriteElementString("Name", DataManager.data.PlayerName);

                writer.WriteEndElement();//End Player

                writer.WriteEndElement();//End Info

                writer.WriteEndDocument();
            }
        }
        //If it does, reset it.
        else
        {
            playerDoc = new XmlDocument();
            playerDoc.LoadXml(File.ReadAllText(savePath));

            foreach (XmlElement node in playerDoc.SelectNodes("//Characters"))
            {
                node.SelectSingleNode("Jean").InnerText = "0";
                node.SelectSingleNode("Emo").InnerText = "0";
                node.SelectSingleNode("MrBones").InnerText = "0";
                node.SelectSingleNode("Dere").InnerText = "0";
            }

            foreach (XmlElement node in playerDoc.SelectNodes("//Player"))
            {
                node.SelectSingleNode("Name").InnerText = nameBox.text;
            }

            playerDoc.Save(savePath);
        }

        namePanel.SetActive(false);

        sh.AdvanceScene();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
