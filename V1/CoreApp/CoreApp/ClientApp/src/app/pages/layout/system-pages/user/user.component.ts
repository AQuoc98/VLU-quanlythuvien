import { Component, OnInit, ViewChild } from '@angular/core';
import * as _ from 'lodash';

import { Guid } from '@infrastructure/common';
import { CoreUserApiService } from '@infrastructure/api-services';
import { GridViewComponent } from '@shared/components';
import { Logger } from '@core/logger';
import { RouteService } from '@core/services';
import { Toast } from '@core/services';
export const logger = new Logger('User');

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  //#region Fields
  @ViewChild(GridViewComponent)
  private gridViewComponent: GridViewComponent;
  //#endregion

  constructor(private routeService: RouteService, private coreUserApiService: CoreUserApiService, private toast: Toast) {

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
    this.routeService.navigate(['/admin/user/add', Guid.empty], { replaceUrl: true });
  }

  /**
   * Edit event from Grid view
   * @param recordId
   */
  editEvent(recordId) {
    this.routeService.navigate(['/admin/user/add', recordId], { replaceUrl: true });
  }

  /**
   * Delete event from Grid view
   * @param recordIds
   */
  deleteEvent(recordIds) {
    const entities = _.map(recordIds, (recordId) => {
      return { id: recordId };
    });
    this.coreUserApiService.delete(entities).subscribe((res) => {
      this.toast.apiResult(res);
      this.gridViewComponent.reload();
    });
  }
  //#endregion
}
