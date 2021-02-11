import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Subject } from 'rxjs';
import { TranslateService } from '@ngx-translate/core';

import { HeaderInfoModel } from '../../infrastructure/models';

@Injectable()
export class HeaderService {
    updatedEvent$: Subject<any>;
    private headerInfo: HeaderInfoModel;

    constructor(private titleService: Title, private translateService: TranslateService, ) {
        this.updatedEvent$ = new Subject();
    }

    setHeaderInfo(headerInfo: HeaderInfoModel) {
        this.headerInfo = headerInfo;
        // Set browser title
        this.titleService.setTitle(`${this.translateService.instant(this.headerInfo.subtitle)} - ${this.translateService.instant(this.headerInfo.title)}`);

        this.updatedEvent$.next();
    }

    getHeaderInfo(): HeaderInfoModel {
        return this.headerInfo;
    }
}
