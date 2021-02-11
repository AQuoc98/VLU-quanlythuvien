import { Component, OnInit, ViewChild } from '@angular/core';
import * as _ from 'lodash';
import { Guid } from '@infrastructure/common';
import { CorePolicyApiServices } from '@infrastructure/api-services';
import { GridViewComponent } from '@shared/components';
import { Logger } from '@core/logger';
import { RouteService } from '@core/services';
import { Toast } from '@core/services';
import { ConfirmDialogService } from '@shared/components';

export const logger = new Logger('Policy');

@Component({
    selector: 'app-policy',
    templateUrl: './policy.component.html',
    styleUrls: ['./policy.component.scss']
  })
  export class PolicyComponent implements OnInit {

    @ViewChild(GridViewComponent)
    private gridViewComponent: GridViewComponent;

    constructor(private routeService: RouteService, private toast: Toast, private corePolicyApiServices: CorePolicyApiServices, private confirmDialogService: ConfirmDialogService) {


    }

    ngOnInit() {
    }


    addEvent() {
      this.routeService.navigate(['/admin/policy/add', Guid.empty ] , { replaceUrl: true } )
    }

    /**
   * Edit event from Grid view
   * @param recordId
   */

    editEvent(recordId) {
      this.routeService.navigate(['/admin/policy/add', recordId], { replaceUrl: true });
    }

    deleteEvent(recordIds) {
      const entities = _.map(recordIds, (recordId) => {
        return { id: recordId };
      });
      this.confirmDialogService.confirm('CONFIRM_DIALOG.CONFRIM_DELETE_MESSAGE').then(
        (accept) => {
          // Accept
          this.corePolicyApiServices.delete(entities).subscribe((res) => {
            this.toast.apiResult(res);
            this.gridViewComponent.reload();
          });
        },
        (decline) => {
          // Decline
          // Inogre
        });
    }
  }
