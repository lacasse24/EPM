import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { HistoriqueService } from "./Historique.service";



@Injectable()
export class AuthentificationService {
    private estConnecte : boolean;
    private estAdmin: boolean;
    private DomaineChoisi: boolean;
    private UtilisateurURL = 'api/utilisateur/login';

    constructor(
        private http: Http,
        private historiqueService:HistoriqueService)
        {
            if(localStorage.getItem("ConnectedUser"))
            {
                let currentUser = JSON.parse(localStorage.getItem("ConnectedUser"));
                this.estConnecte = currentUser.connect;
                this.estAdmin = currentUser.type;
                this.historiqueService.SetUsager();
                this.historiqueService.ObtenirHistorique();
                let DomChoix = JSON.parse(localStorage.getItem('DomaineChoisi'))
                this.DomaineChoisi = DomChoix.domaine;
            }
            else
            {
                this.estAdmin = false;
                this.estConnecte = false;
                this.DomaineChoisi = false;
            }
        }

        


    public login(user: string, motdepasse: string) {      
        let headers = new Headers();

        headers.append('Content-type', 'application/json');
        return this.http.post(this.UtilisateurURL, JSON.stringify({ "UtilPWD": motdepasse, "UtilUserName": user }), { headers });        
    }

    public ValidationConnexion(Valide: any): any
    {
        console.log(Valide);
        let tRetour : number[] = new Array();


        if (Valide.status === 200 && Valide._body != null)
        {
            this.estConnecte = true;
            
            tRetour = this.StringEnTableauNombre(Valide._body);

            if (tRetour[0] != 0)
            {
                this.estAdmin = false;
            }
            else
            {
                this.estAdmin = true;
            }
        }
        else
        {
            this.estConnecte = false;
            this.estAdmin = false;
        }

        localStorage.setItem('ConnectedUser', JSON.stringify({ type: this.estAdmin, connect: this.estConnecte, IdUtil: tRetour[1] }));
        localStorage.setItem('DomaineChoisi',JSON.stringify({domaine: this.DomaineChoisi}));
    }

    public logout(): void {
        localStorage.removeItem('ConnectedUser');
        this.estConnecte = false;
        this.estAdmin = false;
        localStorage.removeItem('ModifType');
        localStorage.removeItem('DomaineChoisi');
        localStorage.removeItem('Username');
    }


    private StringEnTableauNombre(ElementAConvertir: string) : number[]
    {
        let Chaine = ElementAConvertir.slice(1, ElementAConvertir.length - 1);
        let tElementString = Chaine.split(",");
        let tElementNombre : number[] = new Array(tElementString.length);

        for(let Indice = 0; Indice < tElementString.length; Indice++)
        {
            tElementNombre[Indice] = Number(tElementString[Indice]);
        }

        return tElementNombre;
    }

    

    public DomaineChange(): void
    {
        this.DomaineChoisi = true;
        localStorage.setItem('DomaineChoisi',JSON.stringify({domaine: this.DomaineChoisi}));
    }

    public InitDomaine(): void
    {
        this.DomaineChoisi = false;
        localStorage.setItem('DomaineChoisi',JSON.stringify({domaine: this.DomaineChoisi}));
    }

    public Domaine()
    {
        return this.DomaineChoisi;
    }

    public Connecte()
    {
        
        return this.estConnecte;
    }

    public Admin()
    {
        return this.estAdmin;
    }
}
