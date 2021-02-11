import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { RouteReuseStrategy, RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { HttpService } from './http/http.service';
import { HttpCacheService } from './http/http-cache.service';
import { ApiPrefixInterceptor } from './http/api-prefix.interceptor';
import { ErrorHandlerInterceptor } from './http/error-handler.interceptor';
import { CacheInterceptor } from './http/cache.interceptor';
import { ApiService, I18nService, HeaderService, RouteService, LocalStorage, SessionStorage, Toast, RouteReusableStrategy, ModalService } from './services';
import { AuthGuardInterceptor } from './interceptors/auth-guard.interceptor';
import { HttpTokenInterceptor } from './interceptors/http-token.interceptor';
import { CorrectDateInterceptor } from './interceptors';
import { UtilityService } from './utility';

@NgModule({
    imports: [
        CommonModule,
        HttpClientModule,
        TranslateModule,
        RouterModule
    ],
    providers: [
        HttpCacheService,
        ApiPrefixInterceptor,
        I18nService,
        HeaderService,
        ErrorHandlerInterceptor,
        CacheInterceptor,
        {
            provide: HttpClient,
            useClass: HttpService
        },
        {
            provide: RouteReuseStrategy,
            useClass: RouteReusableStrategy
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthGuardInterceptor,
            multi: true
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: HttpTokenInterceptor,
            multi: true
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: CorrectDateInterceptor,
            multi: true
        },
        ApiService,
        RouteService,
        LocalStorage,
        SessionStorage,
        Toast,
        ModalService,
        UtilityService
    ]
})
export class CoreModule {

    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
        // Import guard
        if (parentModule) {
            throw new Error(`${parentModule} has already been loaded. Import Core module in the AppModule only.`);
        }
    }

}