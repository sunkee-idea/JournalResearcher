import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../../core/service/guard/auth.service';
import { ServerService } from '../../core/service/server.service';

@Component({
  selector: 'app-user-add',
  templateUrl: './user-add.component.html',
  styleUrls: ['./user-add.component.css']
})
export class UserAddComponent implements OnInit {
  addthesisform: FormGroup;
  isLoading :boolean = false;
  @ViewChild("ThesisUploader") ThesisUploader: ElementRef;
  constructor(private fb: FormBuilder,private auth:AuthService,private apiService:ServerService) { }

  ngOnInit() {
    if (this.ThesisUploader.nativeElement.value = '') {
      alert('File cannot be empty');
    }
    this.ValidateField();
  }
  ValidateField() {
    this.addthesisform = this.fb.group({
      title: ['', Validators.required],
      abstract: ['', Validators.required],
      references: ['', Validators.required],
      author: ['', Validators.required],
      supervisorName: ['', Validators.required],
      thesisDate: ['', Validators.required],
      thesisfile: [null,Validators.required]
    });
  }

  onFileChange(event) {
    let reader = new FileReader();
    if (event.target.files && event.target.files.length > 0) {
      let file = event.target.files[0];
      reader.readAsDataURL(file)
      reader.onload = () => {
        console.log(reader.result)
      }
    }
    ;
    if (event.target.files.length > 0) {
      let file = event.target.files[0];
      this.addthesisform.get('thesisfile').setValue(file);
    }

  }


  private prepareUser(): FormData {
    let formData = new FormData();
    formData.append('ThesisFile', this.addthesisform.get('thesisfile').value);
    formData.append('Title', this.addthesisform.get('title').value);
    formData.append('Abstract', this.addthesisform.get('abstract').value);
    formData.append('SupervisorName', this.addthesisform.get('supervisorName').value);
    formData.append('Author', this.addthesisform.get('author').value);
    formData.append('Reference', this.addthesisform.get('references').value);
    formData.append('ThesisDateTime',this.addthesisform.get('thesisDate').value);
    formData.append('ApplicantId',this.auth.Key);
    return formData;
  }

  uploadThesis() {
    this.isLoading = true;
    let formModel = this.prepareUser();
    this.apiService.submitThesis(formModel).subscribe((res) => {
      console.log(res);
      this.isLoading = false;
    }, error => {
      this.isLoading = false;
      console.log(error);
    })
  }

  clearFile() {
    this.addthesisform.get('thesisfile').setValue(null);
    this.ThesisUploader.nativeElement.value = '';
  }
}
