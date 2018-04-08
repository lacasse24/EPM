import { Critere, CritereDTO } from './critere';
import { Headers, Http } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/toPromise'; // Pour accéder à la méthode .toPromise()


@Injectable()
export class CritereService 
{
    private CriteresURL = 'api/criterecat/';  // URL de l'API
    private headers = new Headers({ 'Content-Type': 'application/json' }); //Spécifie le type de données voulues

    constructor(private http: Http) { }

    //Envoie une requête d'obtention des critères au "controller".
    public getCriteres(id: number) 
    {
        return this.http.get(this.CriteresURL + id);
    }

    //Permet d'envoyer une requête de suppression d'un certain critère au "controller".
    public deleteCritere(id: number)
    {
        const url = `api/delcrite/${id}`;
        return this.http.delete(url);
    }

  
    //Permet d'envoyer une requête HTTP d'ajout d'un critère.
    public addCritere(critdto: CritereDTO)
    {
       return this.http.post("api/ajoutcrite", critdto);
    }
}
