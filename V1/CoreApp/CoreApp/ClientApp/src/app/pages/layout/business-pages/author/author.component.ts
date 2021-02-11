import { Component, OnInit, ViewChild } from '@angular/core';
import * as _ from 'lodash';

import { Guid } from '@infrastructure/common';
import { CoreAuthorApiServices } from '@infrastructure/api-services';
import { GridViewComponent } from '@shared/components';
import { Logger } from '@core/logger';
import { RouteService } from '@core/services';
import { Toast } from '@core/services';

import { ConfirmDialogService } from '@shared/components';

export const logger = new Logger('Author');

@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrls: ['./author.component.scss']
})
export class AuthorComponent implements OnInit {

  @ViewChild(GridViewComponent)
  private gridViewComponent: GridViewComponent;

  constructor(private routeService: RouteService, private toast: Toast, private coreAuthorApiServices: CoreAuthorApiServices, private confirmDialogService: ConfirmDialogService) {


  }

  ngOnInit() {
  }


  addEvent() {
    this.routeService.navigate(['/admin/author/add', Guid.empty], { replaceUrl: true });
  }

  /**
 * Edit event from Grid view
 * @param recordId
 */

  editEvent(recordId) {
    this.routeService.navigate(['/admin/author/add', recordId], { replaceUrl: true });
  }

  deleteEvent(recordIds) {
    const entities = _.map(recordIds, (recordId) => {
      return { id: recordId };
    });

    // this.coreAuthorApiServices.delete(entities).subscribe((res) => {
    //   this.toast.apiResult(res);
    //   this.gridViewComponent.reload();
    // });

    this.confirmDialogService.confirm('CONFIRM_DIALOG.CONFRIM_DELETE_MESSAGE').then(
      (accept) => {
        // Accept
        this.coreAuthorApiServices.delete(entities).subscribe((res) => {
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
