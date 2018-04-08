//Modules
import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { routes } from './app-routing.module';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

//Components
import { AppComponent } from './app.component';
import { IndexComponent } from './index.component';
import { PageCatComponent } from './page-cat.component';
import { ChoixComponent } from './choix.component';
import { AjoutAdminComponent } from './page-ajout-admin.component';
import { AuthentificationGuard } from "./authentification.guard";
import { AuthentificationService } from "./authentification.service";

import { AjoutSuppComponent } from "./ajout-cat-crit.component";
import { mdpcomponent } from "./pagemdp.component";
import { ModifProfilComponent } from "./page-modif-profil.component";
import { UtilisateurService } from "./utilisateur.service";
import { GestionUtilComponent } from "./gestionutil.component";
import { HistoriqueService } from "./Historique.service"; 


@NgModule({
  imports:      [ BrowserModule, routes, HttpModule, FormsModule ], // Importation des modules de l'application
  declarations: [ IndexComponent, PageCatComponent, AppComponent, ChoixComponent, AjoutAdminComponent, mdpcomponent, ModifProfilComponent,  AjoutSuppComponent, GestionUtilComponent ],  // Déclaration des "components" de l'application
  bootstrap:    [ AppComponent ],
  providers: [ AuthentificationGuard, AuthentificationService, UtilisateurService, HistoriqueService ]
})

export class AppModule { }
