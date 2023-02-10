using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



// NOTES SUR LE RENDU UNITY 
/* Augmenter distance entre caractère sur le rendu de "mot" => Spacing options dans TMP Text UI */



public class GameManager : MonoBehaviour
{

    // VARIABLES DE CLASSE

    //Champs affichés en jeu
    public TextMeshProUGUI mot;
    public TextMeshProUGUI renduMot;
    public TextMeshProUGUI affichage;
    public TextMeshProUGUI lettreEssayées;
    public TextMeshProUGUI lettreDéjàEssayée;
    public TextMeshProUGUI gameOver;
    public Image imgPendu;
    //Lettre entrée (à tester) par le joueur
    public TMP_InputField input;

    //Liste des images du pendu
    public List<Sprite> tableauImage = new List<Sprite>();
    //Numéro de l'image du pendu affichée selon les erreurs
    public int itérateurErreur = 0;

    //Phases de jeu
    public GameObject ecranDemarrage;
    public GameObject phaseJeu;
    public GameObject ecranFin;

    //Dictionnaire des mots
    [SerializeField] private List<string> listeDictionnaire = new List<string>(); /* [SerializeField] private permet à une variable privée d'être qd mm affichée dans l'inspector */

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
            affichage.text = "Bien joué ! Vous avez trouvé le mot.";
            gameOver.text = "Gagné !";
            phaseJeu.SetActive(false);
            ecranFin.SetActive(true);
        }

    }

    public void affichageLettre()
    {
        /*afficher la/les lettres correspondantes sur le mot cherché*/
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

        if (!lettreEssayées.text.Contains(input.text))
        {
            lettreEssayées.text += "<color="+ couleur +">" + input.text + "</color> ";
            lettreDéjàEssayée.text = "";
            if (couleur == "red")
            {
                itérateurErreur += 1;
                Debug.Log(tableauImage[itérateurErreur]);
                imgPendu.sprite = tableauImage[itérateurErreur];
            }
        }
        else
        {
            lettreDéjàEssayée.text = "Lettre déjà essayée !!";
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

        /* génération d'un mot random à deviner 
        mot.text = listeDictionnaire[Random.Range(0, listeDictionnaire.Count)];

        /* mot anonymisé sous forme de "_" aka renduMot
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

        // mot anonymisé sous forme de "_" aka renduMot
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
                Debug.Log("Oui le mot contient la lettre entrée.");
                affichageLettre();
                ajouteLettre("green");
                motComplet();      

            }
            else
            {
                Debug.Log("Non le mot ne contient pas la lettre entrée.");
                /*afficher un message type 'bien essayé'*/
                affichage.text = "Bien essayé ! Retentez votre chance !";
                ajouteLettre("red");
   
                /*si image du pendu complète alors game over*/
                if (itérateurErreur == 6)
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
