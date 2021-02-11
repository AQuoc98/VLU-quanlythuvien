export interface DataModel {

}

export class DataModel {
    constructor() { }

    assign(...source: any[]) {
        source.forEach(obj => {
            Object.assign(this, obj);
        })
    }
}