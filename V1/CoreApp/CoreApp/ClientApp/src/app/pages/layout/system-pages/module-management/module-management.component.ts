import { Component, OnInit, ViewChild } from '@angular/core';
import * as _ from 'lodash';

import { Guid } from '@infrastructure/common';
import { GridViewComponent } from '@shared/components';
import { CoreModuleApiService } from '@infrastructure/api-services';
import { RouteService } from '@core/services';
import { Logger } from '@core/logger';

export const logger = new Logger('Module Management');

@Component({
    selector: 'app-module-management',
    templateUrl: './module-management.component.html',
    styleUrls: ['./module-management.component.scss']
})
export class ModuleManagementComponent implements OnInit {
    //#region Fields
    @ViewChild(GridViewComponent)
    private gridViewComponent: GridViewComponent;
    //#endregion

    constructor(private routeService: RouteService, private coreModuleApiService: CoreModuleApiService) {

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
        this.routeService.navigate(['/admin/module-management/add', Guid.empty], { replaceUrl: true });
    }

    /**
     * Edit event from Grid view
     * @param recordId 
     */
    editEvent(recordId) {
        this.routeService.navigate(['/admin/module-management/add', recordId], { replaceUrl: true });
    }

    /**
     * Delete event from Grid view
     * @param recordIds 
     */
    deleteEvent(recordIds) {
        const entities = _.map(recordIds, (recordId) => {
            return { id: recordId };
        });
        this.coreModuleApiService.delete(entities).subscribe((res) => {
            this.gridViewComponent.reload();
        });
    }
    //#endregion
}
