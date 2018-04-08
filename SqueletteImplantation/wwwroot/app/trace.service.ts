import { Trace } from './trace';
import { TraceDTO } from './tracedto';
import { Headers, Http } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/toPromise'; // Pour accéder à la méthode .toPromise()


@Injectable()
export class TraceService 
{
    private TracesURL = 'api/TraceListe/';  // URL de l'API
    private headers = new Headers({ 'Content-Type': 'application/json' }); //Spécifie le type de données voulues

    constructor(private http: Http) { }

    //Envoie une requête d'obtention des Tracés au "controller".
    getTraces(ListeId: string)
    {
        return this.http.get(this.TracesURL + ListeId);
    }

    //Permet d'envoyer une requête de suppression d'un certain Tracé au "controller".
    deleteTrace(id: number)
    {
        const url = `${this.TracesURL}${id}`;
        return this.http.delete(url);
    }

    //Permet d'envoyer une requête HTTP d'ajout de Tracé.
    addTrace(trace: TraceDTO)
    {
      return this.http.post("/api/ajouttrace" , trace);
    } 

}

