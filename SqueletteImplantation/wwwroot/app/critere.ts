export class Critere
{
    constructor(public critId: number,public critNom: String, public catId: number){} 
}
export class CritereDTO
{
    constructor(public NomCrit: String, public IdCat: number){}
}