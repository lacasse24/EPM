import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthentificationService } from "./authentification.service";
import { HistoriqueService } from "./Historique.service";
import {HistoriqueDTO} from './Historique';


@Component({
  selector: 'app-root',
  templateUrl: 'app/html/app.component.html', //template de base va ici
  styleUrls: ['app/css/app.component.css']
})

export class AppComponent 
{

  private IDIntervaleActivite : number;
  private IDVerification : number;
  private TempsDeVerifierActivite : boolean = false;
  private infostelechargement: HistoriqueDTO;

  constructor (
    private router: Router,
    private authentificationService: AuthentificationService,
    private historiqueService:HistoriqueService){  }

    
  public UpdateAuthentificationPageIndex(): void {
    localStorage.removeItem('ConnectedUser');
    this.authentificationService.logout();
  }
  public UpdateAuthentification(): void {
    this.authentificationService.Connecte();
    this.authentificationService.Admin();
    this.authentificationService.Domaine();
  }

  public ChoixDomaine(): void {
    this.authentificationService.DomaineChange();
  }

  

  Deconnexion(Raison : Number){
    this.authentificationService.logout();
    this.router.navigateByUrl('index');

    if(Raison == 1)
    {
      alert("Votre session a été fermée à cause de votre inactivité");
    }
  }


  Reroutage(type: Number): void 
  {
    
    if (type === 0 && this.DetectionPage() == 'neurologie') //tu sais déjà qu'il est admin
    {
      this.router.navigateByUrl('neurologie/ajouttrace');
    }
    else
      if (type === 0 && this.DetectionPage() === 'cardiologie') {
        this.router.navigateByUrl('cardiologie/ajouttrace');
      }
      else
        if (type === 1 && this.DetectionPage() == 'neurologie') {
          this.router.navigateByUrl('neurologie/ajoutsupp');
        }
        else
          if (type === 1 && this.DetectionPage() == 'cardiologie') {
            this.router.navigateByUrl('cardiologie/ajoutsupp');
          }
          else
            if (type === 2) {
              this.router.navigateByUrl('choix');
            }
            else
                if(type === 3)
                {
                    this.router.navigateByUrl('ModificationProfil');
                }
                else
                  if(type === 4)
                  {
                    this.router.navigateByUrl('GestionUtilisateur');
                  }
  }


  DetectionPage(): string // Pour savoir si on est dans la catégorie cardio ou neuro
  {
    let CheminLong: string = this.router.url.toString();
    let Page: string[];

    Page = CheminLong.split('/', 2);

    return Page[1];
  }

  DetectionActivite() : void
  { 

    if(this.authentificationService.Connecte() === true)
    {
      if(this.IDIntervaleActivite != null)
      {
        window.clearTimeout(this.IDIntervaleActivite);
      }

      this.IDIntervaleActivite = window.setTimeout(() => this.Deconnexion(1), 900000);//Bon temps = 900000
    }
  }

  VerificationActivite() : void
  {
    if(this.authentificationService.Connecte() === true)
    {
      if(this.IDVerification == null)
      {
        this.IDVerification = window.setInterval(() => this.VerificationActivite(), 30000);
      }
      else
      {
        this.TempsDeVerifierActivite = true;
      }
    }
    else
    {
      if(this.IDVerification != null)
      {
        window.clearInterval(this.IDVerification);
        window.clearTimeout(this.IDIntervaleActivite);
        this.IDIntervaleActivite = null;
        this.IDVerification = null;

      }
    }
  }

  
  MouvementSouris(event: MouseEvent) : void
  {
    if(this.TempsDeVerifierActivite == true)
    {
      this.TempsDeVerifierActivite = false;
      this.DetectionActivite();
    }
  }

  onClickTelecharger(id: number)
    {       
        this.infostelechargement=new HistoriqueDTO(id,this.historiqueService.IdUsager);
        
        console.log(this.infostelechargement);

        this.historiqueService.addRechercheRecente(this.infostelechargement).subscribe(Reponse=> this.historiqueService.ObtenirHistorique());
    }
}

