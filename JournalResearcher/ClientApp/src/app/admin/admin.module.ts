import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { Adminrouting } from './admin.routing';
import { AuthService } from '../core/service/guard/auth.service';
import { ServerService } from '../core/service/server.service';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import {BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { IntroBannerComponent } from './intro-banner/intro-banner.component';
import { PagerService } from '../core/service/pager.service';
import { NgbModule, NgbDateParserFormatter } from '@ng-bootstrap/ng-bootstrap';
import { NgbDateCustomParserFormatter } from '../core/common/dateFormat';
import { JasperoConfirmationsModule } from '@jaspero/ng-confirmations';


@
NgModule({
  imports: [
    CommonModule,
    Adminrouting,
    FormsModule,
    BsDatepickerModule.forRoot(),
    NgbModule,
    JasperoConfirmationsModule.forRoot()
   
  ],
  declarations: [AdminComponent,NavMenuComponent,IntroBannerComponent],
  providers: [
    AuthService,
    ServerService,
    PagerService,
    { provide: NgbDateParserFormatter, useClass: NgbDateCustomParserFormatter }
    
    ]
})
export class AdminModule { }
