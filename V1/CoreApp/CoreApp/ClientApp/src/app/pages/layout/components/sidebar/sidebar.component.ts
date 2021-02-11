import { Component, OnInit, Input, SimpleChanges, SimpleChange } from '@angular/core';
import * as _ from 'lodash';
import { Router, ActivatedRoute, NavigationEnd } from '@angular/router';
import { Observable, forkJoin } from 'rxjs';
import { map, filter } from 'rxjs/operators';

import { AuthService } from '@infrastructure/auth/auth.service';
import { MenuModel } from '@infrastructure/models';
import { CoreModuleApiService, CoreViewApiService } from '@infrastructure/api-services';
import { ModuleViewManagementService } from '@infrastructure/services/module-view-management.service';


@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  @Input() data: MenuModel[] = [];
  menus: MenuModel[] = [];

  isOpenSlideBar = false;

  constructor(private authService: AuthService,
    private coreModuleApiService: CoreModuleApiService,
    private coreViewApiService: CoreViewApiService,
    private moduleViewManagementService: ModuleViewManagementService,
    private router: Router,
    private activedRoute: ActivatedRoute) {
    this.listenRouteChange();
  }

  ngOnInit() {

  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes.data.currentValue !== changes.data.previousValue) {
      this.buildMenus();
      this.checkCurrentMenuActived();
    }
  }

  private buildMenus() {
    const tmpMenus: MenuModel[] = [];
    const parentMenus = _.filter(this.data, (item) => item.parentId === null);
    parentMenus.forEach(menu => {
      menu.childs = _.filter(this.data, (item) => item.parentId === menu.id);
      tmpMenus.push(menu);
    });
    this.menus = tmpMenus;
  }

  private listenRouteChange() {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(async (e) => {
      this.checkCurrentMenuActived();
    });
  }

  private async checkCurrentMenuActived() {
    const currentRouteActived = await this.getCurrentRouteActived();
    const currentMenuActived = _.find(this.data, (item) => item.href === currentRouteActived);
    if (currentMenuActived) {
      this.moduleViewManagementService.clearCurrentModuleView();
      this.updateCurrentModuleAndView(currentMenuActived.moduleId, currentMenuActived.viewId);
    }
  }

  private getCurrentRouteActived(): Promise<string> {
    const promise = new Promise<string>((resolve, reject) => {
      this.activedRoute.url.subscribe(() => {
        const currentRouterActiveLink = this.activedRoute.snapshot.firstChild.url.join('');
        resolve(currentRouterActiveLink);
      }, (error) => reject());
    });
    return promise;
  }

  private updateCurrentModuleAndView(moduleId: string, viewId: string) {
    if (moduleId && viewId) {
      forkJoin(this.coreModuleApiService.getById(moduleId), this.coreViewApiService.getById(viewId)).subscribe(
        (res) => {
          this.moduleViewManagementService.setCurrentModuleView(res[0], res[1]);
        }
      );
    }
  }
}
