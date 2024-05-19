import { BearerTokenRequestAssignerInterceptor } from './common/interceptors/BearerTokenRequestAssignerInterceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MaterialExportModule } from './imported-modules/material.module';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { UnauthenticatedUserInterceptor } from './common/interceptors/UnauthenticatedUserInterceptor.interceptor';
import { ErrorHandlingHttpInterceptor } from './common/interceptors/ErrorHandlingHttpInterceptor';
import { RequestLoaderInterceptor } from './common/interceptors/RequestLoaderInterceptor';
import { PaginatorTranslationService } from './common/services/paginator-translation.service';
import { InternalCommonModule } from './common/internal-common.module';
import { MomentModule } from 'ngx-moment';
import { LoginComponent } from './modules/users/components/login/login.component';
import { SignupComponent } from './modules/users/components/signup/signup.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SignupComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    InternalCommonModule,
    MaterialExportModule,
    MomentModule
  ],
  providers: [
    provideAnimationsAsync(),
    { provide: MAT_DATE_LOCALE, useValue: 'pt-br' },
    { provide: HTTP_INTERCEPTORS, useClass: RequestLoaderInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: BearerTokenRequestAssignerInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: UnauthenticatedUserInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorHandlingHttpInterceptor, multi: true },
    { provide: MatPaginatorIntl, useClass: PaginatorTranslationService },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
