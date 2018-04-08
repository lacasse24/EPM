import { Categorie,CategorieDTO } from './categorie';
import { Headers, Http } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/toPromise'; // Pour accéder à la méthode .toPromise()


@Injectable()
export class CategorieService 
{
    private CategoriesURL = 'api/categorie';  // URL de l'API
    private CatDomURL ='api/CategorieDomaine/';
    private headers = new Headers({ 'Content-Type': 'application/json' }); //Spécifie le type de données voulues

    constructor(private http: Http) { }

    //Envoie une requête d'obtention des catégories au "controller".
    getCategories(id: number) 
    {
        return this.http.get(this.CatDomURL + id);
    }

    //Permet d'envoyer une requête de suppression d'une certaine catégorie au "controller".
    deleteCategorie(id: number)
    {
        const url = `api/delcat/${id}`;
        return this.http.delete(url);
    }

    //Permet d'envoyer une requête HTTP d'ajout d'une catégorie'.
    addCategorie(catdto: CategorieDTO)
    {
       return this.http.post("api/ajoutcat", catdto);
    }
}

