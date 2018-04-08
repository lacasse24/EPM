import { Component } from "@angular/core";
import { NgForm } from '@angular/forms';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Utilisateur } from "./utilisateur";
import { UtilisateurService } from "./utilisateur.service";
import { AuthentificationService } from "./authentification.service";
import { Router } from '@angular/router';

declare var jBox: any;

@Component ({
    selector: 'gestion-util',
    templateUrl: 'app/html/gestionutil.component.html',
    styleUrls: [ 'app/css/gestionutil.component.css' ]
})

export class GestionUtilComponent 
{
    m_TabUtil: Utilisateur[];

    constructor(private http: Http, private utilisateurService: UtilisateurService, private router: Router, private authentificationservice: AuthentificationService) 
    { 
        this.utilisateurService.getUtils().subscribe(retour => this.AfficheUtils(retour));
        this.authentificationservice.InitDomaine();
    }
   
    public AjoutUtilisateur(f: NgForm): void
    {
        var Util: Utilisateur = new Utilisateur(null, f.value.prenom as string, f.value.nom as string, null, f.value.utilisateur as string, f.value.courriel as string, null);
        
        var droitutil: number; 
        if(f.value.admin === true)
        {
            Util.utiltype = 0;
        }
        else
        {
            Util.utiltype = 1;
        }
        this.utilisateurService.CreationUtil(Util).subscribe(Reponse => {
            new jBox('Notice', {
                content: 'Utilisateur créé avec succès.',
                color: 'green',
                offset:{y: 50},
                autoclose: 2000,
                stack: false
            });
            this.utilisateurService.getUtils().subscribe(retour => this.AfficheUtils(retour));
        });
    }

    public AfficheUtils(param: any)
    {
        this.m_TabUtil = (param.json() as Utilisateur[]);
    }

    public onClickDeleteUtil(id: number)
    {
        if(confirm("Voulez-vous vraiment supprimer définitivement l'utilisateur' #" + id  + "?"))
         {
            this.utilisateurService.deleteUtil(id).subscribe(reponse => 
                {
                    this.utilisateurService.getUtils().subscribe(retour => this.AfficheUtils(retour));
                });
         }
    }

    public onClickModifUtil(utilId:number, utiltype:number, utilUsername:string)
    {
        localStorage.setItem('ModifUser', JSON.stringify(utilId));
        localStorage.setItem('Username', JSON.stringify(utilUsername));
        localStorage.setItem('ModifType', JSON.stringify(utiltype));
        this.router.navigateByUrl('ModificationProfilUtilisateurs');
    }
}