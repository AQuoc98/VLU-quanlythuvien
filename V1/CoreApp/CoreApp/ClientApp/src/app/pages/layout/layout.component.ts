import { Component, OnInit } from '@angular/core';
import * as _ from 'lodash';

import { CoreMenuApiService } from '../../infrastructure/api-services';
import { MenuModel } from '../../infrastructure/models';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
  menus: MenuModel[];

  constructor(private coreMenuApiService: CoreMenuApiService) { }

  ngOnInit() {
    this.loadData();
  }

  private loadData() {
    this.getMenus();
  }

  private getMenus() {
    this.coreMenuApiService.getMenus().subscribe((res) => {
      this.menus = _.orderBy(res, (item) => item.position);
    });
  }
}
