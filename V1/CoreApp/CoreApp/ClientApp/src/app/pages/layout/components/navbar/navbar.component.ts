import { Component, OnInit } from '@angular/core';

import { AuthService } from '@infrastructure/auth';
import { I18nService } from '@core/services';


@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

    currentUser: any = {};
    isShown:boolean=false;

    constructor(public i18nService: I18nService,private authService: AuthService) { }

    setDefaultLang($event: any) {
        if (!$event) {
            this.i18nService.language = 'en-US';
        }
    }
    ngOnInit() {
        this.loadData();
    }

    private loadData() {
        this.currentUser = this.authService.currentUser;
    }
}
