import { Component, OnInit } from '@angular/core';

import { Logger } from '../../../core';
import { forkJoin } from 'rxjs';
import { AuthService } from '@infrastructure/auth';
import { Constants } from '@core/constants';
import { ModalService } from '@core/services';

export const logger = new Logger('Dashboard');

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
    currentDate: Date = new Date();
    dataModel: any = {};
    messages: any = []
    carriageWays: any = [];
    carriageWayId: any = null;
    isHideDashboardBus: any = true;
    countCarriageWayGoing: number = 0 ;

    constructor(private modalService: ModalService, private authService: AuthService) { }

    ngOnInit() {
        this.loadData();
        this.loadRefData();
    }

    reload() {
        this.currentDate = new Date();
        this.loadData();
    }

    private loadData() {

    }

    private loadRefData() {

    }


}
