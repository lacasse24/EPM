/*
À faire :
S'entendre sur ce que le controller et le service vont faire
Modifier l'interface en fonction
Faire un ajout qui marche
*/


import { Component } from '@angular/core';
import {Router} from '@angular/router';
import { Http } from '@angular/http';
import {AppComponent} from './app.component';

//Importation des services
import { CategorieService } from './categorie.service';
import { CritereService } from './critere.service';

//Importation des composants
import { Categorie, CategorieDTO } from './categorie';
import { Critere, CritereDTO } from './critere';

declare var jBox:any;

@Component ({
    selector: 'my-ajoutsupp',
    templateUrl: 'app/html/ajout-cat-crit.component.html',
    styleUrls: [ 'app/css/ajout-cat-crit.component.css'],
    providers: [CritereService,CategorieService, AppComponent]
})

export class AjoutSuppComponent
{ 
    m_TabCat: Categorie[];
    m_TabCrit: Critere[];
    m_CatID: number;
    m_CritID: number;
    NomCateg: String;
    NomCateg2: String;
    NomCrit: String;
    ListeCrit: String;
    NomAjoutCat: String;
    NomAjoutCrit: String;
    TypeDom:number;

    constructor(private catService: CategorieService, private critService: CritereService, private http:Http, private router:Router, private AppComp:AppComponent)
    {
        this.ActualisationListes();
    }

    ngOnInit(): void 
    {
        if(this.router.url.toString() == "/neurologie/ajoutsupp" )
        {
           this.catService.getCategories(2).subscribe(cat => this.AffichageCat(cat,1));
           this.catService.getCategories(2).subscribe(cat => this.AffichageCat(cat,2));
           this.TypeDom = 2;
           document.getElementById("btnNeuro").style.background='#008cba';
        
        }


        if(this.router.url.toString() == "/cardiologie/ajoutsupp")
        {
            this.catService.getCategories(1).subscribe(cat => this.AffichageCat(cat,1));
            this.catService.getCategories(1).subscribe(cat => this.AffichageCat(cat,2));
            this.TypeDom = 1;
            document.getElementById("btnCardio").style.background='#008cba';

        }
    }

    private AffichageCat(param: any,idListe: number) 
    {
        this.m_TabCat = (param.json() as Categorie[]);

        if(this.m_TabCat.length < 8)
        {
            if(idListe === 1)
            {
                document.getElementsByClassName("ListeCategorie")[0].setAttribute("size", this.m_TabCat.length.toString());  
            }
            else
            {
                document.getElementsByClassName("ListeCategorie2")[0].setAttribute("size", this.m_TabCat.length.toString());  
            }
            
        }
        else
        {
            if(idListe === 1)
            {
                document.getElementsByClassName("ListeCategorie")[0].setAttribute("size", "8");          
            }  
            else
            {
                document.getElementsByClassName("ListeCategorie2")[0].setAttribute("size", "8");     
            }
        }
    }

    private AffichageCrit(param: any) 
    {
        this.m_TabCrit = (param.json() as Critere[]);
        this.ListeCrit = "";

        for(var i = 0; i < this.m_TabCrit.length; i++)
        {
            this.ListeCrit += "\n\r" + this.m_TabCrit[i].critNom + ", " ;
        }

        if(this.m_TabCrit.length < 8)
        {
            if(this.m_TabCrit.length > 1)
            {
              document.getElementsByClassName("ListeCritere")[0].setAttribute("size", this.m_TabCrit.length.toString());
            }
            else
            {
                 document.getElementsByClassName("ListeCritere")[0].setAttribute("size", "2");
            }
        
        }
        else
        {
            document.getElementsByClassName("ListeCritere")[0].setAttribute("size", "8");
        }

    }

    OnClickListeDeroulanteCritere()
    {
	    document.getElementsByClassName("ListeCritere")[0].classList.toggle("ShowElement");
    }
	
    OnClickListeDeroulanteCategorie()
    {
	    document.getElementsByClassName("ListeCategorie")[0].classList.toggle("ShowElement");
    }

    OnClickListeDeroulanteCategorie2()
    {
	    document.getElementsByClassName("ListeCategorie2")[0].classList.toggle("ShowElement");
    }

     //Action lors de la sélection d'une catégorie
     OnClickCategorie(categ: Categorie, IdListe : number)
     {
         if(IdListe === 1) //Savoir sur laquelle des deux listes on veut effectuer une modification
         {
            this.NomCateg = categ.catNom;
         }
         else
         {
            this.NomCateg2 = categ.catNom;
         }
         
         this.m_CatID = categ.catId;
         this.NomCrit = "Critères";
         this.critService.getCriteres(categ.catId).subscribe(crit => this.AffichageCrit(crit));
     } 
 
     //Action lors de la sélection d'un critère
     OnClickCritere(crit: Critere)
     {
         this.NomCrit = crit.critNom;
         this.m_CritID = crit.critId;
     }

