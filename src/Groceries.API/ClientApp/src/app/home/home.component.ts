import { Component, OnInit } from '@angular/core';
import { grocery } from '../models/grocery';
import { GroceriesService } from '../services/groceries.service';
import { tap, map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  items: grocery[];
 
  constructor(private groceryService: GroceriesService) { }

  ngOnInit() {
    this.groceryService.get('').subscribe((m: any) => {
      this.items = m
    });
   }
   

} 
