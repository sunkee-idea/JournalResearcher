import { Component, OnInit, ElementRef,ViewChild} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-applicant-add',
  templateUrl: './applicant-add.component.html',
  styleUrls: ['./applicant-add.component.css']
})
export class ApplicantAddComponent implements OnInit {
  addthesisform: FormGroup;
  @ViewChild("ThesisUploader") ThesisUploader: ElementRef;
  constructor(private fb:FormBuilder) { }

  ngOnInit() {
    if (this.ThesisUploader.nativeElement.value === '') {
      alert('File cannot be empty');
    }
    this.validateField();
  }
  validateField() {
    this.addthesisform = this.fb.group({
      title: ['', Validators.required],
      abstract: ['', Validators.required],
      references: ['', Validators.required],
      author: ['', Validators.required],
      supervisorName: ['', Validators.required],
      thesisDate: ['', Validators.required],
      thesisfile:null
    });
  }

  uploadThesis() {
    
  }
}
