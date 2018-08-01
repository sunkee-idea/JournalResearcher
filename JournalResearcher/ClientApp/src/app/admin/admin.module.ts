import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { Adminrouting } from './admin.routing';

@NgModule({
  imports: [
    CommonModule,
    Adminrouting
  ],
  declarations: [AdminComponent]
})
export class AdminModule { }
