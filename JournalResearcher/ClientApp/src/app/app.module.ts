import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { Approuting} from './app.routing';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { SignInComponent } from './auth/sign-in/sign-in.component';
import { IntroBannerComponent } from './intro-banner/intro-banner.component';
import { FooterComponent } from './footer/footer.component';
import { AuthGuardService } from './core/service/guard/auth-gaurd.service';
import { AuthService } from './core/service/guard/auth.service';
import { RoleGuardService } from './core/service/guard/role-gaurd.service';
import { AuthInterceptor } from './core/interceptor/AuthInterceptor';
import { ErrorHandler } from './core/common/ErrorHandler';
import { DataKeeperService } from './core/service/data-keeper.service';
import { ServerService } from './core/service/server.service';
import { ToastrModule } from 'ngx-toastr';
import { ApplicantModule } from './applicant/applicant.module';
import { UserComponent } from './user/user.component';
import { UserAddComponent } from './user/user-add/user-add.component';
import { RouterModule } from '@angular/router';




@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    SignUpComponent,
    SignInComponent,
    IntroBannerComponent,
    FooterComponent,
    UserComponent,
    UserAddComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ApplicantModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-right',
      preventDuplicates: true,

    }),
    Approuting
//    RouterModule.forRoot([
//      { path: '', component: SignUpComponent},
//      { path: 'counter', component: CounterComponent },
//      { path: 'fetch-data', component: FetchDataComponent },
//    ])
  ],
  providers: [
    AuthService,
    AuthGuardService,
    RoleGuardService,
    DataKeeperService,
    ServerService,
    { provide: ErrorHandler, useClass: ErrorHandler },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
