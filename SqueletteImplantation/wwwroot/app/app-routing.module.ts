
import { ModuleWithProviders } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


import { PageCatComponent} from './page-cat.component';
import { AjoutAdminComponent} from './page-ajout-admin.component';
import { IndexComponent } from './index.component';
import { ChoixComponent } from './choix.component';
import { ModifProfilComponent } from './page-modif-profil.component';
import { AjoutSuppComponent } from "./ajout-cat-crit.component";
import { mdpcomponent } from './pagemdp.component';
import { AuthentificationGuard } from './authentification.guard';
import { GestionUtilComponent } from './gestionutil.component';



export const router: Routes = 
[

  { path: '', redirectTo: '/choix', pathMatch: 'full' },
  { path: 'MDP', component: mdpcomponent },
  { path: 'ModificationProfil', component: ModifProfilComponent, canActivate: [AuthentificationGuard] },
  { path: 'ModificationProfilUtilisateurs', component: ModifProfilComponent, canActivate: [AuthentificationGuard] },
  { path: 'GestionUtilisateur', component: GestionUtilComponent, canActivate: [AuthentificationGuard]},
  { path: 'cardiologie', component: PageCatComponent, canActivate: [AuthentificationGuard]},
  { path: 'cardiologie/ajouttrace', component: AjoutAdminComponent, canActivate: [AuthentificationGuard]},
  { path: 'cardiologie/ajoutsupp', component: AjoutSuppComponent, canActivate: [AuthentificationGuard]},
  { path: 'neurologie', component: PageCatComponent, canActivate: [AuthentificationGuard]},
  { path: 'neurologie/ajouttrace', component: AjoutAdminComponent, canActivate: [AuthentificationGuard]},
  { path: 'neurologie/ajoutsupp', component: AjoutSuppComponent, canActivate: [AuthentificationGuard]},
  { path: '', redirectTo: '/choix', pathMatch: 'full' },
  { path: 'categorie', component: PageCatComponent, canActivate: [AuthentificationGuard]},
  { path: 'index', component: IndexComponent },
  { path: 'choix', component: ChoixComponent, canActivate: [AuthentificationGuard]},
  { path: 'ajout', component: AjoutAdminComponent, canActivate: [AuthentificationGuard]},
  { path: '**', component: ChoixComponent }  

];
 
export const routes: ModuleWithProviders = RouterModule.forRoot(router);


