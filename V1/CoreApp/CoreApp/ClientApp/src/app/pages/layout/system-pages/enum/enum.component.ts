import { Component, OnInit, ViewChild } from '@angular/core';
import * as _ from 'lodash';

import { Guid } from '@infrastructure/common';
import { GridViewComponent } from '@shared/components';
import { Logger } from '@core/logger';
import { RouteService } from '@core/services';
import { CoreEnumApiService } from '@infrastructure/api-services';

export const logger = new Logger('Enum');

@Component({
    selector: 'app-enum',
    templateUrl: './enum.component.html',
    styleUrls: ['./enum.component.scss']
})
export class EnumComponent implements OnInit {
    //#region Fields
    @ViewChild(GridViewComponent)
    private gridViewComponent: GridViewComponent;
    //#endregion

    constructor(private routeService: RouteService, private coreEnumApiService: CoreEnumApiService) {

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
        this.routeService.navigate(['/admin/enum/add', Guid.empty], { replaceUrl: true });
    }

    /**
     * Edit event from Grid view
     * @param recordId 
     */
    editEvent(recordId) {
        this.routeService.navigate(['/admin/enum/add', recordId], { replaceUrl: true });
    }

    /**
     * Delete event from Grid view
     * @param recordIds 
     */
    deleteEvent(recordIds) {
        const entities = _.map(recordIds, (recordId) => {
            return { id: recordId };
        });
        this.coreEnumApiService.delete(entities).subscribe((res) => {
            this.gridViewComponent.reload();
        });
    }
    //#endregion
}
