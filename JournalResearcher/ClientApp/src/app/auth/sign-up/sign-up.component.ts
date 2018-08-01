import { Component, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators} from "@angular/forms";
import { ValidationService } from "../../core/service/validation.service";
import { IUser } from "../../core/models/IUser";
import { SystemConstant as Roles} from "../../shared/constant";
import { ServerService } from "../../core/service/server.service";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: "app-sign-up",
  templateUrl: "./sign-up.component.html",
  styleUrls: ["./sign-up.component.css"]
})
export class SignUpComponent implements OnInit {
  signupform: FormGroup;
  user: IUser;
  notificationType: string;
  notificationMsg: string;
  isLoading:boolean = false;

  constructor(private apiService: ServerService, private toastrService: ToastrService) {}

  ngOnInit() {
   
    this.validateFields();
  }

  validateFields() {
    this.signupform = new FormGroup({
        firstName: new FormControl("", Validators.required),
        lastName: new FormControl("", Validators.required),
        phone: new FormControl("", Validators.required),
        password: new FormControl("", [Validators.required, ValidationService.passwordValidator]),
        email: new FormControl("", Validators.email),
        confirmpassword: new FormControl("", Validators.required)
      },
      ValidationService.passwordMatchValidator.bind(this));
  }

  signupHandle() {
    this.isLoading = true;
    this.user = this.signupform.value;
    this.user.role = Roles.LEVEL_ACCESS.u;
    this.user.phoneNumber = this.signupform.value.phone;
    this.apiService.signupApplicant(this.user).subscribe((user: IUser) => {
      this.notificationType = "success";
        this.notificationMsg = "Your account have been created successfully";
      this.toastrService.success("Your account have been created successfully");
        this.isLoading = false;

      },
      error => {
        this.notificationType = "error";
        this.notificationMsg = error;
        this.toastrService.error(error, "Sorry,an error occurred,try again");
        this.isLoading = false;
      });

  }

}
