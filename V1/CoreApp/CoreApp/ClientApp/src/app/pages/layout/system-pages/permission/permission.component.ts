import { Component, OnInit, ViewChild } from '@angular/core';
import * as _ from 'lodash';

import { Guid } from '@infrastructure/common';
import { GridViewComponent } from '@shared/components';
import { CorePermissionApiService } from '@infrastructure/api-services';
import { Logger } from '@core/logger';
import { RouteService } from '@core/services';

export const logger = new Logger('Menu');

@Component({
    selector: 'app-permission',
    templateUrl: './permission.component.html',
    styleUrls: ['./permission.component.scss']
})
export class PermissionComponent implements OnInit {
    //#region Fields
    @ViewChild(GridViewComponent)
    private gridViewComponent: GridViewComponent;
    //#endregion

    constructor(private routeService: RouteService, private corePermissionApiService: CorePermissionApiService) {

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
        this.routeService.navigate(['/admin/permission/add', Guid.empty], { replaceUrl: true });
    }

    /**
     * Edit event from Grid view
     * @param recordId 
     */
    editEvent(recordId) {
        this.routeService.navigate(['/admin/permission/add', recordId], { replaceUrl: true });
    }

    /**
     * Delete event from Grid view
     * @param recordIds 
     */
    deleteEvent(recordIds) {
        const entities = _.map(recordIds, (recordId) => {
            return { id: recordId };
        });
        this.corePermissionApiService.delete(entities).subscribe((res) => {
            this.gridViewComponent.reload();
        });
    }
    //#endregion
}
