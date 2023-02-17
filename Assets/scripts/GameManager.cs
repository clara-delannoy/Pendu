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
    public TextMeshProUGUI lettreEssayees;
    public TextMeshProUGUI affichageErreur;
    public TextMeshProUGUI gameOver;
    public Image imgPendu;

    //Lettre entr�e (� tester) par le joueur
    public TMP_InputField input;

    //Liste des images du pendu
    public List<Sprite> tableauImageMoches = new List<Sprite>();
    public List<Sprite> tableauForet = new List<Sprite>();

    //Num�ro de l'image du pendu affich�e selon les erreurs
    public int iterateurErreur = 0;

    //Phases de jeu
    public GameObject ecranDemarrage;
    public GameObject phaseJeu;
    public GameObject ecranFin;

    //Dictionnaire des mots
    private List<string> listeDictionnaire = new List<string>();

    public DownloadText motDL;
    
    void Start()
    {
        affichagePhase(true, false, false, false);
    }

    void Update()
    {
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

        if (!lettreEssayees.text.Contains(input.text))
        {
            lettreEssayees.text += "<color="+ couleur +">" + input.text + "</color> ";
            affichageErreur.text = "";
            Debug.Log(couleur);
            print(couleur);
            if (couleur == "red")
            {
                iterateurErreur += 1;
                Debug.Log(tableauForet[iterateurErreur]);
                imgPendu.sprite = tableauForet[iterateurErreur];
            }
        }
        else
        {
            affichageErreur.text = "Lettre d�j� essay�e !!";
        }
    }

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
        imgPendu.sprite = tableauForet[0];
    }

    public void bouttonTester()
    {
        Debug.Log(input.text);

        if (input.text == "") { }
            //on v�rifie si l'user utilise un caract�re sp�cial
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
                    if (iterateurErreur == 8)
                    {
                        Debug.Log("Game over");
                        gameOver.text = "Perdu !";
                        phaseJeu.SetActive(false);
                        ecranFin.SetActive(true);
                    }
                }
            }
            else
            {
                affichageErreur.text = "Attention n'utilisez pas de caract�res sp�ciaux !";
             }
    }

    private void bouttonResetJeu()
        {
            SceneManager.LoadScene(0);
        }

}
