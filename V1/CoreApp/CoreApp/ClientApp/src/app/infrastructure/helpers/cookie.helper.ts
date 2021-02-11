import { Injectable } from '@angular/core';

@Injectable()
export class CookieHelper {

    constructor() { }

    getCookie(name: string, cookie: string) {
        let ca: Array<string> = cookie.split(';');
        let caLen: number = ca.length;
        let cookieName = `${name}=`;
        let c: string;

        for (let i: number = 0; i < caLen; i += 1) {
            c = ca[i].replace(/^\s+/g, '');
            if (c.indexOf(cookieName) == 0) {
                return c.substring(cookieName.length, c.length);
            }
        }
        return '';
    }
}