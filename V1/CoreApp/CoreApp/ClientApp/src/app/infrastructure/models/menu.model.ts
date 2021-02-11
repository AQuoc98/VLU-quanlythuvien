export interface MenuModel {
    id: string,
    moduleId: string,
    viewId: string,
    titleDict: string,
    href: string,
    icon: string,
    position: number,
    parentId: string,
    childs: MenuModel[]
}

export class MenuModel {
    constructor() {

    }
}