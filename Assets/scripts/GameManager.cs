using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



// NOTES SUR LE RENDU UNITY 
/* Augmenter distance entre caract�re sur le rendu de "mot" => Spacing options dans TMP Text UI */



public class GameManager : MonoBehaviour
{

    // VARIABLES DE CLASSE

    //Champs affich�s en jeu
    public TextMeshProUGUI mot;
    public TextMeshProUGUI renduMot;
    public TextMeshProUGUI affichage;
    public TextMeshProUGUI lettreEssay�es;
    public TextMeshProUGUI lettreD�j�Essay�e;
    public TextMeshProUGUI gameOver;
    public Image imgPendu;
    //Lettre entr�e (� tester) par le joueur
    public TMP_InputField input;

    //Liste des images du pendu
    public List<Sprite> tableauImage = new List<Sprite>();
    //Num�ro de l'image du pendu affich�e selon les erreurs
    public int it�rateurErreur = 0;

    //Phases de jeu
    public GameObject ecranDemarrage;
    public GameObject phaseJeu;
    public GameObject ecranFin;

    //Dictionnaire des mots
    [SerializeField] private List<string> listeDictionnaire = new List<string>(); /* [SerializeField] private permet � une variable priv�e d'�tre qd mm affich�e dans l'inspector */

    public DownloadText motDL;
    
    void Start()
    {
        affichagePhase(true, false, false, false);
    }


    public void affichagePhase(bool demarrage, bool fin, bool jeu, bool image)
    {
        /*Changement d'affichage des phases de jeu */
        ecranDemarrage.SetActive(demarrage);
        ecranFin.SetActive(fin);
        phaseJeu.SetActive(jeu);
        imgPendu.gameObject.SetActive(image); /* activer le gameobject image */
    }
    


    public void motComplet()
    {
        if (mot.text == renduMot.text)
        {
            Debug.Log("le mot est complet");
            affichage.text = "Bien jou� ! Vous avez trouv� le mot.";
            gameOver.text = "Gagn� !";
            phaseJeu.SetActive(false);
            ecranFin.SetActive(true);
        }

    }

    public void affichageLettre()
    {
        /*afficher la/les lettres correspondantes sur le mot cherch�*/
        string monTexte = renduMot.text;
        for (int i = 0; i < mot.text.Length; i++)
        {
            if (mot.text[i] == input.text[0])
            {
                monTexte = monTexte.Remove(i, 1);
                monTexte = monTexte.Insert(i, input.text);

            }
        }
        renduMot.text = monTexte;
        affichage.text = "Bravo !";
    }

    public void ajouteLettre(string couleur)
    {

        if (!lettreEssay�es.text.Contains(input.text))
        {
            lettreEssay�es.text += "<color="+ couleur +">" + input.text + "</color> ";
            lettreD�j�Essay�e.text = "";
            if (couleur == "red")
            {
                it�rateurErreur += 1;
                Debug.Log(tableauImage[it�rateurErreur]);
                imgPendu.sprite = tableauImage[it�rateurErreur];
            }
        }
        else
        {
            lettreD�j�Essay�e.text = "Lettre d�j� essay�e !!";
        }


    }

    /* public void motRandom()
    {
        mot.text = "";
        renduMot.text = "";
        /* liste des mots du dictionnaire  ==================> appeler le dictionnaire de MYG voir image + lien api 
        listeDictionnaire.Add("ABC");
        listeDictionnaire.Add("DEF");
        listeDictionnaire.Add("GHI");
        listeDictionnaire.Add("XYZ");

        /* g�n�ration d'un mot random � deviner 
        mot.text = listeDictionnaire[Random.Range(0, listeDictionnaire.Count)];

        /* mot anonymis� sous forme de "_" aka renduMot
        do
        {
            renduMot.text += "_";
        }
        while (renduMot.text.Length != mot.text.Length);
    } */

    public void motRandom()
    {
        mot.text = motDL.motChoisi().ToUpper();

        renduMot.text = "";

        // mot anonymis� sous forme de "_" aka renduMot
        do
        {
            renduMot.text += "_";
        }
        while (renduMot.text.Length != mot.text.Length);
    }


    public void bouttonDemarrer()
    {
        affichagePhase(false, false, true, true);
        motRandom();
    }

    public void bouttonTester()
    {
        Debug.Log(input.text);

        if (input.text != "")
        {
            if (mot.text.Contains(input.text))
            {
                Debug.Log("Oui le mot contient la lettre entr�e.");
                affichageLettre();
                ajouteLettre("green");
                motComplet();      

            }
            else
            {
                Debug.Log("Non le mot ne contient pas la lettre entr�e.");
                /*afficher un message type 'bien essay�'*/
                affichage.text = "Bien essay� ! Retentez votre chance !";
                ajouteLettre("red");
   
                /*si image du pendu compl�te alors game over*/
                if (it�rateurErreur == 6)
                {
                    Debug.Log("Game over");
                    gameOver.text = "Perdu !";
                    phaseJeu.SetActive(false);
                    ecranFin.SetActive(true);    
                }

            }


            
        }

    }

    public void bouttonResetJeu()
    {
        SceneManager.LoadScene(0);
    }

    /*
    public void test()
    {
        for (int i = 0; i >= 1; i++)
        {

        }


        int i = 0; 



        do
        {

        }
        while (i == 0); //permet une exec obligatoire sans verif condition

        while (i == 0) //si condition fausse alors non exec
        {

        }




        List<int> liste = new List<int>();
        liste.Add(1);

        foreach (int j in liste)
        {

        }


        int[] arrayDeInt = { 0, 1 };


        DownloadText test = new DownloadText() ; //creation d'une instance locale 

        Constructeur de classe (dans la classe DownloadText)
        public DownloadText(string status, string motChoisi) 
        {
            this.mot.status = status;
            this.mot.motChoisi = motChoisi;
        }
        
    }
    */


}
