import { Component, OnInit, ViewChild } from '@angular/core';
import * as _ from 'lodash';

import { Guid } from '@infrastructure/common';
import { GridViewComponent } from '@shared/components';
import { Logger } from '@core/logger';
import { CoreMenuApiService } from '@infrastructure/api-services';
import { RouteService } from '@core/services';

export const logger = new Logger('Menu');

@Component({
    selector: 'app-menu',
    templateUrl: './menu.component.html',
    styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
    //#region Fields
    @ViewChild(GridViewComponent)
    private gridViewComponent: GridViewComponent;
    //#endregion

    constructor(private routeService: RouteService, private coreMenuApiService: CoreMenuApiService) {

    }

    //#region Page Cycles Functions
    ngOnInit() {

    }
    //#endregion

    //#region Public Functions
    /**
     * Add event from Grid view
     */
    addEvent() {
        this.routeService.navigate(['/admin/menu/add', Guid.empty], { replaceUrl: true });
    }

    /**
     * Edit event from Grid view
     * @param recordId 
     */
    editEvent(recordId) {
        this.routeService.navigate(['/admin/menu/add', recordId], { replaceUrl: true });
    }

    /**
     * Delete event from Grid view
     * @param recordIds 
     */
    deleteEvent(recordIds) {
        const entities = _.map(recordIds, (recordId) => {
            return { id: recordId };
        });
        this.coreMenuApiService.delete(entities).subscribe((res) => {
            this.gridViewComponent.reload();
        });
    }
    //#endregion
}
