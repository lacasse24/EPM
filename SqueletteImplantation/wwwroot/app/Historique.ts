export class Historique
{
    constructor(public TracId: number,public UtilId: number,public DateTelechargement:Date){} 
}

export class HistoriqueDTO
{
    constructor(public TracId: number, public UtilId: number){}
}