import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Utilisateur } from './utilisateur';
import { NgForm } from '@angular/forms';
import { AppComponent } from './app.component';
import { AuthentificationService } from "./authentification.service";
import { UtilisateurService } from "./utilisateur.service";
import { Response } from '@angular/http';

declare var jBox :any;

@Component ({
    selector: 'mdp-oublie',
    templateUrl: 'app/html/pagemdp.component.html',
    styleUrls: [ 'app/css/pagemdp.component.css' ]
})

export class mdpcomponent
{ 
    private courriel: string;

    constructor(private router: Router, private utilisateurService: UtilisateurService){}

    public Cancel(): void
    {
        this.router.navigateByUrl('choix');
    }
    public ResetMDP() {
        console.log(this.courriel);
        this.utilisateurService.reset(this.courriel).subscribe(res => {
            console.log(res);
            if(res.statusText == "OK"){
                new jBox('Notice', {
                    content: 'Mot de passe réinitialisé. Vérifier vos courriels indésirables. Il peut y avoir un délais avant la réception du courriel.',
                    color: 'green',
                    stack: false,
                    offset:{y: 50},
                    autoclose: 2000
                });
                let timeoutId = setTimeout(() => {
                    this.router.navigateByUrl("choix");
                    }, 2000);
            }
            else
            {
                new jBox('Notice',{
                    content: 'Aucun compte trouvé à cette adresse courriel',
                    color: 'red',
                    stack: false,
                    offset:{y: 50},
                    autoclose: 2000
                });
            }
        })
    }
}
