export class Categorie
{
    constructor(public catId: number,public catNom: String, public domId: number, public domaine:String, public criteres:String ){} 
}

export class CategorieDTO
{
    constructor(public NomCat: String, public IdDom: number){}
}