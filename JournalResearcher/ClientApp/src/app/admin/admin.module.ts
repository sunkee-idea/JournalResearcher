import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { Adminrouting } from './admin.routing';
import { AuthService } from '../core/service/guard/auth.service';
import { ServerService } from '../core/service/server.service';

@NgModule({
  imports: [
    CommonModule,
    Adminrouting
  ],
  declarations: [AdminComponent],
  providers: [
    AuthService,
    ServerService
    ]
})
export class AdminModule { }
