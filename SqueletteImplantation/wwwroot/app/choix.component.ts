import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from "./app.component";
import { AuthentificationService } from "./authentification.service";



@Component ({
    selector: 'my-choix',
    templateUrl: 'app/html/choix.component.html',
    styleUrls: [ 'app/css/choix.component.css' ]
})

export class ChoixComponent 
{ 
    constructor(private router: Router, private appcomponent: AppComponent, private authentificationservice: AuthentificationService) {
        this.appcomponent.UpdateAuthentification();
     }

    ngOnInit():void
    {
        this.authentificationservice.InitDomaine();
        this.appcomponent.VerificationActivite();
    }

    NeuroClick(): void 
    {
        this.authentificationservice.DomaineChange();
        this.router.navigateByUrl('/neurologie');
    }

    CardioClick(): void
    {
        this.authentificationservice.DomaineChange();
        this.router.navigateByUrl('/cardiologie');
    }
}