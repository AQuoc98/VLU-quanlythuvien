import { Component, OnInit, ViewChild } from '@angular/core';
import * as _ from 'lodash';

import { Guid } from '@infrastructure/common';
import { CoreBookItemApiServices } from '@infrastructure/api-services';
import { GridViewComponent } from '@shared/components';
import { Logger } from '@core/logger';
import { RouteService } from '@core/services';
import { Toast } from '@core/services';
import { ConfirmDialogService } from '@shared/components';

import { HttpClient } from '@angular/common/http';
import { saveAs } from 'file-saver'

export const logger = new Logger('BookItem');


@Component({
  selector: 'app-bookitem',
  templateUrl: './bookitem.component.html',
  styleUrls: ['./bookitem.component.scss']
})
export class BookItemComponent implements OnInit {

  @ViewChild(GridViewComponent)
  private gridViewComponent: GridViewComponent;
  showImport: boolean = true

  constructor(private routeService: RouteService,
    private toast: Toast,
    private coreBookItemApiServices: CoreBookItemApiServices,
    private confirmDialogService: ConfirmDialogService,
    private http: HttpClient) {


  }

  ngOnInit() {
  }



  addEvent() {
    this.routeService.navigate(['/admin/bookitem/add', Guid.empty], { replaceUrl: true });
  }

  /**
 * Edit event from Grid view
 * @param recordId
 */

  editEvent(recordId) {
    this.routeService.navigate(['/admin/bookitem/add', recordId], { replaceUrl: true });
  }

  deleteEvent(recordIds) {
    const entities = _.map(recordIds, (recordId) => {
      return { id: recordId };
    });

    this.confirmDialogService.confirm('CONFIRM_DIALOG.CONFRIM_DELETE_MESSAGE').then(
      (accept) => {
        // Accept
        this.coreBookItemApiServices.delete(entities).subscribe((res) => {
          this.toast.apiResult(res);
          this.gridViewComponent.reload();
        });
      },
      (decline) => {
        // Decline
        // Inogre
      });
  }

  importEvent() {
    this.routeService.navigate(['/admin/bookitem/import-excel'], { replaceUrl: true })
  }


  exportEvent() {
    let excelUrl = ''
    let excelName = 'BookItem.xlsx'
    // let FileSaver = require('file-saver');
    this.http.get('http://171.244.34.24:808/api/DoBookItemApi/Download/', { responseType: 'text' }).subscribe(res => { excelUrl = res; saveAs(excelUrl, excelName) })

  }





}
