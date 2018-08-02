import { Component, OnInit } from '@angular/core';
import { ServerService } from '../core/service/server.service';
import { IThesis } from '../core/models/IThesis';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  theses:IThesis[];
  isLoading:boolean = false;
  constructor(private apiService:ServerService) { }

  ngOnInit() {
    this.isLoading = true;
    this.apiService.getThesis("403920-dedejd-3rf3j2k").subscribe((res) => {
        this.isLoading = false;
        this.theses = res;
        console.log(this.theses);
      },
      error => {
        this.isLoading = false;
        console.log(error);
      });
  }

}
