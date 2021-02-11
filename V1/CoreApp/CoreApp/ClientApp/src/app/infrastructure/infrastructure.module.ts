import { NgModule } from '@angular/core';
import { ToastrModule } from 'ng6-toastr-notifications';
import { TranslateModule } from '@ngx-translate/core';

import { AuthService } from './auth/auth.service';
import { ModuleViewManagementService } from './services/module-view-management.service';
import * as apiServices from './api-services';

@NgModule({
  imports: [
    TranslateModule,
    ToastrModule.forRoot()
  ],
  exports: [],
  declarations: [],
  providers: [
    AuthService,
    ModuleViewManagementService,

    apiServices.CoreConfigApiService,
    apiServices.CoreEnumApiService,
    apiServices.CoreMenuApiService,
    apiServices.CoreModuleApiService,
    apiServices.CorePermissionApiService,
    apiServices.CoreTableApiServcice,
    apiServices.CoreUserApiService,
    apiServices.CoreViewApiService,
    apiServices.FileManagerApiService,
    apiServices.CoreAuthorApiServices,
    apiServices.CorePublisherApiService,
    apiServices.CoreMemberGroupApiService,
    apiServices.CoreCatalogApiServices,
    apiServices.CoreRackApiServices,
    apiServices.CoreFormatApiServices,
    apiServices.CoreBookApiServices,
    apiServices.CoreLanguageApiServices,
    apiServices.CoreBookItemApiServices,
    apiServices.CoreStatusApiServices,
    apiServices.CoreImportExportExcelApiServices,
    apiServices.CoreMemberApiServices,
    apiServices.CorePolicyApiServices
  ],
})
export class InfrastructureModule { }
