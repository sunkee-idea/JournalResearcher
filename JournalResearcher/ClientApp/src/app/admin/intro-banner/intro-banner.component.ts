import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-intro-banner',
  templateUrl: './intro-banner.component.html',
  styleUrls: ['./intro-banner.component.css']
})
export class IntroBannerComponent implements OnInit {
  @Input() data:string;
  @Input() action:string;
  constructor() { }

  ngOnInit() {
  }

}
