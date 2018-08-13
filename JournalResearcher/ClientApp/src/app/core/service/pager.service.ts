import { Injectable } from '@angular/core';
import * as _ from 'underscore';

@Injectable()
export class PagerService {

  getPager(totalItems: number, currentPage: number = 1, pageSize: number = 10) {
    //Calculate total Pages
    let totalPages = Math.ceil(totalItems / pageSize);
    let startPage: number, endPage: number;

    if (totalPages <= 5) {
      startPage = 1;
      endPage = totalPages;
    } else {
      if (currentPage <= 3) {
        startPage = 1;
        endPage = 5;
      } else if (currentPage + 1 >= totalPages) {
        startPage = totalPages - 4;
        endPage = totalPages;
      } else {
        startPage = currentPage - 2;
        endPage = currentPage + 2;
      }
    }


    //Calculate start and end item indexes

    let startIndex = (currentPage - 1) * pageSize;
    let endIndex = Math.min(startIndex + pageSize - 1, totalItems - 1);

    //Create an array of pages to ngFor in the pager control

    let pages = _.range(startPage, endPage + 1);

    //return object with all pager properties required by the view

    return {
      totalItems: totalItems,
      currentPage: currentPage,
      endPage: endPage,
      pageSize: pageSize,
      totalPages: totalPages,
      startPage: startPage,
      startIndex: startIndex,
      endIndex: endIndex,
      pages: pages
    };

  }

}
