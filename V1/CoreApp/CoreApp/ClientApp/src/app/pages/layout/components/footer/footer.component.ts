import { Component, OnInit } from '@angular/core';

import { I18nService } from '@core/services';


@Component({
    selector: 'app-footer',
    templateUrl: './footer.component.html',
    styleUrls: ['./footer.component.scss']
})
export class FooterComponent {
    languages: any = [{ "name": "Tiếng Việt", "value": "vi-VN" }, { "name": "English", "value": "en-US" }];
    constructor(public i18nService: I18nService) { }

    setDefaultLang($event: any) {
        if (!$event) {
            this.i18nService.language = 'en-US';
        }
    }
}