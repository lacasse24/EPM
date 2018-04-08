import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';

// Importation des classes
import { Trace } from './trace';
import { Categorie } from './categorie';
import { Critere } from './critere';
import { TraceDTO } from './tracedto';
import {HistoriqueDTO} from './Historique';

// Importation des services 
import { TraceService } from './trace.service';
import { CategorieService } from './categorie.service';
import { CritereService } from './critere.service';
import { AuthentificationService } from './authentification.service';
import { HistoriqueService } from './Historique.service';


declare var jBox:any;


@Component({
  selector: 'page-cat',
  templateUrl: 'app/html/page-cat.component.html',
  styleUrls: [ 'app/css/page-cat.component.css' ],
  providers: [TraceService, CritereService, CategorieService]
})

export class PageCatComponent implements OnInit
{
/*  Define a traces array property.
    Inject the TraceService in the constructor and hold it in a private TraceService field.
    Call the service to get traces inside the Angular ngOnInit() lifecycle hook.
*/
    m_TabTrace: Trace[];
    m_TabCat: Categorie[];
    m_TabCrit: Critere[];
    m_TabRecherche: Critere[] = [];
    m_EnvoieTrace: TraceDTO = null;
    NomCateg: String;
    NomCrit: String;
    CategorieSelectionne: String;
    infostelechargement: HistoriqueDTO;


    constructor(private traceService: TraceService, private catService: CategorieService, private critService: CritereService,
    private router: Router, private authentificationService: AuthentificationService, private historiqueService: HistoriqueService)
    {
        this.NomCateg = 'Catégories';
        this.NomCrit = 'Critères';
    }

    // ngOnInit est une méthode du "Framework"" Angular qui est appelée après le constructeur dudit composant.
    ngOnInit(): void
    {
        // Remplit les objets avec les données de la BD
        if(this.router.url.toString() === '/neurologie')
        {
            this.catService.getCategories(2).subscribe(cat => this.m_TabCat = cat.json() as Categorie[]);
        }
        else
        {
            this.catService.getCategories(1).subscribe(cat => this.m_TabCat = cat.json() as Categorie[]);
        }
    }



    private AffichageTrace(param: any)
    {
        this.m_TabTrace = (param.json() as Trace[]);
    }

    OnClickListeDeroulanteCritere(param: any)
    {
        
        if(document.getElementById("ListeCritere").style.display === "" || document.getElementById("ListeCritere").style.display === "none" || param !== this.CategorieSelectionne)
        {
            document.getElementById("ListeCritere").style.display = "inline-block";
            this.CategorieSelectionne = param;
        }
        else
        {
            document.getElementById("ListeCritere").style.display = "none";
        }
    }
	
    
    // Action lors de la sélection d'une catégorie
    OnClickCategorie(categ: Categorie)
    {
        
        for(var i=0; i<this.m_TabCat.length; i++)
        {
            document.getElementById(this.m_TabCat[i].catId.toString()).style.backgroundColor = "rgba(125, 141, 163, 0.71)";
        }
        
        
        document.getElementById(categ.catId.toString()).style.backgroundColor="rgba(43, 47, 61, 0.71)";

        let offsTop = document.getElementById(categ.catId.toString()).offsetTop;
        document.getElementById("ListeCritere").style.top = offsTop +"px";

        let offsLargeur = document.getElementById("EspaceCritereChoisi").offsetWidth;
        document.getElementById("ListeCritere").style.maxWidth = offsLargeur + "px";

        let offsDroite = document.getElementById("ChoixCategorie").offsetWidth;
        document.getElementById("ListeCritere").style.left = offsDroite - 5 + "px";
        

        this.NomCateg = categ.catNom;
        this.NomCrit = "Critères";
        this.critService.getCriteres(categ.catId).subscribe(crit => this.m_TabCrit = crit.json() as Critere[]);
    }

    // Action lors de la sélection d'un critère
    OnClickCritere(crit: Critere)
    {
        this.NomCrit = crit.critNom;
        this.m_TabRecherche.push(crit);
    }

    // Action lors du clic sur supprimer
    OnClickSupprimer(crit: Critere)
    {
        this.m_TabRecherche.splice(this.m_TabRecherche.indexOf(crit), 1);
    }
    
    onClickImg(url: string)
    {
        window.open(url);
    }

    ValidationPage(): boolean
    {
      let CheminLong: string = this.router.url.toString();
      let Page: string[];

      Page = CheminLong.split('/', 2);
     
      if(Page[1] === 'neurologie')
      {
        return false;
      }
      return true;
    }

    // Action lors de l'appui sur le bouton recherche
    OnClickRechercher()
    {
        let RequeteId: string;
        
        RequeteId = "?";

        for (let crit of this.m_TabRecherche)
        {
            RequeteId += "Id=" + crit.critId + "&";
        }
        RequeteId = RequeteId.substr(0, RequeteId.length - 1);

        this.traceService.getTraces(RequeteId).subscribe(trac => this.AffichageTrace(trac));
    }

    onClickTelecharger(id: number)
    {
        this.infostelechargement = new HistoriqueDTO(id, this.historiqueService.IdUsager);

        console.log(this.infostelechargement);

        this.historiqueService.addRechercheRecente(this.infostelechargement).subscribe(Reponse=> this.historiqueService.ObtenirHistorique());
    }


    /***************************************************************/
    ValidationUtil(): boolean
    {
        return this.authentificationService.Admin();
    }

    /**********AJOUT ET SUPPRESSION DE TRACÉS*********************/
    public onClickDeleteTrace(id: number)
    {
        if(confirm("Voulez-vous vraiment supprimer définitivement le tracé #" + id  + "?"))
         {
            this.traceService.deleteTrace(id).subscribe(reponse => 
                {
                    this.AffichageRepDel(reponse);
                    this.OnClickRechercher();
                });
            
         }
         else
         {
             console.log("ABORT");
         }

    }

    public onClickAddTrace()
    {
        if(this.router.url.toString() === '/neurologie')
        {
            this.router.navigateByUrl('/neurologie/ajouttrace');
        }
        else
        {
             this.router.navigateByUrl('/cardiologie/ajouttrace');
        }
    }

    private AffichageRepDel(param: any) // Si le param est une string !
    {
        console.log(param);
    }

}