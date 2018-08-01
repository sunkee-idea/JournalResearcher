import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApplicantAddComponent } from './applicant-add/applicant-add.component';
import { ApplicantComponent } from './applicant.component';
import { ApplicantRouting } from './applicant.routing';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';




@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ApplicantRouting
  ],
  declarations: [
    ApplicantAddComponent,
    ApplicantComponent
  ]
})
export class ApplicantModule { }
