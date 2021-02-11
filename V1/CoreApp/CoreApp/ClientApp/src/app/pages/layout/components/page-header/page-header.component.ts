import { Component, OnInit } from '@angular/core';

import { HeaderInfoModel } from '@infrastructure/models';
import { HeaderService } from '@core/services';


@Component({
    selector: 'app-page-header',
    templateUrl: './page-header.component.html',
    styleUrls: ['./page-header.component.scss']
})
export class PageHeaderComponent implements OnInit {
    headerInfo: HeaderInfoModel = new HeaderInfoModel();

    constructor(private headerService: HeaderService) {

    }

    ngOnInit(): void {
        this.updateHeader();
        this.listenEvent();
    }

    private listenEvent() {
        this.headerService.updatedEvent$.subscribe(() => {
            this.updateHeader();
        });
    }

    private updateHeader() {
        this.headerInfo = this.headerService.getHeaderInfo();
    }
}