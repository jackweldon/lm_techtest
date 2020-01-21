import { Component, OnInit } from '@angular/core';
import { GroceriesService } from '../services/groceries.service';
import { grocery } from '../models/grocery';    

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
   
  items: grocery[];
  blob: any;
  constructor(private groceryService: GroceriesService) { }

  ngOnInit() {
    this.groceryService.get('').subscribe((m: any) => {
      this.items = m
    });
  }
  updatePrice(item: grocery) {
    console.log(item)
    this.groceryService.put('', item.fruit, item).subscribe((m:grocery) => {
      console.log(m)
      var newPrice = this.items.find(m => m.fruit == m.fruit);
      newPrice.price = m.price;
    });
  }

}
