import { Component, OnInit } from '@angular/core';
import { ServerService } from '../core/service/server.service';
import { IThesis, IPaginationModel } from '../core/models/IThesis';
import { PagerService } from '../core/service/pager.service';
import { ConfirmationService} from "@jaspero/ng-confirmations";
import {IResolveEmit} from '../core/models/resolve-emit';
import {IConfirmSettings} from '../core/models/confirm-settings';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
 paginatedResult:IPaginationModel;
  datepickerModel = {};
  pager: any = {};
  page: number;
  filtered:string;
  // paged items
  pagedItems: IThesis[];
  theses:IThesis[];
  isLoading: boolean = false;

  settings: IConfirmSettings | any = {
    overlay: true,
    overlayClickToClose: true,
    showCloseButton: true,
    confirmText: 'Yes',
    declineText: 'No'
  };
  constructor(private apiService:ServerService,private pagerService:PagerService,private confrimationService:ConfirmationService) { }

  ngOnInit() {

    this.page = 1;
    this.isLoading = true;
    this.filtered = JSON.stringify(this.datepickerModel);
    this.apiService.getAllThesis(this.filtered, this.page).subscribe((res) => {
        this.isLoading = false;
      this.paginatedResult = res;
        this.theses = this.paginatedResult.items;
        this.setPage(1);
        console.log(this.paginatedResult);
      },
      error => {
        this.isLoading = false;
        console.log(error);
      });
  }


  setPage(page: number) {
    if (page < 1 || page > this.paginatedResult.total) {
      return;
    }
    this.page = page;
    // get pager object from service
    this.pager = this.pagerService.getPager(this.paginatedResult.items.length, page);
    // get current page of items
    this.paginatedResult.items = this.paginatedResult.items.slice(this.pager.startIndex, this.pager.endIndex + 1);
  }

  nextPage() {
    this.page++;
    this.paginate(this.page);
  }

  confirmAction(thesis :IThesis) {
    this.confrimationService.create("Confirm Action", "Confirm you want to perform this action", this.settings)
      .subscribe((ans: IResolveEmit) => {
        if (ans.resolved) {
          
          this.approveThesis(thesis);
          return true;
        } 
          return false;
        
      });

  }
  approveThesis(thesis:IThesis) {
    thesis.isApproved = !thesis.isApproved;
    thesis.action = thesis.isApproved ? 'Approve' : 'Reject';
    this.apiService.approveThesis(thesis).subscribe((res) => {
        console.log(res);
      },
      error => {
        console.log(error);
      });


  }

  filter() {
    this.isLoading = true;
    this.filtered = JSON.stringify(this.datepickerModel);
    this.apiService.getAllThesis(this.filtered, this.page = 1).subscribe((res) => {
        this.isLoading = false;
        this.paginatedResult= res;
      console.log(this.paginatedResult.items);
      },
      error => {
        this.isLoading = false;
        console.log(error);
      });
  }

  paginate(page:number) {
    this.apiService.getAllThesis(this.filtered, page).subscribe((res) => {
        this.isLoading = false;
      this.paginatedResult = res;
        this.theses = this.paginatedResult.items;
      console.log(this.paginatedResult.items);
      },
      error => {
        this.isLoading = false;
        console.log(error);
      });
  }

}