     OnClickSupprimerCateg()
     {
       if(this.NomCateg != "Catégories")
       {
        if(confirm("Voulez-vous vraiment supprimer définitivement la catégorie suivante : " + this.NomCateg  + " ? \n\rLa suppression de cette catégorie entrainera la suppression des critère(s) suivant(s) : " + this.ListeCrit))
            {
               
               //Effacement des critères associés à ladite catégorie
               for(var i = 0; i < this.m_TabCrit.length; i++) 
               {
                     this.critService.deleteCritere(this.m_TabCrit[i].critId).subscribe(reponse => this.ActualisationListeSuppCrit(reponse));
               }

               this.catService.deleteCategorie(this.m_CatID).subscribe(reponse => this.ActualisationListeSuppCat(reponse));
            }
       }
       else
       {
            new jBox('Notice', {
                content: 'Veuillez choisir une catégorie afin de pouvoir la supprimer',
                color: 'red',
                offset:{y: 50},
                stack: true
            });
       }

     }

     OnClickSupprimerCrit()
     {
        if(this.NomCrit != "Critères") 
        {
            if(confirm("Voulez-vous vraiment supprimer définitivement le critère suivant :" + this.NomCrit  + " ?"))
                {
                    this.critService.deleteCritere(this.m_CritID).subscribe(reponse => this.ActualisationListeSuppCrit(reponse));
                }
        }
        else
        {
            new jBox('Notice', {
                content: 'Veuillez choisir un critère afin de pouvoir le supprimer',
                color: 'red',
                offset:{y: 50},
                stack: true
            });
        }
     }

     OnClickAjoutCategorie()
     {
        if(this.NomAjoutCat != "" && this.NomAjoutCat != null)
        {
            let catdto = new CategorieDTO(this.NomAjoutCat,this.TypeDom);
            this.catService.addCategorie(catdto).subscribe(reponse => this.ActualisationListeAddCat(reponse));
        }
        else
        {
            new jBox('Notice', {
                content: 'Veuiller donner un nom à la catégorie à ajouter',
                color: 'red',
                offset:{y: 50},
                stack: true
            });
        }
     }

     OnClickAjoutCritere()
     {
         if(this.m_CatID != null)
         {
             if(this.NomAjoutCrit != null && this.NomAjoutCrit != "")
             {
                let critdto = new CritereDTO(this.NomAjoutCrit,this.m_CatID);
                this.critService.addCritere(critdto).subscribe(reponse => this.ActualisationListeAddCrit(reponse));
             }
             else
             {
                new jBox('Notice', {
                    content: 'Veuillez donner un nom à la catégorie à ajouter',
                    color: 'red',
                    offset:{y: 50},
                    stack: true
                }); 
             }
            
         }
         else
         {
            new jBox('Notice', {
                content: 'Veuillez choisir une catégorie afin de pouvoir ajouter un critère à celle-ci',
                color: 'red',
                offset:{y: 50},
                stack: true
            });
         }
     }

     private ActualisationListeAddCat(param: any)
     {
        if(param.status == 200)
        {
            new jBox('Notice', {
                content: 'Catégorie ajoutée avec succès',
                color: 'green',
                offset:{y: 50},
                stack: true
            });
        }
        this.catService.getCategories(this.TypeDom).subscribe(cat => this.AffichageCat(cat,1));
        this.ActualisationListes();
     }

     private ActualisationListeSuppCat(param: any)
     {
        if(param.status == 200)
        {
            new jBox('Notice', {
                content: 'Catégorie supprimée avec succès',
                color: 'green',
                offset:{y: 50},
                stack: true
            });
        }
         this.catService.getCategories(this.TypeDom).subscribe(cat => this.AffichageCat(cat,1));
         this.ActualisationListes();
     }

     private ActualisationListeAddCrit(param: any)
     {
        if(param.status == 200)
        {
            new jBox('Notice', {
                content: 'Critère ajouté avec succès',
                color: 'green',
                offset:{y: 50},
                stack: true
            });
        }
         this.critService.getCriteres(this.m_CatID).subscribe(rep => this.AffichageCrit(rep));
         this.NomCrit = "Critères";
     }

     private ActualisationListeSuppCrit(param: any)
     {
        if(param.status == 200)
        {
            new jBox('Notice', {
                content: 'Critère supprimé avec succès',
                color: 'green',
                offset:{y: 50},
                stack: true
            });
        }
         this.critService.getCriteres(this.m_CatID).subscribe(rep => this.AffichageCrit(rep));
         this.NomCrit = "Critères";
     }

     OnClickDomCardio()
     {
         
         document.getElementById("btnCardio").style.background='#008cba';
         document.getElementById("btnNeuro").style.background='#FFFFFF';

         this.ActualisationListes();
         this.catService.getCategories(1).subscribe(cat => this.AffichageCat(cat,1));
         
         
         this.TypeDom = 1;
     }

     OnClickDomNeuro()
     {
         document.getElementById("btnCardio").style.background='#FFFFFF';
         document.getElementById("btnNeuro").style.background='#008cba';

         this.ActualisationListes();
         this.catService.getCategories(2).subscribe(cat => this.AffichageCat(cat,1));
         
         
         this.TypeDom = 2;
     }

     ActualisationListes()
     {
         this.NomCateg = "Catégories";
         this.NomCrit = "Critères"
         this.NomCateg2 = "Catégories";
     }

     
}