import { Component, OnInit } from '@angular/core';
import { AuthService } from '../core/service/guard/auth.service';
import { ServerService } from '../core/service/server.service';
import { IThesis } from '../core/models/IThesis';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  theses: IThesis[];
  isLoading: boolean = false;
  notificationType: string;
  notificationMsg: string;
  constructor(private auth:AuthService,private apiService: ServerService) { }

  ngOnInit() {
   this.getTheses();
  }


  getTheses() {
    this.isLoading = true;
    this.apiService.getThesis(this.auth.Key).subscribe((res) => {
        this.isLoading = false;
        this.theses = res;
        //console.log(this.theses);
      },
      error => {
        this.isLoading = false;
        this.notificationMsg = "Error Occurred while fetching Data,try reload the page";
        //console.log(error);
      });
  }

}
