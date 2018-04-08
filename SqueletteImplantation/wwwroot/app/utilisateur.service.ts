
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { Injectable } from "@angular/core";
import { Utilisateur } from './utilisateur';


@Injectable()
export class UtilisateurService {

    baseUrl: string = '';
    idUtil: number;

    constructor(private http: Http) { }

    public reset(email: string) {
        this.baseUrl = "api/utilisateur/reset/";
        let headers = new Headers();
        headers.append('Content-type', 'application/json');
        
        return this.http.post(this.baseUrl + email, JSON.stringify({ email }), { headers });
    }

   


    public ModifierNomUtilisateur(NouveauNomUtilisateur : string, isUtilModif :boolean)
    {
        let headers = new Headers();
        headers.append('Content-type', 'application/json');

        let URL = "api/utilisateur/modifiernomutil/";
        let CurrentUser;

        if(isUtilModif  != true)
        {
            CurrentUser = JSON.parse(localStorage.getItem('ModifUser'));
            return this.http.patch(URL, JSON.stringify({"UtilId": CurrentUser, "UtilUserName": NouveauNomUtilisateur}), { headers });
            
        }
        
      
        CurrentUser = JSON.parse(localStorage.getItem("ConnectedUser"));
        
        return this.http.patch(URL, JSON.stringify({"UtilId": CurrentUser.IdUtil, "UtilUserName": NouveauNomUtilisateur}), { headers });
    }

    public ModifierEmail(NouveauEmail : string, isUtilModif :boolean)
    {
        let headers = new Headers();
        headers.append('Content-type', 'application/json');

        let URL = "api/utilisateur/modifieremail/";
        let CurrentUser;

        if(isUtilModif != true)
        {
           CurrentUser = JSON.parse(localStorage.getItem("ModifUser"));

           return this.http.patch(URL, JSON.stringify({"UtilId": CurrentUser, "UtilEmail": NouveauEmail}), { headers });
        }

        CurrentUser = JSON.parse(localStorage.getItem("ConnectedUser"));

        return this.http.patch(URL, JSON.stringify({"UtilId": CurrentUser.IdUtil, "UtilEmail": NouveauEmail}), { headers });
    }

    public ModifierMotDePasse(NouveauMotDePasse : string, isUtilModif :boolean)
    {
        let headers = new Headers();
        headers.append('Content-type', 'application/json');

        let URL = "api/utilisateur/modifiermotdepasse/";
        let CurrentUser;

        if(isUtilModif != true)
        {
           CurrentUser = JSON.parse(localStorage.getItem("ModifUser"));
           
           return this.http.patch(URL, JSON.stringify({"UtilId": CurrentUser, "UtilPWD": NouveauMotDePasse}), { headers });
        }

        CurrentUser = JSON.parse(localStorage.getItem("ConnectedUser"));

        return this.http.patch(URL, JSON.stringify({"UtilId": CurrentUser.IdUtil, "UtilPWD": NouveauMotDePasse}), { headers });
    }

    public ModifierDroitUser(NouveauDroit : boolean, isUtilModif :boolean)
    {
        let headers = new Headers();
        headers.append('Content-type', 'application/json');

        let URL = "api/utilisateur/modifierdroituser/";
        let CurrentUser;
        let NumDroit;

        if(NouveauDroit === true)
        {
            NumDroit = 0;
        }
        else
        {
            NumDroit = 1;
        }

        if(isUtilModif != true)
        {
           CurrentUser = JSON.parse(localStorage.getItem("ModifUser"));

           return this.http.patch(URL, JSON.stringify({"UtilId": CurrentUser, "UtilType": NumDroit}), { headers });
        }
    }


   
    public getUtils()
    {
        let URL = "api/utilisateur/liste/";
        return this.http.get(URL);
    }

    public deleteUtil(id: number)
    {
        const url = `api/delutil/${id}`;
        return this.http.delete(url);
    }

    public CreationUtil(nouveauutilisateur: Utilisateur)
    {
        this.baseUrl = "api/utilisateur/creationutilisateur";
        let headers = new Headers();
        headers.append('Content-type', 'application/json');

        return this.http.post(this.baseUrl, JSON.stringify({ "utilpren": nouveauutilisateur.utilpren, "utilnom": nouveauutilisateur.utilnom,
        "utilusername":nouveauutilisateur.utilusername, "utilemail":nouveauutilisateur.utilemail, "utiltype":nouveauutilisateur.utiltype }), { headers }); 
    }
}
