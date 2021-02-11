import { Component, OnInit, EventEmitter, Output, Input, ViewChild } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import * as _ from 'lodash';

import { Logger } from '@core/logger';
import { CoreViewApiService } from '@infrastructure/api-services';
import { QueryDataModel, PagingInfoModel, SearchDataModel } from '@infrastructure/models';
import { ModuleViewManagementService } from '@infrastructure/services/module-view-management.service';
import { GridViewSortStatusModel } from '@infrastructure/models';
import { Constants } from '@core/constants';
import { FilterControlComponent } from './components/filter-control/filter-control.component';

import { HeaderInfoModel } from '@infrastructure/models';
import { HeaderService } from '@core/services';

export const logger = new Logger('Grid View');

@Component({
  selector: 'app-grid-view',
  templateUrl: './grid-view.component.html',
  styleUrls: ['./grid-view.component.scss']
})
export class GridViewComponent implements OnInit {
  @Input() showImport: boolean = false;
  @Input() showChangeView: boolean = false;
  @Input() hideAdd: boolean = false;
  @Input() hideDelete: boolean = false;

  @Output() addEvent = new EventEmitter();
  @Output() editEvent = new EventEmitter();
  @Output() deleteEvent = new EventEmitter();
  @Output() importEvent = new EventEmitter();
  @Output() changeViewEvent = new EventEmitter();
  @Output() exportEvent = new EventEmitter();
  headerInfo: HeaderInfoModel = new HeaderInfoModel();

  pagingInfo: PagingInfoModel = {
    pageIndex: 1,
    pageSize: 10,
    totalRow: 0
  }
  queryDataModel: QueryDataModel = new QueryDataModel();
  searchDataModel: SearchDataModel = new SearchDataModel();
  gridViewColumns: any;
  gridViewData: any;
  isSelectedAll: boolean = false;
  isShowGridView: boolean = true;
  gridViewSortStatus: GridViewSortStatusModel = {
    columnId: '',
    expression: '',
    class: 'sorting'
  };
  isQueryData: boolean = true;
  private destroyed$ = new Subject();


  @ViewChild(FilterControlComponent)
  private filterControlComponent: FilterControlComponent;

  constructor(private coreViewApiService: CoreViewApiService, private moduleViewManagementService: ModuleViewManagementService, private headerService: HeaderService) {

  }

  ngOnInit() {
    this.updateHeader();
    this.listenEvents();

  }

  ngOnDestroy() {
    this.destroyed$.next();
    this.destroyed$.complete();
  }

  gridViewTrackByFn(index, item) {
    return item.id;
  }

  checkAll() {
    this.gridViewData.forEach(item => {
      item.isSelected = this.isSelectedAll;
    });
  }

  sortBy(col) {
    if (col.sortable != true)
      return;
    const expression = this.gridViewSortStatus.expression.indexOf(Constants.SortExpression.Asc) > -1 ? Constants.SortExpression.Desc : Constants.SortExpression.Asc;
    this.queryDataModel.orderExpression = `${col.sqlName} ${expression}`;
    this.gridViewSortStatus.expression = expression;
    this.gridViewSortStatus.columnId = col.id;
    this.gridViewSortStatus.class = `sorting_${expression.toLocaleLowerCase()}`;
    this.loadData();
  }

  sortingClass(col): string {
    if (this.gridViewSortStatus.columnId === col.id)
      return this.gridViewSortStatus.class;
    if (col.sortable === true)
      return 'sorting';
  }

  pageChanged($event) {
    this.pagingInfo.pageIndex = $event.page;
    this.loadData();
  }

  reload() {
    // Reset page index
    this.pagingInfo.pageIndex = 1;
    // Reset sort
    this.gridViewSortStatus.expression = '';
    this.gridViewSortStatus.columnId = '';
    this.loadData();
    this.filterControlComponent.clear();
  }

  add() {
    this.addEvent.emit();
  }

  edit(recordId) {
    this.editEvent.emit(recordId);
  }

  delete() {
    const selectedItems = _.filter(this.gridViewData, (item) => item.isSelected === true);
    const selectedIds = _.map(selectedItems, (item) => item.id);
    this.deleteEvent.emit(selectedIds);
  }

  checkDisableDeleteButton(): boolean {
    const countSelected = _.filter(this.gridViewData, (item) => item.isSelected === true);
    return countSelected.length === 0;
  }

  onFilter(conditions) {
    this.searchData(conditions);
  }

  import() {
    this.importEvent.emit();
  }

  export(){
    this.exportEvent.emit();
  }

  changeView() {
    this.changeViewEvent.emit();
  }

  hideGridView() {
    this.isShowGridView = false;
  }

  private listenEvents() {
    if (this.moduleViewManagementService.isHasValue) {
      this.loadData();
    } else {
      this.moduleViewManagementService.updatedEvent.pipe(takeUntil(this.destroyed$)).subscribe(() => {
        this.loadData();
      });
      this.headerService.updatedEvent$.subscribe(() => {
        this.updateHeader();
      });
    }
  }


  private updateHeader() {
    this.headerInfo = this.headerService.getHeaderInfo();
  }
  private loadData() {
    this.queryData();
  }

  private queryData() {
    const currentModule = this.moduleViewManagementService.currentModule;
    const currentView = this.moduleViewManagementService.currentView;
    this.queryDataModel.moduleId = currentModule.id;
    this.queryDataModel.viewId = currentView.id;
    this.queryDataModel.pageIndex = this.pagingInfo.pageIndex;
    this.queryDataModel.pageSize = this.pagingInfo.pageSize;
    this.coreViewApiService.queryData(this.queryDataModel).subscribe((res) => this.dataCallback(res));
  }

  private searchData(conditions) {
    this.searchDataModel.queryDataModel = this.queryDataModel;
    this.searchDataModel.searchConditions = conditions;
    this.coreViewApiService.searchData(this.searchDataModel).subscribe((res) => this.dataCallback(res.queryDataModel));
  }

  private dataCallback(res: any) {
    const visibleColumns = _.filter(res.columns, (item) => item.visible === true);
    this.gridViewColumns = _.orderBy(visibleColumns, (item) => item.position);
    this.gridViewData = res.data;
    this.pagingInfo.totalRow = res.totalRecord;
  }

}
