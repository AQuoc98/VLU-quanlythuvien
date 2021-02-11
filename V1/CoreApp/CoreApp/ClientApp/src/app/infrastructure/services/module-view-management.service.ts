import { Injectable } from '@angular/core';
import { ModuleModel, ViewModel, MenuModel } from '../models';
import { Subject } from 'rxjs';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { filter } from 'rxjs/operators';

import { RouteService } from '../../core/services';


@Injectable()
export class ModuleViewManagementService {

    private cModule: ModuleModel;
    private cView: ModuleModel;
    private moduleViewUpdatedEvent$: Subject<any>;

    constructor() {
        this.moduleViewUpdatedEvent$ = new Subject();
    }

    setCurrentModuleView(module: ModuleModel, view: ViewModel) {
        this.cModule = module;
        this.cView = view;
        this.moduleViewUpdatedEvent$.next();
    }

    clearCurrentModuleView() {
        this.cModule = null;
        this.cView = null;
    }

    /**
     * Get current module
     * @return {ModuleModel} Current module
     */
    get currentModule(): ModuleModel | null {
        return this.cModule;
    }

    /**
     * Get current view
     * @return {ViewModel} Current view
     */
    get currentView(): ViewModel | null {
        return this.cView;
    }

    /**
    * Module and view Updated event
    * @return {Subject} Subject
    */
    get updatedEvent(): Subject<any> {
        return this.moduleViewUpdatedEvent$;
    }

    /**
    * Check module and view has value
    * @return {boolean}
    */
    get isHasValue(): boolean | false {
        return this.cModule != null || this.cView != null;
    }

}
