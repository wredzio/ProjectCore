import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { AlertComponent } from './components/shared/alert/alert.component';
import { AlertService } from './components/shared/alert/alert.service';
import { LoaderComponent } from './components/shared/loader/loader.component';
import { LoaderService } from './components/shared/loader/loader.service';
import { AuthenticationService } from './components/shared/authentication/authentication.service';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './components/shared/AuthGuard/auth.guard';
import { LoaderInterceptor } from './interceptors/loader.interceptor';
import { ErrorInterceptor } from './interceptors/error.interceptor';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        AlertComponent,
        LoginComponent,
        RegisterComponent,
        LoaderComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: 'login', component: LoginComponent },
            { path: 'register', component: RegisterComponent },
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        AuthGuard,
        AlertService,
        AuthenticationService,
        LoaderService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: LoaderInterceptor,
            multi: true,
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: ErrorInterceptor,
            multi: true,
        }
    ]
})
export class AppModuleShared {
}
