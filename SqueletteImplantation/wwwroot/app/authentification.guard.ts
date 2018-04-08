import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

import { AuthentificationService } from './authentification.service';

@Injectable()
export class AuthentificationGuard implements CanActivate {

    constructor(
        private authentificationService: AuthentificationService,
        private router: Router) {}

    canActivate() {

        if(this.authentificationService.Connecte()) {
            return true;
        }
        this.router.navigate(['index']);
    }
}
